using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;

using Roguelike.Core.Localization;

namespace Roguelike.Core
{
	public class Race
	{
		#region Properties

		private readonly Func<LanguageRaces, string> _getName;

		public Color SkinColor
		{ get; }

		public IReadOnlyCollection<Color> HairColors
		{ get; }

		#endregion

		private Race(Func<LanguageRaces, string> getName, Color skinColor, IEnumerable<Color> hairColors)
		{
			_getName = getName;
			SkinColor = skinColor;
			HairColors = hairColors.ToArray();
		}

		public string GetName(LanguageRaces language)
		{
			return _getName(language);
		}

		#region List

		public static readonly Race SinglePossible = new Race(language => "Human", Color.White, new[] { Color.Black });

		#endregion
	}
}
