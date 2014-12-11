using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MVVM;
using System.Windows.Input;
using System.Windows;

namespace VkApp.ModelView
{
	public class LoginViewModel: ViewModelBase
	{
		private string mvLogin = string.Empty;
		private string mvPassword = string.Empty;
		private DelegateCommand mvAuthorize;

		public string Login
		{
			get
			{
				return mvLogin;
			}
			set
			{
				if (mvLogin == value)
					return;
				mvLogin = value;

				OnPropertyChanged("Login");
			}
		}

		public string Password
		{
			get
			{
				return mvPassword;
			}
			set
			{
				if (mvPassword == value)
					return;

				mvPassword = value;

				OnPropertyChanged("Password");
			}
		}

		
		public ICommand Authorize
		{
			get
			{
				return mvAuthorize;
			}
		}

		public LoginViewModel()
		{
			mvAuthorize = new DelegateCommand(OnAuthorize, () => { return !IsBusy && Login.Length > 0; });
			Login = "asres@km.ru";
			IsBusy = false;
		}

		private void OnAuthorize()
		{
			IsBusy = true;

			VkEngine.API = new VkNet.VkApi();
			VkEngine.Instance.Login(Login, Password).RunSynchronously();
			
			IsBusy = false;

			if (!VkEngine.Instance.IsLogged)
				return;


			var cachedWindow = Application.Current.MainWindow;
			MainWindow window = new MainWindow();
			window.Owner = cachedWindow;
			window.Show();
			cachedWindow.Visibility = Visibility.Hidden;
		}



	}
}
