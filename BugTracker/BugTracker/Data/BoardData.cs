
using System.Collections.Generic;
namespace BugTracker.Data
{
	public class BoardData
	{
		public string Id { get; set; }
		public string Name { get; set; }
		public string Desc { get; set; }
		public bool Closed {get; set;}
		public string IdOrganization {get; set;}
		public bool Invited {get; set;}
		public bool Pinned {get; set;}
		public bool Starred {get; set;}
		public string Url {get; set;}
		//public object DescData {get; set;}

		public PrefsData Prefs { get; set; }

		public List<ListData> Lists { get; set; }
	}
}