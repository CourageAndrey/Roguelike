using System;
using System.Globalization;
using System.Text;
using System.Windows.Data;

namespace Roguelike.WpfClient
{
	[ValueConversion(typeof(string), typeof(string))]
	public class BorderConverter : IValueConverter
	{
		public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
		{
			string original = (string) value;
			var wrapped = new StringBuilder();
			wrapped.AppendLine("┌" + new string('─', (original != null ? original.Length : 0) + 2) + "┐");
			wrapped.AppendLine("│ " + original + " │");
			wrapped.Append("└" + new string('─', (original != null ? original.Length : 0) + 2) + "┘");
			return wrapped.ToString();
		}

		public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
		{
			throw new NotSupportedException();
		}
	}
}
