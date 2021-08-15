using System;
using System.Globalization;
using System.Windows.Data;

using Roguelike.Core;

namespace Roguelike.WpfClient.Converters
{
	[ValueConversion(typeof(object), typeof(bool))]
	public class ItemSelectedConverter : IValueConverter
	{
		public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
		{
			var item = value as ListItem;
			return item != null && item.IsAvailable;
		}

		public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
		{
			throw new NotSupportedException();
		}
	}
}
