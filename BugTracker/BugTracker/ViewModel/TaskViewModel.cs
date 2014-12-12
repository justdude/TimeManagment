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
	public class TaskViewModel: ExtendedViewModelBase
	{

		private float mvEstimateValue = 0;
		private float mvSpentValue = 0;
		private bool mvIsVisibleAddButton = false;

		private RelayCommand OnSave;
		private ICommand OnAdd;

		public CardData Task { get; private set; }

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

		public ICommand Add
		{
			get
			{
				if (OnAdd == null)
				{
					OnAdd = new RelayCommand(this.OnSaveClick);
				}
				return OnAdd;
			}
		}

		
		public ICommand Save
		{
			get
			{
				if (OnSave == null)
				{
					OnSave = new RelayCommand(() => { IsLoading = false; IsVisibleAddButton = true; });
				}
				return OnSave;
			}
		}

		private void OnSaveClick()
		{
			//FloatToStringConverter
			ThreadPool.QueueUserWorkItem( new WaitCallback((p)=>
				{ 
					Invoke(()=>IsLoading = true);
					PutData(SpentValue, EstimateValue);
					Invoke(() => IsLoading = false);
				}));
		}

		public float SpentValue
		{
			get
			{
				return mvSpentValue;
			}
			set
			{
				if (value == mvSpentValue)
					return;

				mvSpentValue = value;

				base.RaisePropertyChanged("SpentValue");
			}
		}
		public float EstimateValue
		{
			get
			{
				return mvEstimateValue;
			}
			set
			{
				if (value == mvEstimateValue)
					return;
				
				mvEstimateValue = value;

				base.RaisePropertyChanged("EstimateValue");
			}
		}
		public float RemainingValue
		{
			get
			{
				return SpentValue - EstimateValue;
			}
		}



		public void PutData(float spent, float estimate)
		{
			var text = Handlers.WordsParser.ToString(spent, estimate);
			Engine.Instance.Cards.CreateCard(IdList, DateTime.Now.ToString(), text);
		}

		public TaskViewModel(CardData task)
		{
			Task = task;
		}

	}
}
