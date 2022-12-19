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

		private readonly Func<LanguageRaces, string> _getRaceName;
		private readonly Action<Humanoid> _dressCostume;
		private readonly Func<bool, string> _generateName;

		public Color SkinColor
		{ get; }

		public IReadOnlyCollection<Color> HairColors
		{ get; }

		#endregion

		private Race(
			Func<LanguageRaces, string> getName,
			Action<Humanoid> dressCostume,
			Func<bool, string> generateName,
			Color skinColor,
			IEnumerable<Color> hairColors)
		{
			_getRaceName = getName;
			_dressCostume = dressCostume;
			_generateName = generateName;
			SkinColor = skinColor;
			HairColors = hairColors.ToArray();
		}

		public string GetName(LanguageRaces language)
		{
			return _getRaceName(language);
		}

		public void DressCostume(Humanoid humanoid)
		{
			_dressCostume(humanoid);
		}

		public string GenerateName(bool sexIsMale)
		{
			return _generateName(sexIsMale);
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
			sexIsMale =>
			{
				var maleNames = new List<string>
				{
					"John",
					"Bill",
					"Bob",
					"Tom",
					"Edward",
					"Harry",
					"Jack",
					"Charles",
					"George",
					"Henry",
					"Louis",
					"Oscar",
				};

				var femaleNames = new List<string>
				{
					"Emma",
					"Alice",
					"Berta",
					"Ella",
					"Charlotte",
					"Amelia",
					"Lillian",
					"Eleanor",
					"Lucy",
					"Juliet",
				};

				var names = sexIsMale ? maleNames : femaleNames;
				var random = new Random(DateTime.Now.Millisecond);
#warning Need to generate surnames
				return names[random.Next(0, names.Count - 1)] + " Smith";
			},
			Color.White,
			new[] { Color.Black });

		#endregion
	}
}
