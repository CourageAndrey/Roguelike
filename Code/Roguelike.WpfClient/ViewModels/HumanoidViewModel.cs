using System.Drawing;

using Roguelike.Core.ActiveObjects;

namespace Roguelike.WpfClient.ViewModels
{
	internal class HumanoidViewModel : ObjectViewModel<Humanoid>
	{
		public HumanoidViewModel(Humanoid o)
			: base(o)
		{ }

		#region Overrides

		public override string Text
		{ get { return "@"; } }

		public override Color Foreground
		{ get { return (Object is Hero) ? Color.White : Color.LightSeaGreen; } }

		#endregion
	}
}
