using Roguelike.Core;
using Roguelike.Core.ActiveObjects;

namespace Roguelike.WpfClient.Windows
{
	public partial class PickpocketWindow
	{
		public PickpocketWindow()
		{
			InitializeComponent();
		}

		#region Properties

		public Game Game
		{ get; set; }

		public Humanoid Victim
		{ get; set; }

		public ActionResult Result
		{ get; private set; }

		#endregion
	}
}
