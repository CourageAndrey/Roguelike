using System.Drawing;

using Roguelike.Core.StaticObjects;

namespace Roguelike.WpfClient.ViewModels
{
	internal class FireViewModel : ObjectViewModel<Fire>
	{
		public FireViewModel(Fire o)
			: base(o)
		{ }

		#region Overrides

		public override string Text
		{ get { return "&"; } }

		public override Color Foreground
		{ get { return Color.Red; } }

		#endregion
	}
}
