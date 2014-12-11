using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using TrelloNet;

namespace BugTracker.Handlers
{
	public class CTrelloAdapter
	{
		public ITrello Trello { get; private set; }
		public Member Me
		{
			get
			{
				if (mvMe == null)
				{
					mvMe = Trello.Members.Me();
				}
				return mvMe;
			}
			private set
			{
				mvMe = value;
			}
		}

		public CTrelloAdapter()
		{
			Trello = new Trello(Constants.Trello.Key);

		}


		private string GetAuthorizationUrl(Scope scope = Scope.ReadWriteAccount, 
			Expiration exp = Expiration.Never)
		{
			Uri url = Trello.GetAuthorizationUrl(Constants.Global.AppName, scope, exp);
			WebRequest request = WebRequest.CreateHttp(url);
			return request.ToString();
		}

		public void Authorize(string token)
		{
			Trello.Authorize(token);
		}

		Member MyAccount;
		private Member mvMe;

		public IList<Board> GetBoardsForMe()
		{
			List<Board> allMyBoards = Trello.Boards.ForMe(BoardFilter.All).ToList();
			return allMyBoards;
		}

		public IEnumerable<TrelloNet.List> GetLists(Board board, ListFilter filter = ListFilter.All)
		{
			return Trello.Lists.ForBoard(board, filter);
		}

		public void DoStuff()
		{
			var boards = Trello.Boards;

			IEnumerable<Board> allBoardsOfTrelloAppsOrg = Trello.Boards.ForMe();

			var member = Trello.Members.Me();

			var boards2 = Trello.Boards.ForMe();
			var orgs = Trello.Organizations.ForMe();
			foreach (var item in orgs)
			{
				var cachedBoard = new NewBoard("!!!!");
				cachedBoard.IdOrganization = item.Id;
				Trello.Boards.Add(cachedBoard);

				var boardstest = Trello.Boards.ForOrganization(item);
			}
			

			//boards.Add(new NewBoard("ssssssssss") { Desc = "Board" });

			var myBoard = Trello.Boards.Add("My Board");
			var board2 = new NewBoard("ssssssssss") { Desc = "Board2" };

			Trello.Lists.Add("Doing", myBoard);
			Trello.Lists.Add("Done", myBoard);

			//Trello.CCardsApi.Add("My card", todoList);

			foreach (var list in Trello.Lists.ForBoard(myBoard))
			{

			}
		}

	}
}
