using System.Drawing;

using Roguelike.Core.ActiveObjects;

namespace Roguelike.WpfClient.ViewModels
{
	internal class AnimalViewModel : ObjectViewModel<Animal>
	{
		public AnimalViewModel(Animal o)
			: base(o)
		{ }

		#region Overrides

		public override string Text
		{ get { return "a"; } }

		public override Color Foreground
		{ get { return Color.Lime; } }

		#endregion
	}
}
