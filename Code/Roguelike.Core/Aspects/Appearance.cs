using System.Drawing;

using Roguelike.Core.Interfaces;
using Roguelike.Core.Objects;

namespace Roguelike.Core.Aspects
{
	public class Appearance : IAspect
	{
		#region Properties

		public Color HairColor
		{ get; }

		public Haircut Haircut
		{ get; set; }

		#endregion

		public Appearance(Color hairColor, Haircut haircut)
		{
			HairColor = hairColor;
			Haircut = haircut;
		}
	}
}
