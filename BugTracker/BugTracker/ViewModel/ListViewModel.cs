using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using GalaSoft.MvvmLight;

using BugTracker.Data;
using System.Collections.ObjectModel;
using BugTracker.Services;
using System.Windows.Input;
using GalaSoft.MvvmLight.Command;
using System.Threading;
using System.Linq;

namespace BugTracker.ViewModel
{
	public class ListViewModel: ExtendedViewModelBase
	{
		private bool mvIsVisibleAddButton;
		private RelayCommand OnSave;
		private RelayCommand OnAdd;
		private float mvSpentValue;
		private float mvEstimateValue;
		private string mvNameInput;
		private string mvDeskInput;
		private string mvDescInput;

		#region ListDataColl
		public ListData ListDataCollection { get; private set; }
		public ObservableCollection<CardViewModel> Tasks { get; private set; }
		#endregion 

		#region ListData

		public string Name
		{
			get
			{
				return ListDataCollection.Name;
			}
			set
			{
				if (ListDataCollection.Name == value)
					return;

				ListDataCollection.Name = value;

				base.RaisePropertyChanged("Name");
			}
		}

		public string Desc
		{
			get
			{
				return ListDataCollection.Desc;
			}
			set
			{
				if (ListDataCollection.Desc == value)
					return;

				ListDataCollection.Desc = value;

				base.RaisePropertyChanged("Desc");
			}
		}

		public string Id
		{
			get
			{
				return ListDataCollection.Id;
			}
		}

		public string IdList
		{
			get
			{
				return ListDataCollection.IdList;
			}
		}

		public string IdBoard
		{
			get
			{
				return ListDataCollection.IdBoard;
			}
		}
		#endregion 

		public ListViewModel(ListData listData)
		{
			ListDataCollection = listData;
		}

		#region Commands
		public ICommand Save
		{
			get
			{
				if (OnSave == null)
				{
					OnSave = new RelayCommand(OnSaveClick);
				}
				return OnSave;
			}
		}		

		public ICommand Add
		{
			get
			{
				if (OnAdd == null)
				{
					OnAdd = new RelayCommand(()=>
					{
						this.IsVisibleAddButton = !IsVisibleAddButton;
					});
				}
				return OnAdd;
			}
		}
		#endregion


		public bool IsVisibleAddButton
		{
			get
			{
				return mvIsVisibleAddButton;
			}
			set
			{
				if (mvIsVisibleAddButton == value)
					return;

				mvIsVisibleAddButton = value;

				base.RaisePropertyChanged("IsVisibleAddButton");
			}
		}

		public string NameInput
		{
			get
			{
				return mvNameInput;
			}
			set
			{
				if (mvNameInput == value)
					return;

				mvNameInput = value;

				base.RaisePropertyChanged("NameInput");
			}
		}
		public string DescInput
		{
			get
			{
				return mvDescInput;
			}
			set
			{
				if (mvDescInput == value)
					return;

				mvDescInput = value;

				base.RaisePropertyChanged("DescInput");
			}
		}

		private void OnSaveClick()
		{
			ThreadPool.QueueUserWorkItem(new WaitCallback((p) =>
			{
				Invoke(() => IsLoading = true);

				AddCard(NameInput, DescInput);
				var cards = Engine.Instance.Cards.GetCards(this.Id, this.IdBoard);
				
				NameInput = "";
				DescInput = "";

				Load();
				Invoke(() =>
				{
					//Tasks.Clear();
					//var items = (cards.Select<CardData, CardViewModel>(t => new CardViewModel(this, t)));
					//foreach (var item in items)
					//{
					//	Tasks.Add(item);
					//}
					IsLoading = false;
					IsVisibleAddButton = false;
				});
			}));
		}

		public void AddCard(string name, string desc)
		{
			Engine.Instance.Cards.CreateCard(Id, name, desc);
		}

		public void Load()
		{
			try
			{
				//foreach (var list in Lists)
				{
					Invoke(() =>
					{
						if (Tasks == null)
						{
							Tasks = new ObservableCollection<CardViewModel>();
						}

						Tasks.Clear();
					});


					var cards = Engine.Instance.Cards.GetCards(this.Id, this.IdBoard);
					foreach(var card in cards)
					{
						card.ProcessValues();
					}

					Invoke(() =>
					{
						foreach (var card in cards)
						{
							var newItem = new CardViewModel(this, card);
							Tasks.Add(newItem);
						}

					});
					
				}
			}
			catch (Exception ex)
			{

			}
		}

		private ObservableCollection<CardViewModel> GetTasks(IEnumerable<CardData> collection)
		{
			if (collection == null)
				return new ObservableCollection<CardViewModel>();

			return new ObservableCollection<CardViewModel>(collection.Select<CardData, CardViewModel>(p => new CardViewModel(this, p)));
		}




	}
}
