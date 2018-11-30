using System;
using System.IO;
using System.Linq;
using System.Text;

namespace pawrser.classes {

	public class MainLoop {
		public DbFileConfig DbFileConfig;
		public SrcFileConfig SrcFileConfig;

		public MainLoop() {
		}

		public void Process() {
			if (DbFileConfig == null || SrcFileConfig == null) {
				throw new ArgumentNullException();
			}

			File.ReadLines(SrcFileConfig.GetFilePath(), Encoding.UTF8)
				.Select(x => new FileLine(x))
				.Where(x => x.IsFile)
				.GroupBy(x => x.Title, (a, b) => new {
					Title = a,
					Tags = b.FirstOrDefault()?.Tags,
					Episodes = b.Select(y => y.Episode)
				})
				.ToList()
				.ForEach(x => Console.WriteLine(x.Title + "[" + string.Join(",", x.Tags) + "]"));
		}
	}

}
