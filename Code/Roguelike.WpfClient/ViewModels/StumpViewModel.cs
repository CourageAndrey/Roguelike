using System.Drawing;

using Roguelike.Core.StaticObjects;

namespace Roguelike.WpfClient.ViewModels
{
	internal class StumpViewModel : ObjectViewModel<Stump>
	{
		public StumpViewModel(Stump o)
			: base(o)
		{ }

		#region Overrides

		public override string Text
		{ get { return "т"; } }

		public override Color Foreground
		{ get { return Color.DarkGray; } }

		#endregion
	}
}
