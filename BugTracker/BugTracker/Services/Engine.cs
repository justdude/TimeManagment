using BugTracker.Services.Trello;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BugTracker.Services
{
	public class Engine
	{

		public IOauthCollection Oauth { get; set; }
		public CBoardsApi Board { get; private set; }
		public CCardsApi Cards { get; private set; }

		public static Engine Instance = null;


		static Engine()
		{
			Instance = new Engine();
		}

		private Engine()
		{
			Oauth = new IOauthCollection(string.Empty, Constants.Trello.Key);
			Board = new CBoardsApi(Oauth);
			Cards = new CCardsApi(Oauth);
		}


	}
}
