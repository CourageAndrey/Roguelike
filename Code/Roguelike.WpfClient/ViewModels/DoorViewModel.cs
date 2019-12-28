using System.Drawing;

using Roguelike.Core.StaticObjects;

namespace Roguelike.WpfClient.ViewModels
{
	internal class DoorViewModel : ObjectViewModel<Door>
	{
		public DoorViewModel(Door o)
			: base(o)
		{ }

		#region Overrides

		public override string Text
		{ get { return Object.IsOpened ? "/" : "+"; } }

		public override Color Foreground
		{ get { return Color.SaddleBrown; } }

		#endregion
	}
}
