using System.Drawing;

using Roguelike.Core.StaticObjects;

namespace Roguelike.WpfClient.ViewModels
{
	internal class PoolViewModel : ObjectViewModel<Pool>
	{
		public PoolViewModel(Pool o)
			: base(o)
		{ }

		#region Overrides

		public override string Text
		{ get { return "0"; } }

		public override Color Foreground
		{ get { return Color.DodgerBlue; } }

		#endregion
	}
}
