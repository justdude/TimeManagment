using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Logic;
using VkNet;
using System.Threading.Tasks;
using VkNet.Model;
namespace VkApp.ModelView
{
	public class VkEngine
	{
		private static VkEngine mvInstance;
		public static VkApi API = new VkApi();
		private string mvPhotoPath;
		private User mvAccount;


		private object modLock = new object();
		public User Account
		{
			get
			{
				lock (modLock)
				{
					if (mvAccount == null)
					{
						Task t = new Task(() =>
						{
							IsLoading = true;

							mvAccount = API.Users.Get((long)API.UserId);

							IsLoading = false;
						});
						t.Start();
						//t.Wait();
					}
				}
				return mvAccount;
			}
		}

		public string PhotoPath
		{
			get
			{
				if (string.IsNullOrEmpty(mvPhotoPath))
				{
					mvPhotoPath = Account.PhotoPreviews.Photo200;
				}
				return mvPhotoPath;
			}
		}

		public bool IsLoading
		{
			get;
			private set;
		}

		public static VkEngine Instance
		{
			get
			{
				if (mvInstance == null)
				{
					mvInstance = new VkEngine();
				}
				return mvInstance;
			}
		}

		public bool IsLogged
		{
			get
			{
				return string.IsNullOrEmpty(API.AccessToken) == false;
			}
		}

		private VkEngine()
		{ }

		public Task Login(string login, string password)
		{
			var setting = VkNet.Enums.Filters.Settings.All;

			Task task = new Task(()=>
			API.Authorize(Constants.appIDINT, login, password, setting));
			return task;
		}

	



	}
}
