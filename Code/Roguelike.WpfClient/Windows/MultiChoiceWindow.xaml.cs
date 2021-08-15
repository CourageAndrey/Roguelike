using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

using Roguelike.Core;

namespace Roguelike.WpfClient.Windows
{
	public partial class MultiChoiceWindow
	{
		public MultiChoiceWindow()
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

		public IList<ListItem> SelectedItems
		{
			get { return ((listBox.ItemsSource as IEnumerable<ListItem>) ?? new ListItem[0]).Where(i => i.IsAvailable && i.IsSelected).ToList(); }
			set
			{
				foreach (ListItem item in listBox.ItemsSource)
				{
					item.IsSelected = value.Contains(item);
				}
			}
		}

		private void okClick(object sender, RoutedEventArgs e)
		{
			DialogResult = true;
		}

		private void cancelClick(object sender, RoutedEventArgs e)
		{
			DialogResult = false;
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
