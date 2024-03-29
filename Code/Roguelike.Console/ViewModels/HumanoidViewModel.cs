﻿using System.Drawing;

using Roguelike.Core.Aspects;
using Roguelike.Core.Interfaces;
using Roguelike.Core.Items;
using Roguelike.Core.Objects;

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
		{ get { return getColor(Object).ToConsole(); } }

		public override System.ConsoleColor Background
		{
			get
			{
				
				if (Object.Rider.Transport != null)
				{
					var aliveTransport = Object.Rider.Transport as IAlive;
					return (aliveTransport != null
						? aliveTransport.SkinColor
						: invert(getColor(Object))).ToConsole();
				}
				else
				{
					return base.Background;
				}
			}
		}

		#endregion

		private static Color invert(Color color)
		{
			const int rgbmax = 255;
			return Color.FromArgb(
				rgbmax - color.R,
				rgbmax - color.G,
				rgbmax - color.B);
		}

		private static Color getColor(IHumanoid humanoid)
		{
			var topWear = getTopWear(humanoid.Manequin);
			return topWear != null
				? topWear.Color
				: humanoid.SkinColor;
		}

		private static IItem getTopWear(Manequin manequin)
		{
			if (!(manequin.CoverWear is Naked)) return manequin.CoverWear;
			if (!(manequin.UpperBodyWear is Naked)) return manequin.UpperBodyWear;
			if (!(manequin.LowerBodyWear is Naked)) return manequin.LowerBodyWear;
			return null;
		}
	}
}
