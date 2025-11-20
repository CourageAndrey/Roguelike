using Roguelike.Core;
using Roguelike.Core.ActiveObjects;

namespace Roguelike.WpfClient.Windows
{
	public partial class TradeWindow
	{
		public TradeWindow()
		{
			InitializeComponent();
		}

		#region Properties

		public Game Game
		{ get; set; }

		public Humanoid Trader
		{ get; set; }

		public ActionResult Result
		{ get; private set; }

		#endregion
	}
}
