using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;

namespace Roguelike.WpfClient.Windows
{
	public class RoguelikeWindow : Window
	{
		public RoguelikeWindow()
		{
			FontFamily = new FontFamily("Courier New");
			Background = Brushes.Black;
			SizeToContent = SizeToContent.WidthAndHeight;
			WindowStartupLocation = WindowStartupLocation.CenterScreen;
			PreviewKeyDown += exitKeyDown;

			var commonControlStyle = new Style
			{
				Setters =
				{
					new Setter(BackgroundProperty, Brushes.Black),
					new Setter(BorderThicknessProperty, new Thickness(0)),
					new Setter(ForegroundProperty, Brushes.White),
					new Setter(MarginProperty, new Thickness(5)),
				},
			};
			var buttonStyle = new Style(typeof(Button), commonControlStyle)
			{
				Triggers =
				{
					new Trigger
					{
						Property = IsFocusedProperty,
						Value = true,
						Setters =
						{
							new Setter(BackgroundProperty, Brushes.Blue),
						},
					}
				},
			};
			Resources.Add(typeof(Control), commonControlStyle);
			Resources.Add(typeof(Button), buttonStyle);
			Resources.Add(typeof(ListBox), commonControlStyle);
			Resources.Add(typeof(TextBox), commonControlStyle);
		}

		private void exitKeyDown(object sender, KeyEventArgs e)
		{
			if (e.Key == Key.Escape)
			{
				DialogResult = false;
			}
		}
	}
}
