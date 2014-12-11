using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BugTracker.WebWindow
{
	public partial class AuthWindow : Form
	{
			public AuthWindow()
			{
				InitializeComponent();
			}

			public Action<string> DocumentCompleted { get; set; }
			public Action<string> Navigated { get; set; }

			public void Navigate(string urlString)
			{
				if (webBrowser1 == null)
					return;

				if (string.IsNullOrEmpty(urlString))
					return;

				webBrowser1.Navigate(urlString);
			}


			private void AuthWindow_Load(object sender, EventArgs e)
			{
				webBrowser1.Navigated += webBrowser1_Navigated;
				webBrowser1.DocumentCompleted += webBrowser1_DocumentCompleted;
			}


			void webBrowser1_DocumentCompleted(object sender, WebBrowserDocumentCompletedEventArgs e)
			{
				if (DocumentCompleted == null)
					return;

				DocumentCompleted(e.Url.ToString());
			}

			void webBrowser1_Navigated(object sender, WebBrowserNavigatedEventArgs e)
			{
				if (Navigated == null)
					return;

				Navigated(e.Url.ToString());
			}


		public string DocumentText
		{
			get
			{
				return webBrowser1.DocumentText;
			}
		}

		public string Parse(string startSub, string endSub)
		{
			int start = DocumentText.IndexOf(startSub) + startSub.Length;
			int finish = DocumentText.IndexOf(endSub);

			return DocumentText.Substring(start, finish - start);
		}

	}
}
