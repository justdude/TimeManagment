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
		private ListViewModel modParentList;

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
		public TaskViewModel(ListViewModel parentList, CardData task)
		{
			Task = task;
			modParentList = parentList;
		}
		#endregion

	}
}
