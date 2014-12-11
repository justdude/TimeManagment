using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using GalaSoft.MvvmLight;

using BugTracker.Data;

namespace BugTracker.ViewModel
{
	public class TaskViewModel: ViewModelBase
	{
		public TaskCard Task { get; private set; }


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

		public TaskViewModel(TaskCard task)
		{
			task = Task;
		}

	}
}
