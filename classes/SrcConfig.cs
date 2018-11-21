using System.IO;

namespace pawrser.classes {

	public class SrcFileConfig : BaseFileConfig {

		public SrcFileConfig(string source) {
			try {
				_fileInfo = new FileInfo(source);
			} catch {
				// doing nothing
			}
		}

		public bool IsValid => _fileInfo != null && _fileInfo.Exists;
	}

}
