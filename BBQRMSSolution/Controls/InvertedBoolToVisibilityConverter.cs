using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;

namespace Controls
{
	public class InvertedBoolToVisibilityConverter : IValueConverter
	{
		public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
		{
			if (value == null) return Visibility.Visible;
			if(value is bool || value is bool?)
			{
				bool boolValue = (bool) value;
				return boolValue ? Visibility.Collapsed : Visibility.Visible;
			}
			else
			{
				return Visibility.Collapsed;
			}

		}

		public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
		{
			throw new NotImplementedException();
		}
	}
}