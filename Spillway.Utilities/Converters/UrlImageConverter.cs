using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Media.Imaging;


namespace Spillway.Utilities.Converters
{
	public class UrlImageConverter : IValueConverter
	{
		// this is a shallow cache for the images that are loaded via this item

		public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
		{


			string url = value?.ToString();
			if (url != null)
			{
				return ImageCache.GetImage(url);
			}
			return null;
		}

		public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
		{
			throw new NotImplementedException();
		}
	}
}
