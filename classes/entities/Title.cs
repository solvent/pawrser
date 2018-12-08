using System.Collections.Generic;

namespace pawrser.classes.entities {

	public class Title {

		public int Id {
			get; set;
		}

		public string Name {
			get; set;
		}

		public List<Episode> Episodes {
			get; set;
		}

		public List<TagRef> TagRefs {
			get; set;
		}
	}

}
