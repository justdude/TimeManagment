using BugTracker.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using BugTracker.Constants;
namespace BugTracker.Converters
{
	public class ColorToStateTextConverter :IValueConverter
	{

		#region IValueConverter Members

		public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
		{
			LabelData.ColorTypes color = (LabelData.ColorTypes)value;

			switch(color)
			{
				case LabelData.ColorTypes.blue:
					return Global.ClosedStateText;
				case LabelData.ColorTypes.green:
					return Global.OpenStateText;
				case LabelData.ColorTypes.orange:
					return Global.FixedStateText;
				case  LabelData.ColorTypes.red:
					return Global.ReopenedStateText;
				default:
					return string.Empty;
			}
		}

		public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
		{
			string text = (string)value;

			switch (text)
			{
				case Global.ClosedStateText:
					return LabelData.ColorTypes.blue;
				case Global.OpenStateText:
					return LabelData.ColorTypes.green;
				case Global.FixedStateText:
					return LabelData.ColorTypes.orange;
				case Global.ReopenedStateText:
					return LabelData.ColorTypes.red;
				default:
					return LabelData.ColorTypes.blue;
			}
		}

		#endregion
	}
}
