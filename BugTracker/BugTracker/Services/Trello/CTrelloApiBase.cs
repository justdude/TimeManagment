	using System;
	using System.Collections.Generic;
	using System.Diagnostics;
	using System.Linq;
	using System.Net;
	using System.Threading.Tasks;

	using RestSharp;
	using BugTracker.Data;

	namespace BugTracker.Services.Trello
	{

	public class IOauthCollection
	{
		public string Token { get; set; }
		public string Key { get; set; }
		public IOauthCollection(string token, string key)
		{
			Token = token;
			Key = key;
		}
	}

	public class CTrelloApiBase
	{
		IOauthCollection mvIOauthCollection;

		public string Token { get { return mvIOauthCollection.Token; } }
			public string Key { get{ return mvIOauthCollection.Key;} }

		protected CTrelloApiBase(IOauthCollection oauth)
		{
			mvIOauthCollection = oauth;
		}

		protected CTrelloApiBase(string key, string token)
		{
			mvIOauthCollection = new IOauthCollection(token, key);
		}

		public bool IsTokenExpired()
		{
			if (CheckArg())
				return false;

			var resource = string.Format("/tokens/{0}", Token);

			var request = CreateRequest(Token, resource);

			var client = CreateClient();
			RestResponse<TrelloToken> response = new RestResponse<TrelloToken>(); //SPIKE??????!!!!!!!!!
			try
			{
				client.Execute<TrelloToken>(request);
			}
			catch (Exception)
			{
				return true;
			}
			return IsTokenExpired(response);
		}

			protected RestRequest CreateRequest(string userToken, string resource)
			{
				return CreateRequest(Key, userToken, resource);
			}

			protected bool CheckArg()
			{
 				return string.IsNullOrEmpty(Key);
			}

			protected static bool IsTokenExpired(RestResponse<TrelloToken> response)
			{
				if (response.StatusCode == HttpStatusCode.Unauthorized)
					return true;

				var trelloToken = response.Data;
				return trelloToken != null && trelloToken.DateExpires != null && trelloToken.DateExpires < DateTime.Today;
			}

			protected RestRequest CreateRequest(string resource)
			{
				return CreateRequest(Token, resource);
			}
			
			protected static RestClient CreateClient()
			{
				var restClient = new RestClient(Constants.Trello.ServiceBaseUrl);
				return restClient;
			}

			protected static RestRequest CreateRequest(string key, string userToken, string resource)
			{
				var request = new RestRequest
				{
						Resource = resource,
						RequestFormat = DataFormat.Json,
				};
				request.AddParameter("key", key); //Testing
				request.AddParameter("token", userToken);
				request.AddParameter("r", DateTime.Now.Ticks); //To make sure we don't get cached results
				return request;
			}

		
	}
}