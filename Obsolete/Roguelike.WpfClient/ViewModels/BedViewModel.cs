using System.Drawing;

using Roguelike.Core.StaticObjects;

namespace Roguelike.WpfClient.ViewModels
{
	internal class BedViewModel : ObjectViewModel<Bed>
	{
		public BedViewModel(Bed o)
			: base(o)
		{ }

		#region Overrides

		public override string Text
		{ get { return "&"; } }

		public override Color Foreground
		{ get { return Color.SaddleBrown; } }

		#endregion
	}
}
