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

namespace BugTracker.ViewModel
{
	public class ListViewModel: ExtendedViewModelBase
	{
		private bool mvIsVisibleAddButton;
		private RelayCommand OnSave;
		private RelayCommand OnAdd;
		private float mvSpentValue;
		private float mvEstimateValue;

		#region ListDataColl
		public ListData ListDataCollection { get; private set; }
		public ObservableCollection<TaskViewModel> Tasks { get; private set; }
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

		private CardData modCachedTask = new CardData();

		public float SpentValue
		{
			get
			{
				return modCachedTask.SpentValue;
			}
			set
			{
				if (value == modCachedTask.SpentValue)
					return;

				modCachedTask.SpentValue = value;

				base.RaisePropertyChanged("SpentValue");
			}
		}
		public float EstimateValue
		{
			get
			{
				return modCachedTask.EstimateValue;
			}
			set
			{
				if (value == modCachedTask.EstimateValue)
					return;

				modCachedTask.EstimateValue = value;

				base.RaisePropertyChanged("EstimateValue");
			}
		}
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

				switch (mvIsVisibleAddButton)
				{
					case (true):
						modCachedTask = new CardData(0, 0);
						break;
					case (false):
						modCachedTask = null;
						break;
				}

				mvIsVisibleAddButton = value;

				base.RaisePropertyChanged("IsVisibleAddButton");
			}
		}

		private void OnSaveClick()
		{
			//FloatToStringConverter
			ThreadPool.QueueUserWorkItem(new WaitCallback((p) =>
			{
				Invoke(() => IsLoading = true);

				AddCard(SpentValue, EstimateValue);
				Load();

				Invoke(() =>
				{
					IsLoading = false;
					IsVisibleAddButton = false;
				});
			}));
		}

		public void AddCard(float spent, float estimate)
		{
			Engine.Instance.Cards.CreateCard(IdList, 
				DateTime.Now.ToString(), modCachedTask.ToString());
		}

		public void Load()
		{
			try
			{
				//foreach (var list in Lists)
				{
					var cards = Engine.Instance.Cards.GetCards(this.Id, this.IdBoard);
					Invoke( ()=> Tasks = GetTasks(cards));
				}
			}
			catch (Exception ex)
			{

			}
		}

		private ObservableCollection<TaskViewModel> GetTasks(IEnumerable<CardData> collection)
		{
			if (collection == null)
				return new ObservableCollection<TaskViewModel>();

			return new ObservableCollection<TaskViewModel>(collection.Select<CardData, TaskViewModel>(p => new TaskViewModel(this, p)));
		}




	}
}
