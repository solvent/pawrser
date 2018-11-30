using System.Text.RegularExpressions;
using System.Collections.Generic;
using System.Linq;

namespace pawrser.classes {

	public class FileLine {
		public bool IsFile;
		public string Title;
		public string Episode;
		public IEnumerable<string> Tags;

		public FileLine(string path) {
			var pieces = path.Split('/');
			IsFile = regex.IsMatch(pieces.Last());
			if (IsFile) {
				Title = pieces[pieces.Count() - 2];
				Episode = pieces[pieces.Count() - 1];
				Tags = pieces.Skip(3).Take(pieces.Count() - 5);
			} else {
				Title = pieces[pieces.Count() - 1];
				Tags = pieces.Skip(3).Take(pieces.Count() - 4);
			}
		}

		private static Regex regex = new Regex(@"^[\w,\s-]+\.[A-Za-z]+$");
	}

}
