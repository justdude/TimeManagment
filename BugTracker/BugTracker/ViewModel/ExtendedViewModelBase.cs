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
	public class ExtendedViewModelBase :ViewModelBase
	{
		private bool mvIsLoading;
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