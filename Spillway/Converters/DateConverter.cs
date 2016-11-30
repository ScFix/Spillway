using Spillway.Utilities;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace Spillway.Converters
{
    public class DateConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            long val;
            if (long.TryParse(value.ToString(), out val))
            {
                var date = DateUtil.FromUnixTime(val);
                return date.ToShortTimeString();
            }
            return "";

        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            DateTime d;
            if (DateTime.TryParse(value.ToString(), out d))
            {
                return d.ToOADate();
            }
            return null;
        }
    }
}
