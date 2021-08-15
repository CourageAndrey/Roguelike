using System.Windows;
using System.Windows.Input;

using Roguelike.Core.ActiveObjects;
using Roguelike.Core.Localization;

namespace Roguelike.WpfClient.Windows
{
	public partial class CharacherWindow
	{
		public CharacherWindow()
		{
			InitializeComponent();
		}

		public Language GameLanguage
		{
			get { return buttonsPanel.DataContext as Language; }
			set { buttonsPanel.DataContext = value; }
		}

		public Humanoid Character
		{ get; set; }

		private void exitKeyDown(object sender, KeyEventArgs e)
		{
			if (e.Key == Key.Escape)
			{
				DialogResult = false;
			}
		}

		private void generalClick(object sender, RoutedEventArgs e)
		{
			var dialog = new Windows.PersonWindow
			{
				GameLanguage = GameLanguage,
				Character = Character,
			};
			dialog.ShowDialog();
		}

		private void bodyClick(object sender, RoutedEventArgs e)
		{
			var dialog = new Windows.BodyWindow
			{
				GameLanguage = GameLanguage,
				Character = Character,
			};
			dialog.ShowDialog();
		}

		private void effectsClick(object sender, RoutedEventArgs e)
		{
			var dialog = new Windows.EffectsWindow
			{
				GameLanguage = GameLanguage,
				Character = Character,
			};
			dialog.ShowDialog();
		}

		private void statsClick(object sender, RoutedEventArgs e)
		{
			var dialog = new Windows.StatsWindow
			{
				GameLanguage = GameLanguage,
				Character = Character,
			};
			dialog.ShowDialog();
		}

		private void skillsClick(object sender, RoutedEventArgs e)
		{
			var dialog = new Windows.SkillsWindow
			{
				GameLanguage = GameLanguage,
				Character = Character,
			};
			dialog.ShowDialog();
		}

		private void wearedClick(object sender, RoutedEventArgs e)
		{
			var dialog = new Windows.WearingsWindow
			{
				GameLanguage = GameLanguage,
				Character = Character,
			};
			dialog.ShowDialog();
		}

		private void inventoryClick(object sender, RoutedEventArgs e)
		{
			var dialog = new Windows.InventoryWindow
			{
				GameLanguage = GameLanguage,
				Character = Character,
			};
			dialog.ShowDialog();
		}
	}
}
