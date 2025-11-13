using System.Drawing;

using Roguelike.Core.Interfaces;
using Roguelike.Core.Objects;

namespace Roguelike.Core.Aspects
{
	public class Appearance : AspectWithHolder<IHumanoid>
	{
		#region Properties

		public Color HairColor
		{ get; }

		public Haircut Haircut
		{ get; set; }

		#endregion

		public Appearance(IHumanoid holder, Color hairColor, Haircut haircut)
			: base(holder)
		{
			HairColor = hairColor;
			Haircut = haircut;
		}
	}
}
