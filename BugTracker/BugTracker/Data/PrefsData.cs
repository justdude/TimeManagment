using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BugTracker.Data
{
	public class PrefsData
	{
		public bool calendarFeedEnabled {get; set;}
		public bool canBePublic {get; set;}
		public bool canBeOrg {get; set;}
		public bool canBePrivate {get; set;}
		public bool canInvite { get; set; }
	}
}
