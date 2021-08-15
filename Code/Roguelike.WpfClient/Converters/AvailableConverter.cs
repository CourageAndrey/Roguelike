using System;
using System.Globalization;
using System.Windows.Data;
using System.Windows.Media;

namespace Roguelike.WpfClient.Converters
{
	[ValueConversion(typeof(bool), typeof(Brush))]
	public class AvailableConverter : IValueConverter
	{
		public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
		{
			return (bool) value ? Brushes.White : Brushes.Silver;
		}

		public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
		{
			throw new NotSupportedException();
		}
	}
}
