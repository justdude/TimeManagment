using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using GalaSoft.MvvmLight;

using BugTracker.Data;
using System.Windows.Input;
using BugTracker.Services;
using GalaSoft.MvvmLight.CommandWpf;
using System.Threading;

namespace BugTracker.ViewModel
{
	public class CardViewModel: ExtendedViewModelBase
	{

		private float mvEstimateValue = 0;
		private float mvSpentValue = 0;
		private bool mvIsVisibleAddButton = false;

		private RelayCommand OnSave;
		private RelayCommand OnAdd;
		private ListViewModel modParentList;
		private LogTimeBase modLogTime;
		public CardData Task { get; private set; }

		#region CardFields data
		public string Name
		{
			get
			{
				return Task.Name;
			}
			set
			{
				if (Task.Name == value)
					return;

				Task.Name = value;

				base.RaisePropertyChanged("Name");
			}
		}

		public string Desc
		{
			get
			{
				return Task.Desc;
			}
			set
			{
				if (Task.Desc == value)
					return;

				Task.Desc = value;

				base.RaisePropertyChanged("Desc");
			}
		}

		public string Id
		{
			get
			{
				return Task.Id;
			}
		}

		public string IdList
		{
			get
			{
				return Task.IdList;
			}
		}

		public string IdBoard
		{
			get
			{
				return Task.IdBoard;
			}
		}

		#endregion

		#region computed data
		public float SpentValue
		{
			get
			{
				return Task.SpentValue;
			}
			set
			{
				if (value == Task.SpentValue)
					return;

				Task.SpentValue = value;

				base.RaisePropertyChanged("SpentValue");
			}
		}
		public float EstimateValue
		{
			get
			{
				return Task.EstimateValue;
			}
			set
			{
				if (value == Task.EstimateValue)
					return;

				Task.EstimateValue = value;

				base.RaisePropertyChanged("EstimateValue");
			}
		}
		public float Remaining
		{
			get
			{
				return Task.Remaining;
			}
		}
		#endregion

		#region Ctr
		public CardViewModel(ListViewModel parentList, CardData task)
		{
			Task = task;
			modParentList = parentList;
			modLogTime = new LogTimeBase();
		}
		#endregion

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
					OnAdd = new RelayCommand(() =>
					{
						this.IsVisibleAddButton = !IsVisibleAddButton;
					});
				}
				return OnAdd;
			}
		}
		#endregion

		#region Properties
		public float SpentValueInput
		{
			get
			{
				return modLogTime.SpentValue;
			}
			set
			{
				if (value == modLogTime.SpentValue)
					return;

				modLogTime.SpentValue = value;

				base.RaisePropertyChanged("SpentValueInput");
			}
		}
		public float EstimateValueInput
		{
			get
			{
				return modLogTime.EstimateValue;
			}
			set
			{
				if (value == modLogTime.EstimateValue)
					return;

				modLogTime.EstimateValue = value;

				base.RaisePropertyChanged("EstimateValueInput");
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

				mvIsVisibleAddButton = value;

				base.RaisePropertyChanged("IsVisibleAddButton");
			}
		}

		public string TotalEstimate
		{
			get
			{
				return Task.EstimateValue.ToString();
			}
		}
		public string TotalSpent
		{
			get
			{
				return Task.SpentValue.ToString();
			}
		}

		public string TotalRemaining
		{
			get
			{
				return Task.Remaining.ToString();
			}
		}
		#endregion

		private void OnSaveClick()
		{
			ThreadPool.QueueUserWorkItem(new WaitCallback((p) =>
			{
				Invoke(() => IsLoading = true);

				var str = modLogTime.ToString();
				AddComment(str);

				Load(Id);

				Invoke(() =>
				{
					base.RaisePropertyChanged("TotalSpent");
					base.RaisePropertyChanged("TotalEstimate");
					base.RaisePropertyChanged("TotalRemaining");

					IsLoading = false;
					IsVisibleAddButton = false;
				});
			}));
		}

		public void Load(string Id)
		{
			Task = Engine.Instance.Cards.GetCard(Id);
			Task.ProcessValues();
		}

		private void AddComment(string comment)
		{
			//var test = Engine.Instance.Cards.GetActions("commentCard");
			Engine.Instance.Cards.AddComment(Id, comment);
		}

	}
}
