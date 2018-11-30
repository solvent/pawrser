using System;
using pawrser.classes;

namespace pawrser {

	class Program {
		static void Main(string[] args) {
			if (args.Length == 0 || args.Length > 2) {
				PrintUsage();
				return;
			}
			var source = args[0];
			var destination = args.Length == 2 ? args[1] : null;

			var mainLoop = new MainLoop() {
				SrcFileConfig = new SrcFileConfig(source),
				DbFileConfig = new DbFileConfig(destination)
			};

			mainLoop.Process();
		}

		static void PrintUsage() {
			Console.WriteLine("Usage: dotnet AnimeTreePawrser.dll LIB_2018_04_21.txt [result.sqlite]");
		}
	}
}
