using BugTracker.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BugTracker.Services.Trello
{
	public class CLabelApi : CTrelloApiBase
	{

		public CLabelApi(IOauthCollection oauth)
			: base(oauth)
		{

		}

		public LabelData GetLabel(string labelId)
		{
			var resource = string.Format("/labels/{0}", labelId);

			if (CheckArg())
				return null;

			var request = CreateRequest(resource);

			var client = CreateClient();
			var restResponse = client.Execute<LabelData>(request);

			return restResponse.Data;
		}

	}
}