using System.IO;

namespace pawrser.classes {

	public class BaseFileConfig {

		protected FileInfo _fileInfo = null;

		public string GetFilePath() {
			return _fileInfo.ToString();
		}
	}

}
