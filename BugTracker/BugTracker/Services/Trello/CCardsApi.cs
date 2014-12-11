using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using BugTracker.Data;
using RestSharp;

namespace BugTracker.Services.Trello
{
	public class CCardsApi : CTrelloApiBase
	{

		public CCardsApi(IOauthCollection oauth):base(oauth)
		{

		}

		public TaskCard GetCard(string cardId)
		{
			var resource = string.Format("/cards/{0}", cardId);

			if (CheckArg())
				return null;

			var request = CreateRequest(resource);
			var client = CreateClient();
			var restResponse = client.Execute<TaskCard>(request);

			return restResponse.Data;
		}


		public void UpdateCard(string cardId, string name = null, string description = null, string listId = null)
		{

			if (CheckArg())
				return;

			var client = CreateClient();
			if (name != null)
				UpdateCardField(client, cardId, "name", name);
			if (description != null)
				UpdateCardField(client, cardId, "desc", description);
			if (listId != null)
				UpdateCardField(client, cardId, "idList", listId);
		}

		public void UpdateCardField(IRestClient client, string cardId, string name, string value)
		{
			var request = CreateRequest(Token, string.Format("/cards/{0}/{1}", cardId, name));

			request.Method = Method.PUT;
			request.AddParameter("value", value);

			client.Execute(request);
		}


		public void CloseCard(string id)
		{
			var resource = string.Format("/cards/{0}/closed", id);

			if (CheckArg())
				return;

			var request = CreateRequest(resource);

			request.AddParameter("value", "true");
			request.Method = Method.PUT;

			var client = CreateClient();
			client.Execute(request);
		}

		public void CreateCard(TaskCard card)
		{

			if (CheckArg())
				return;

			var resource = string.Format("/lists/{0}/cards", card.IdList);

			var request = CreateRequest(Token, resource);
			request.Method = Method.POST;
			request.AddParameter("name", card.Name);
			request.AddParameter("desc", card.Desc);

			var client = CreateClient();
			client.Execute(request);
		}

		public IEnumerable<TaskCard> GetCards(string listId, string boardId)
		{
			var resource = string.Format("/lists/{0}/cards/open", listId);

			if (CheckArg())
				return null;

			var request = CreateRequest(Token, resource);

			var client = CreateClient();
			var response = client.Execute<List<TaskCard>>(request);
		
			return response.Data.Select(c =>
			{
				c.IdList = listId;
				c.IdBoard = boardId;
				return c;
			});
		}

	}
}
