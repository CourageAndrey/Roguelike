using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;

using Roguelike.Core.Items;
using Roguelike.Core.Localization;
using Roguelike.Core.Objects;

namespace Roguelike.Core
{
	public class Race
	{
		#region Properties

		private readonly Func<LanguageRaces, string> _getName;
		private readonly Action<Humanoid> _dressCostume;

		public Color SkinColor
		{ get; }

		public IReadOnlyCollection<Color> HairColors
		{ get; }

		#endregion

		private Race(
			Func<LanguageRaces, string> getName,
			Action<Humanoid> dressCostume,
			Color skinColor,
			IEnumerable<Color> hairColors)
		{
			_getName = getName;
			_dressCostume = dressCostume;
			SkinColor = skinColor;
			HairColors = hairColors.ToArray();
		}

		public string GetName(LanguageRaces language)
		{
			return _getName(language);
		}

		public void DressCostume(Humanoid humanoid)
		{
			_dressCostume(humanoid);
		}

		#region List

		public static readonly Race SinglePossible = new Race(
			language => "Human",
			humanoid =>
			{
				if (humanoid.SexIsMale)
				{
					humanoid.Manequin.LowerBodyWear = ItemFactory.CreateTrousers(Color.Brown);
					humanoid.Manequin.UpperBodyWear = ItemFactory.CreateShirt(Color.LightGray);
				}
				else
				{
					humanoid.Manequin.LowerBodyWear = ItemFactory.CreateSkirt(Color.Red);
					humanoid.Manequin.UpperBodyWear = ItemFactory.CreateShirt(Color.LightGray);
				}
			},
		Color.White,
		new[] { Color.Black });

		#endregion
	}
}
