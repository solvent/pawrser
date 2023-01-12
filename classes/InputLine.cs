using System.Text.RegularExpressions;
using System.Collections.Generic;
using System.Linq;

namespace pawrser.classes;

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
			"08_Hachi", "Live!"
		};

	private static readonly IEnumerable<string> TagsToSkip = new List<string>() {
			"", "mnt", "anime"
		};

	private bool IsTag(string piece) {
		return YearRegEx.IsMatch(piece) || KnownTags.Contains(piece);
	}

	public InputLine(string path) {
		var pieces = path.Split('/').Where(x => !TagsToSkip.Contains(x));
		IsFile = pieces.FirstOrDefault() != null
			? FileExtentionRegEx.IsMatch(pieces.LastOrDefault())
			: false;
		Tags = pieces.Where(x => IsTag(x));
		Episode = IsFile ? pieces.LastOrDefault() : "";
		Title = pieces.Where(x => x != Episode && !IsTag(x)).FirstOrDefault()
			?? "Random Pieces";
	}

	private static Regex FileExtentionRegEx = new Regex(@"\.[A-Za-z0-9]{1,4}$");
	private static Regex YearRegEx = new Regex(@"^(19|20)\d{2}$");
}
