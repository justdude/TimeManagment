using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BugTracker.Constants
{
	public static class Global
	{
		public const string AppName = "BugTracker";

		public const string Example = @"Plus S/E 0.01/0 end timer.";
		public const string Plus = "plus!";
		public const string EndTimer = "end timer.";
		public const string SpentEstimate = "S/E";
		public const string Delimeter = "/";

		public const string ClosedStateText = "Closed";
		public const string OpenStateText = "Open";
		public const string FixedStateText = "Fixed";
		public const string ReopenedStateText = "Reopened";

		public static List<string> StatesItems = new List<string>()
		{
			Global.ClosedStateText,
			Global.OpenStateText,
			Global.FixedStateText,
			Global.ReopenedStateText
		};

		enum Status
		{
			Open,
			Closed,
			Fixed,
			Reopened
		}


	}
}
