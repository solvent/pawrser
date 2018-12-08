using System.Text.RegularExpressions;
using System.Collections.Generic;
using System.Linq;

namespace pawrser.classes {

	public class InputLine {
		public bool IsFile;
		public string Title;
		public string Episode;
		public IEnumerable<string> Tags;

		private static readonly IEnumerable<string> KnownTags = new List<string> {
			"01_Ichi",  "Bad Named",   "Long Ongoing",
			"02_Ni",    "Completed",   "Not Anime",
			"03_San",   "Dub",         "Raw",
			"04_Shi",   "Fansub",      "Raws",
			"05_Go",    "Good Named",  "SD",
			"06_Roku",  "Hardsub",     "Softsub",
			"07_Nana",  "HD",          "VHS & DVD Rip, etc",
			"08_Hachi"
		};

		private static readonly IEnumerable<string> TagsToSkip = new List<string>() {
			"", "mnt", "anime"
		};

		public InputLine(string path) {
			var pieces = path.Split('/').Where(x => !TagsToSkip.Contains(x));
			IsFile = pieces.FirstOrDefault() != null
				? regex.IsMatch(pieces.LastOrDefault())
				: false;
			Tags = pieces.Where(x => KnownTags.Contains(x));
			Episode = IsFile ? pieces.LastOrDefault() : "";
			Title = pieces.Where(x => x != Episode && !KnownTags.Contains(x)).FirstOrDefault()
				?? "Random Pieces";
		}

		private static Regex regex = new Regex(@"\.[A-Za-z0-9]{1,4}$");
	}

}
