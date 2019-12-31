using System.Windows.Input;

using Roguelike.Core.Localization;

namespace Roguelike.WpfClient
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

		private void exitKeyDown(object sender, KeyEventArgs e)
		{
			if (e.Key == Key.Escape)
			{
				DialogResult = false;
			}
		}
	}
}
