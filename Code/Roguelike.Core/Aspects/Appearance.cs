using System;
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
		{
			get { return _haircut; }
			set
			{
				if (value != null)
				{
					_haircut = value;
				}
				else
				{
					throw new ArgumentNullException(nameof(value));
				}
			}
		}

		private Haircut _haircut;

		#endregion

		public Appearance(Color hairColor, Haircut haircut)
		{
			HairColor = hairColor;
			Haircut = haircut;
		}
	}
}
