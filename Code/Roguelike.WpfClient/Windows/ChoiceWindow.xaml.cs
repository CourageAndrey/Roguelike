using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

using Roguelike.Core;

namespace Roguelike.WpfClient
{
	public partial class ChoiceWindow
	{
		public ChoiceWindow()
		{
			InitializeComponent();

			buttonsPanel.Children.OfType<Control>().Last().Focus();
		}

		public Game Game
		{
			get { return game; }
			set
			{
				game = value;
				buttonsPanel.DataContext = game.Language;
			}
		}
		private Game game;

		public IEnumerable<ListItem> Items
		{
			get { return listBox.ItemsSource as IEnumerable<ListItem>; }
			set { listBox.ItemsSource = value; }
		}

		public ListItem SelectedItem
		{
			get { return listBox.SelectedItem as ListItem; }
			set { listBox.SelectedItem = value; }
		}

		private void okClick(object sender, RoutedEventArgs e)
		{
			DialogResult = SelectedItem != null && SelectedItem.IsAvailable;
		}

		private void cancelClick(object sender, RoutedEventArgs e)
		{
			DialogResult = false;
		}

		private void itemSelectClick(object sender, MouseButtonEventArgs e)
		{
			if (SelectedItem != null && SelectedItem.IsAvailable)
			{
				DialogResult = true;
			}
		}
	}
}
