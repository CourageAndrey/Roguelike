using System.Drawing;

using Roguelike.Core.ActiveObjects;
using Roguelike.Core.Interfaces;
using Roguelike.Core.Items;

namespace Roguelike.Console.ViewModels
{
	internal class HumanoidViewModel : ObjectViewModel<Humanoid>
	{
		public HumanoidViewModel(Humanoid o)
			: base(o)
		{ }

		#region Overrides

		public override string Text
		{ get { return "@"; } }

		public override System.ConsoleColor Foreground
		{
			get
			{
				var topWear = getTopWear(Object.Manequin);
				var color = topWear != null
					? topWear.Color
					: Object.Race.SkinColor;
				return color.ToConsole();
			}
		}

		public override System.ConsoleColor Background
		{ get { return Object.Transport == null ? base.Background : System.ConsoleColor.Red; } }

		#endregion

		private static IWear getTopWear(IManequin manequin)
		{
			if (!(manequin.CoverWear is Naked)) return manequin.CoverWear;
			if (!(manequin.UpperBodyWear is Naked)) return manequin.UpperBodyWear;
			if (!(manequin.LowerBodyWear is Naked)) return manequin.LowerBodyWear;
			return null;
		}
	}
}
