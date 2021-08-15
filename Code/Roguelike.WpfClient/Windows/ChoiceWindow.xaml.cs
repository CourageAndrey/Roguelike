using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

using Roguelike.Core;

namespace Roguelike.WpfClient.Windows
{
	public partial class ChoiceWindow
	{
		public ChoiceWindow()
		{
			InitializeComponent();
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
			set
			{
				listBox.ItemsSource = value;
				listBox.SelectedItem = value.FirstOrDefault(i => i.IsAvailable);
				(listBox.SelectedItem == null ? buttonCancel : buttonOk).Focus();
			}
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
			chooseCancel();
		}

		private void itemSelectClick(object sender, MouseButtonEventArgs e)
		{
			tryToChooseSelectedItem();
		}

		private void exitKeyDown(object sender, KeyEventArgs e)
		{
			if (e.Key == Key.Escape)
			{
				chooseCancel();
			}
			else if (e.Key == Key.Enter)
			{
				tryToChooseSelectedItem();
			}
		}

		private void chooseCancel()
		{
			DialogResult = false;
		}

		private void tryToChooseSelectedItem()
		{
			if (SelectedItem != null && SelectedItem.IsAvailable)
			{
				DialogResult = true;
			}
		}
	}
}
