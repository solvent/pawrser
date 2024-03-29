using System;
using System.Collections.Generic;
using System.Linq;

namespace pawrser.classes;

public class InputLineFilter {

	private readonly IEnumerable<string> PathsToExclude = new List<string>() {
			"/Temp/",
			"/Work/"
		};

	public Func<string, bool> GetExpression() {
		return x => PathsToExclude.FirstOrDefault(ex => x.Contains(ex)) == null;
	}
}
