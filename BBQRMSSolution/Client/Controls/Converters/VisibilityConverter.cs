using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Data;

namespace Controls.Converters
{
    public class VisibilityConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            bool flag = false;
            if (value is string)
            {
                flag = StringToBoolean((string)value);
            }
            else if (value is bool)
            {
                flag = (bool)value;
            }
            else if (value is bool?)
            {
                bool? nullable = (bool?)value;
                flag = nullable.HasValue ? nullable.Value : false;
            }
            else if (value is Visibility)
            {
                flag = (Visibility)value != Visibility.Visible;
            }
            else if (value != null)
            {
                flag = true;
            }

            if (parameter != null)
            {
                flag = !flag;
            }

            return (flag ? Visibility.Visible : Visibility.Collapsed);
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return ((value is Visibility) && (((Visibility)value) == Visibility.Visible));
        }

        private static bool StringToBoolean(string str)
        {
            if (str != null && !str.Equals("") && !str.ToUpper().Equals("FALSE"))
            {
                return true;
            }
            if (str != null && str.ToUpper().Equals("FALSE"))
            {
                return false;
            }
            return false;
        }
    }
}
