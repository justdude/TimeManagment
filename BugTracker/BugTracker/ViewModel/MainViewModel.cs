using System.Collections.ObjectModel;
using System.Windows.Input;
using System.Threading.Tasks;
using System;
using System.Windows.Forms;
using System.Collections.Specialized;
using System.Collections.Generic;
using System.Linq;

using GalaSoft.MvvmLight;


using BugTracker.Services;
using BugTracker.Data;
using System.Threading;
using BugTracker.Handlers;


namespace BugTracker.ViewModel
{
	public class MainViewModel : ExtendedViewModelBase
	{
		#region Fields

		private WebWindow.AuthWindow window;
		private ManualResetEvent resetEvent = new ManualResetEvent(false);

		#endregion

		#region Properties

		public ObservableCollection<ListViewModel> Lists 
		{
			get
			{
				if (mvLists == null)
				{
					Load();
				}
				return mvLists;
			}
			private set
			{
				mvLists = value;
			}
		}
		public static CTrelloAdapter TrelloAdapter { get; private set; }

		public ICommand Update;
		private ObservableCollection<ListViewModel> mvLists;

		#endregion

		#region Ctor
		public MainViewModel()
		{
			if (IsInDesignMode)
				return;

			
			window = new WebWindow.AuthWindow();
			Login();
			
			TrelloAdapter = new CTrelloAdapter();
			TrelloAdapter.Authorize(Engine.Instance.Oauth.Token);
			resetEvent.Set();
			//Lists = new ObservableCollection<ListViewModel>();
			//ThreadPool.QueueUserWorkItem(new WaitCallback(p => Load()));
			//ListDataCollection.Factory.StartNew(() => { });
		}
		#endregion


		private void Login()
		{
			//ListDataCollection task = new ListDataCollection(() =>
			//{

			Invoke(() => { IsLoading = true; });

			System.Threading.Thread.Sleep(1000);

			try
			{
				//Uri url = TrelloAdapter.Trello.GetAuthorizationUrl(Constants.Global.AppName, TrelloNet.Scope.ReadWriteAccount, TrelloNet.Expiration.Never);

				var del = (MethodInvoker)delegate()
				{
					window.Navigate(Constants.Trello.AuthorizationUrl); //url.AbsoluteUri);
					window.ShowDialog();
				};

				del.Invoke();

				var token = window.Parse(@"<pre>", @"</pre>").Trim();
				Engine.Instance.Oauth.Token = token;
			}
			catch (Exception ex)
			{

			}

			Invoke(() => { IsLoading = false; });

			//});

			//return task;
		}

		public void Load()
		{
			//while (resetEvent.WaitOne(100))
			//{ }

			Invoke(() => { IsLoading = true; });

			try
			{
				const string BoardName = "TestForDevBoard";
				const string BoardDisc = "Boards for test For Dev";

				BoardData mainBoard = GetBoard(BoardName);
				mainBoard = CreateAndGetIfNull(BoardName, BoardDisc, mainBoard);

				var lists = Engine.Instance.Board.GetLists1(mainBoard.Id);

				if (lists == null)
					return;

				Invoke(() =>
				{
					Lists = new ObservableCollection<ListViewModel>(GetLists(lists));
				});

				Thread.Sleep(1000 * 1);

				foreach (var item in Lists)
				{
					item.Load();
				}
			}
			catch (Exception ex)
			{
			}

			Invoke(() => { IsLoading = false; });
			//return task;
		}

		private static BoardData CreateAndGetIfNull(string BoardName, string BoardDisc, BoardData mainBoard)
		{
			if (mainBoard == null)
			{
				var res = Engine.Instance.Board.CreateBoard(BoardName, BoardDisc);
				if (string.IsNullOrEmpty(res))
					return null;//RAISE ERROR

				mainBoard = GetBoard(BoardName);
			}
			return mainBoard;
		}

		private static BoardData GetBoard(string BoardName)
		{
			IList<BoardData> openBoards = Engine.Instance.
						  Board.GetBoards(Services.Trello.CBoardsApi.Filter.Open);
			BoardData mainBoard = openBoards.FirstOrDefault(p => p.Name == BoardName);
			return mainBoard;
		}


		public static IEnumerable<BoardViewModel> GetBoards(IList<BoardData> collection)
		{
			if (collection == null)
				return null;

			return collection.Select<BoardData, BoardViewModel>(p => new BoardViewModel(p));
		}

		public static IEnumerable<ListViewModel> GetLists(IList<ListData> collection)
		{
			if (collection == null)
				return null;

			return collection.Select<ListData, ListViewModel>(p => new ListViewModel(p));
		}


		#region Events
		public void OnLoad()
		{
			
			//task2.Start();
		}


		void ClosedBoards_CollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
		{
			switch (e.Action)
			{
				case NotifyCollectionChangedAction.Add:
					break;
			}
		}

		void OpenedBoards_CollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
		{
			switch (e.Action)
			{
				case NotifyCollectionChangedAction.Add:
					break;
			}
		}

		void Tasks_CollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
		{
			//var tasks = (sender as ObservableCollection<TaskViewModel>());

			//if (tasks == null)
			//	return;

			switch (e.Action)
			{
				case NotifyCollectionChangedAction.Add:

					break;
			}
		}
		#endregion


		

	}
}