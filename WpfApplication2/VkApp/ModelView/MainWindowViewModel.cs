using MVVM;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace VkApp.ModelView
{
	public class MainWindowViewModel : ViewModelBase
	{
		private string avatar;

		public string UserFullName
		{
			get
			{
				return VkEngine.Instance.Account.Nickname;
			}
		}

		public string Avatar
		{
			get
			{
				return VkEngine.Instance.PhotoPath;
			}
		}

	}
}
