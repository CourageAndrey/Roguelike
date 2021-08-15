using Roguelike.Core.ActiveObjects;
using Roguelike.Core.Localization;

namespace Roguelike.WpfClient.Windows
{
	public partial class InventoryWindow
	{
		public InventoryWindow()
		{
			InitializeComponent();
		}

		public Language GameLanguage
		{ get; set; }

		public Humanoid Character
		{ get; set; }
	}
}
