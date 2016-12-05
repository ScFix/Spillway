using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Data;

namespace Spillway.Utilities.Converters
{
	public class BoolToVisibility : IValueConverter
	{
		private bool _VisibleOnTrue = true;

		public bool VisibleOnTrue
		{
			get { return _VisibleOnTrue; }
			set { _VisibleOnTrue = value; }
		}



		public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
		{
			Boolean b = Boolean.Parse(value.ToString());
			if (b)
			{
				return VisibleOnTrue ? Visibility.Visible : Visibility.Hidden;
			}
			else
			{
				return VisibleOnTrue ? Visibility.Hidden : Visibility.Visible;
			}
		}

		public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
		{
			throw new NotImplementedException();
		}
	}
}
