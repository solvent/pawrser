using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using librawry.portable;
using librawry.portable.entities;
using Microsoft.EntityFrameworkCore;
using ShellProgressBar;

namespace pawrser.classes;

public class MainLoop {
	public DbFileConfig DbFileConfig;
	public SrcFileConfig SrcFileConfig;
	public InputLineFilter InputLineFilter;

	public MainLoop() {
	}

	private struct TitleStruct {
		public string Title;
		public IEnumerable<string> Tags;
		public IEnumerable<string> Episodes;
	}

	public void Process() {
		if (DbFileConfig == null || SrcFileConfig == null) {
			throw new ArgumentNullException();
		}

		var contextOptions = new DbContextOptionsBuilder<LibrawryContext>()
			.UseSqlite("Data Source=" + DbFileConfig.GetFilePath())
			.Options;

		using (var db = new LibrawryContext(contextOptions)) {
			db.Database.EnsureDeleted();
			db.Database.EnsureCreated();
			db.ChangeTracker.AutoDetectChangesEnabled = false;

			var data = File.ReadLines(SrcFileConfig.GetFilePath(), Encoding.UTF8)
				.Where(InputLineFilter.GetExpression())
				.Select(x => new InputLine(x))
				.Where(x => x.IsFile)
				.GroupBy(x => x.Title, (a, b) => new TitleStruct() {
					Title = a,
					Tags = b.FirstOrDefault()?.Tags,
					Episodes = b.Select(y => y.Episode)
				})
				.ToList();

			// without progress bar
			if (Console.IsInputRedirected || Console.IsOutputRedirected) {
				data.ForEach(x => {
					ProcessTitle(db, x);
					Console.Write(".");
				});
				return;
			}

			// with progress bar
			var options = new ProgressBarOptions {
				ProgressCharacter = '─',
				ProgressBarOnBottom = true
			};
			using (var pbar = new ProgressBar(data.Count, "Processing...", options)) {
				data.ForEach(x => {
					ProcessTitle(db, x);
					pbar.Tick();
				});
				pbar.Tick("Done.");
			}

		}
	}

	private void ProcessTitle(LibrawryContext db, TitleStruct x) {
		var title = new Title() {
			Name = x.Title
		};
		db.Titles.Add(title);
		db.SaveChanges();

		var tagRefs = new List<TagRef>();
		x.Tags.ToList().ForEach(tagName => {
			var tag = db.Tags.FirstOrDefault(t => t.Name == tagName);
			if (tag == null) {
				tag = new Tag() {
					Name = tagName
				};
				db.Tags.Add(tag);
				db.SaveChanges();
			}
			tagRefs.Add(new TagRef() {
				TitleId = title.Id,
				TagId = tag.Id
			});
		});
		db.TagRefs.AddRange(tagRefs);

		db.Episodes.AddRange(
			x.Episodes.Select(episodeName => new Episode() {
				TitleId = title.Id,
				Name = episodeName
			})
		);

		db.SaveChanges();
	}
}
