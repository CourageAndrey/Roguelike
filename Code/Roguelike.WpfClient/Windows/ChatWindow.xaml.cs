using Roguelike.Core;
using Roguelike.Core.ActiveObjects;

namespace Roguelike.WpfClient.Windows
{
	public partial class ChatWindow
	{
		public ChatWindow()
		{
			InitializeComponent();
		}

		#region Properties

		public Game Game
		{ get; set; }

		public Humanoid Companion
		{ get; set; }

		public ActionResult Result
		{ get; private set; }

		#endregion
	}
}
