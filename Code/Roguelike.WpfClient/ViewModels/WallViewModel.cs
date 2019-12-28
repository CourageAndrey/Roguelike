using System.Drawing;

using Roguelike.Core.StaticObjects;

namespace Roguelike.WpfClient.ViewModels
{
	internal class WallViewModel : ObjectViewModel<Wall>
	{
		public WallViewModel(Wall o)
			: base(o)
		{ }

		#region Overrides

		public override string Text
		{ get { return "#"; } }

		public override Color Foreground
		{ get { return Color.Silver; } }

		#endregion
	}
}
