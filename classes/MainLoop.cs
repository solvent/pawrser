using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace pawrser.classes {

	public class MainLoop {
		public DbFileConfig DbFileConfig;
		public SrcFileConfig SrcFileConfig;
		public InputLineFilter InputLineFilter;

		public MainLoop() {
		}

		public void Process() {
			if (DbFileConfig == null || SrcFileConfig == null) {
				throw new ArgumentNullException();
			}

			File.ReadLines(SrcFileConfig.GetFilePath(), Encoding.UTF8)
				.Where(InputLineFilter.GetExpression())
				.Select(x => new InputLine(x))
				.Where(x => x.IsFile)
				.GroupBy(x => x.Title, (a, b) => new {
					Title = a,
					Tags = b.FirstOrDefault()?.Tags,
					Episodes = b.Select(y => y.Episode)
				})
				.ToList()
				.ForEach(x => Console.WriteLine(
					Environment.NewLine +
					x.Title + Environment.NewLine +
					" = [ " + string.Join(", ", x.Tags) + " ]" + Environment.NewLine +
					" - " + string.Join(Environment.NewLine + " - ", x.Episodes) +
					Environment.NewLine
				));
		}
	}

}
