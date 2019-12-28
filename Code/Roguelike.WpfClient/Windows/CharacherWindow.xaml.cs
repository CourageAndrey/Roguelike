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
	}
}
