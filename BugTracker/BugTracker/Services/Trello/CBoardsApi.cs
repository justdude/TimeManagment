using BugTracker.Data;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BugTracker.Services.Trello
{
	public class CBoardsApi : CTrelloApiBase
	{
		public enum Filter
		{
			Closed,
			Members,
			Open,
			Organization,
			Pinned,
			Public,
			Starred,
			Unpinned
		}

		public CBoardsApi(IOauthCollection oauth)
			: base(oauth)
		{

		}

		public string CreateBoard(string boardName, string boardDescription)
		{
			if (CheckArg())
				return string.Empty;

			var client = CreateClient();
			var createRequest = CreateRequest("/boards");
			createRequest.Method = Method.POST;
			createRequest.AddParameter("name", boardName);
			createRequest.AddParameter("desc", boardDescription);

			var taskBoardResponse = client.Execute<BoardData>(createRequest);
			var boardId = taskBoardResponse.Data.Id;

			//Not currently supported
			//var visibilityRequest = CreateRequest(trelloSettings, string.Format("/boards/{0}/prefs", boardId));
			//visibilityRequest.AddParameter("permissionLevel", visibility.ToString().ToLower());
			//await ExecuteAwaitableAsync(client, visibilityRequest);

			//CreateList(client, boardId, "To Do");
			//CreateList(client, boardId, "Doing");
			//CreateList(client, boardId, "Done");
			return boardId;
		}

		public void CreateList(string boardId, string name)
		{

			if (CheckArg())
				return;

			var client = CreateClient();
			CreateList(client, boardId, name);
		}

		private void CreateList(IRestClient client, string boardId, string name)
		{
			var createRequest = CreateRequest(string.Format("/boards/{0}/lists", boardId));

			createRequest.Method = Method.POST;
			createRequest.AddParameter("name", name);

			client.Execute(createRequest);
		}
		public List<ListData> GetLists1(string boardId)
		{
			var resource = string.Format("/boards/{0}/lists", boardId);

			if (CheckArg())
				return null;

			var request = CreateDumpRequest(resource);
			request.AddParameter("filter ", "all");//enum
			CBoardsApi.AddTokenToParam(Key, Token, request);
			var client = CreateClient();
			var url = client.BuildUri(request);
			var restResponse = client.Execute<List<ListData>>(request);
			return restResponse.Data;
		}

		public void UpdateList(ListData boardList)
		{

			if (CheckArg())
				return;

			var client = CreateClient();
			var request = CreateRequest(Token, string.Format("/lists/{0}", boardList.Id));

			request.AddParameter("name", boardList.Name);
			request.Method = Method.PUT;

			client.Execute(request);
		}
		public BoardData GetBoard(string boardId)
		{
			var resource = string.Format("/boards/{0}", boardId);

			if (CheckArg())
				return null;

			var request = CreateRequest(Token, resource);
			var client = CreateClient();
			var executeAwaitableAsync = client.Execute<BoardData>(request);
			return executeAwaitableAsync.Data;
		}

		public void DeleteBoard(BoardData taskBoard)
		{
			throw new NotSupportedException();
		}

		public List<BoardData> GetBoards(Filter filter = Filter.Open)
		{
			const string resource = "/members/me/boards";

			if (CheckArg())
				return new List<BoardData>();

			var filterStr = filter.ToString().ToLower();

			var request = CreateRequest(resource);
			request.AddParameter("filter", filterStr);
			request.AddParameter("card_fields", "idList");

			var client = CreateClient();
			var restResponse = client.Execute<List<BoardData>>(request);

			Debug.WriteLine(restResponse.Content);
			return restResponse.Data;
		}

		public IEnumerable<CardData> GetCards(string boardId, Filter filter = Filter.Open)
		{
			string resource = String.Format("/boards/{0}/cards", boardId);

			if (CheckArg())
				return new List<CardData>();

			var filterStr = filter.ToString().ToLower();

			var request = CreateRequest(resource);
			request.AddParameter("filter", filterStr);

			var client = CreateClient();
			var restResponse = client.Execute<List<CardData>>(request);

			Debug.WriteLine(restResponse.Content);
			return restResponse.Data;
		}

		public IEnumerable<ListData> GetLists(string boardId)
		{
			var resource = string.Format("/boards/{0}/lists/all", boardId);

			if (CheckArg())
				return Enumerable.Empty<ListData>();

			var request = CreateRequest(resource);

			var client = CreateClient();
			var restResponse = client.Execute<List<ListData>>(request);
			return restResponse.Data.Select(s =>
			{
				s.IdBoard = boardId;
				return s;
			});
		}

	}
}
