using System.Windows.Input;

using Roguelike.Core.ActiveObjects;
using Roguelike.Core.Localization;

namespace Roguelike.WpfClient.Windows
{
	public partial class SkillsWindow
	{
		public SkillsWindow()
		{
			InitializeComponent();
		}

		public Language GameLanguage
		{ get; set; }

		public Humanoid Character
		{ get; set; }

		private void exitKeyDown(object sender, KeyEventArgs e)
		{
			if (e.Key == Key.Escape)
			{
				DialogResult = false;
			}
		}
	}
}
