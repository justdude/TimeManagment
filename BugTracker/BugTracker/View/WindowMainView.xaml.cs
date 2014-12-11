using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Windows.Threading;

namespace BugTracker.View
{
	/// <summary>
	/// Interaction logic for WindowMainView.xaml
	/// </summary>
	public partial class WindowMainView : Window
	{
		public static Dispatcher WindowDispatcher;

		public WindowMainView()
		{
			InitializeComponent();
			WindowDispatcher = Dispatcher.CurrentDispatcher;
		}
	}
}
