using System.IO;

namespace pawrser.classes;

public class DbFileConfig : BaseFileConfig {

	private const string DEFAULT_NAME = "db.sqlite";

	public DbFileConfig(string destination) {
		try {
			_fileInfo = new FileInfo(destination);
			var directory = _fileInfo.DirectoryName;
			if (!Directory.Exists(directory)) {
				Directory.CreateDirectory(directory);
			}
		} catch {
			// is okay, using default DB name
			_fileInfo = new FileInfo(DEFAULT_NAME);
		}
	}
}
