using System.Drawing;

using Roguelike.Core.StaticObjects;

namespace Roguelike.WpfClient.ViewModels
{
	internal class TreeViewModel : ObjectViewModel<Tree>
	{
		public TreeViewModel(Tree o)
			: base(o)
		{ }

		#region Overrides

		public override string Text
		{ get { return "T"; } }

		public override Color Foreground
		{ get { return Color.LimeGreen; } }

		#endregion
	}
}
