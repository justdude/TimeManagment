using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VKMusicSync.ModelView.Tabs;

namespace VKMusicSync.ModelView.Tabs
{
	public class MainMv : ViewModelBase
	{
		public ObservableCollection<TabModelView> Items { get; private set; }
		
		public MainMv()
		{
			Items = new ObservableCollection<TabModelView>();
			Items.Add(new TabModelView());
			Items.Add(new TabModelView());

			Items.Add(new TabModelView());
			Items.Add(new TabModelView());

		}

	}
}
