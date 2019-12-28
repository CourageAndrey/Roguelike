using System.Drawing;

using Roguelike.Core.StaticObjects;

namespace Roguelike.WpfClient.ViewModels
{
	internal class CorpseViewModel : ObjectViewModel<Corpse>
	{
		public CorpseViewModel(Corpse o)
			: base(o)
		{ }

		#region Overrides

		public override string Text
		{ get { return "%"; } }

		public override Color Foreground
		{ get { return Color.SandyBrown; } }

		#endregion
	}
}
