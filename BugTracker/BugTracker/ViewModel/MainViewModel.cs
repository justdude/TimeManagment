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


namespace BugTracker.ViewModel
{
    public class MainViewModel : ViewModelBase
		{
			#region Fields

			private bool mvIsLoading;
			private WebWindow.AuthWindow window;


			#endregion

			#region Properties

			public bool IsLoading
			{
				get
				{
					return mvIsLoading;
				}
				set
				{
					if (mvIsLoading == value)
						return;

					mvIsLoading = value;

					base.RaisePropertyChanged("IsLoading");
				}
			}


			public ObservableCollection<TaskViewModel> Tasks { get; private set; }
			public ObservableCollection<BoardViewModel> Boards { get; private set; }

			public ICommand Update;

			#endregion

			#region Ctor
			public MainViewModel()
      {
				Init();
				Task.Factory.StartNew(() => { OnLoad(); });
      }
			#endregion

			private void Init()
			{
				//CTrelloAdapter trelloAdapter = new CTrelloAdapter();
				window = new WebWindow.AuthWindow();

				Tasks = new ObservableCollection<TaskViewModel>();
				//Tasks.CollectionChanged += Tasks_CollectionChanged;

				Login();
			}

			private void Login()
			{
				//Task task = new Task(() =>
				//{

					Invoke(() => { IsLoading = true; });

					System.Threading.Thread.Sleep(1000);

					try
					{
						//Uri url = trelloAdapter.Trello.GetAuthorizationUrl(Constants.Global.AppName, TrelloNet.Scope.ReadWriteAccount, TrelloNet.Expiration.Never);

						var del = (MethodInvoker)delegate()
						{
							window.Navigate(Constants.Trello.AuthorizationUrl); //url.AbsoluteUri);
							window.ShowDialog();
						};

						del.Invoke();

						var token = window.Parse(@"<pre>", @"</pre>").Trim();
						Engine.Instance.Oauth.Token = token;


						//trelloAdapter.Trello.Authorize(token);
					}
					catch (Exception ex)
					{

					}

					Invoke(() => { IsLoading = false; });

				//});

				//return task;
			}

			private void Fill()
			{
					Invoke (() => { IsLoading = true; });

					try
					{
						IList<TaskBoard> openBoards = Engine.Instance.
							Board.GetBoards(Services.Trello.CBoardsApi.Filter.Open);
						IList<TaskBoard> closedBoards = (Engine.Instance.
							Board.GetBoards(Services.Trello.CBoardsApi.Filter.Closed));

						

						Invoke(() =>
						{
							Boards = new ObservableCollection<BoardViewModel>(GetBoards(openBoards));
							
							foreach (var item in GetBoards(closedBoards))
								Boards.Add(item);
						});

						//Tasks = new ObservableCollection<TaskViewModel>();

					}
					catch (Exception ex)
					{

					}

					Invoke(() => { IsLoading = false; });

				

				//return task;
			}


			public static IEnumerable<BoardViewModel> GetBoards(IList<TaskBoard> collection) 
			{
				return collection.Select<TaskBoard, BoardViewModel>(p => new BoardViewModel(p));
			}

			#region Events
			public void OnLoad()
			{
				ThreadPool.QueueUserWorkItem(new WaitCallback(p=>Fill()));
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


			public static void BeginInvoke(Action act)
			{
				System.Windows.Threading.Dispatcher.CurrentDispatcher.BeginInvoke(act, System.Windows.Threading.DispatcherPriority.DataBind);
			}

			public static void Invoke(Action act)
			{
				if (BugTracker.View.WindowMainView.WindowDispatcher == null)
					System.Windows.Threading.Dispatcher.CurrentDispatcher.Invoke(
						act, System.Windows.Threading.DispatcherPriority.DataBind);
				else
					BugTracker.View.WindowMainView.WindowDispatcher.Invoke(
						act, System.Windows.Threading.DispatcherPriority.DataBind);
			}

			public static void InvokeAsync(Action act)
			{
				System.Windows.Threading.Dispatcher.CurrentDispatcher.InvokeAsync(act, System.Windows.Threading.DispatcherPriority.DataBind);
			}
			
		}
}