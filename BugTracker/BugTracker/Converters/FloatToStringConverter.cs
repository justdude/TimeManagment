using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace BugTracker.Converters
{
	public class FloatToStringConverter: IValueConverter
	{


		public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
		{
			return ((float)value).ToString();
		}

		public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
		{
			float val = 0;

			var res = float.TryParse(value as string, out val);
			
			return val;
		}
	}
}
