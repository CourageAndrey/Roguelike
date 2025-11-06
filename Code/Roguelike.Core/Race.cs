using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;

using Roguelike.Core.Aspects;
using Roguelike.Core.Interfaces;
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
		private readonly Func<bool, string, string> _generateName;
		private readonly Func<Profession, Properties> _getProperties;
		private readonly Func<Profession, IEnumerable<IItem>> _getItems;

		public Color SkinColor
		{ get; }

		public IReadOnlyCollection<Color> HairColors
		{ get; }

		public IReadOnlyList<string> Surnames
		{ get; }

		#endregion

		private Race(
			Func<LanguageRaces, string> getName,
			Action<Humanoid> dressCostume,
			Func<bool, string, string> generateName,
			Color skinColor,
			IEnumerable<Color> hairColors,
			IEnumerable<string> surnames,
			Func<Profession, Properties> getProperties,
			Func<Profession, IEnumerable<IItem>> getItems)
		{
			_getRaceName = getName;
			_dressCostume = dressCostume;
			_generateName = generateName;
			SkinColor = skinColor;
			HairColors = hairColors.ToArray();
			Surnames = surnames.ToArray();
			_getProperties = getProperties;
			_getItems = getItems;
		}

		public string GetName(LanguageRaces language)
		{
			return _getRaceName(language);
		}

		public void DressCostume(Humanoid humanoid)
		{
			_dressCostume(humanoid);
		}

		public string GenerateName(bool sexIsMale, string familyName)
		{
			return _generateName(sexIsMale, familyName);
		}

		public Properties GetProperties(Profession profession)
		{
			return _getProperties(profession);
		}

		public IEnumerable<IItem> GetItems(Profession profession)
		{
			return _getItems(profession);
		}

		#region List

		private static string GenerateRandomName(bool isMale, IList<string> maleNames, IList<string> femaleNames, string familyName)
		{
			var names = isMale ? maleNames : femaleNames;
			var random = new Random(DateTime.Now.Millisecond);
			return names[random.Next(0, names.Count - 1)] + " " + familyName;
		}

		public static readonly Race PlainsMan = new Race(
			language => "Plainsman",
			humanoid =>
			{
				if (humanoid.SexIsMale)
				{
					humanoid.Mannequin.LowerBodyWear = ItemFactory.CreateTrousers(Color.Brown);
					humanoid.Mannequin.UpperBodyWear = ItemFactory.CreateShirt(Color.LightGray);
				}
				else
				{
					humanoid.Mannequin.LowerBodyWear = ItemFactory.CreateSkirt(Color.Red);
					humanoid.Mannequin.UpperBodyWear = ItemFactory.CreateShirt(Color.LightGray);
				}
			},
			(sexIsMale, familyName) =>
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

				return GenerateRandomName(sexIsMale, maleNames, femaleNames, familyName);
			},
			Color.White,
			new[] { Color.Black },
			new[]
			{
				"Black",
				"White",
				"Green",
				"Brown",
				"Evans",
				"Stone",
				"Roberts",
				"Mills",
				"Lewis",
				"Morgan",
				"Florence",
				"Campbell",
				"Bronte",
				"Bell",
				"Adams",
			},
			profession => new Properties(10, 10, 30, 10, 10, 10),
			profession => Array.Empty<IItem>());

		public static readonly Race Nomad = new Race(
			language => "Nomad",
			humanoid =>
			{
				if (humanoid.SexIsMale)
				{
					humanoid.Mannequin.LowerBodyWear = ItemFactory.CreateTrousers(Color.Brown);
					humanoid.Mannequin.UpperBodyWear = ItemFactory.CreateShirt(Color.Brown);
				}
				else
				{
					humanoid.Mannequin.LowerBodyWear = ItemFactory.CreateSkirt(Color.SandyBrown);
					humanoid.Mannequin.UpperBodyWear = ItemFactory.CreateShirt(Color.SandyBrown);
				}
			},
			(sexIsMale, familyName) =>
			{
				var maleNames = new List<string>
				{
					"Abbas",
					"Abu",
					"Aziz",
					"Ali",
					"Valid",
					"Jabir",
					"Zahir",
					"Imran",
					"Ibrahim",
					"Karim",
					"Maimun",
					"Mubarak",
				};

				var femaleNames = new List<string>
				{
					"Aisha",
					"Basma",
					"Jamila",
					"Zainab",
					"Zuhra",
					"Leila",
					"Mariam",
				};

				return GenerateRandomName(sexIsMale, maleNames, femaleNames, familyName);
			},
			Color.SaddleBrown,
			new[] { Color.Black },
			new[]
			{
				"Hagan",
				"Hulgana",
				"Shona",
				"Naran",
				"Oktaj",
			},
			profession => new Properties(10, 10, 30, 10, 10, 10),
			profession => Array.Empty<IItem>());

		public static readonly Race Highlander = new Race(
			language => "Highlander",
			humanoid =>
			{
				if (humanoid.SexIsMale)
				{
					humanoid.Mannequin.LowerBodyWear = ItemFactory.CreateTrousers(Color.DarkGreen);
					humanoid.Mannequin.UpperBodyWear = ItemFactory.CreateShirt(Color.White);
				}
				else
				{
					humanoid.Mannequin.LowerBodyWear = ItemFactory.CreateSkirt(Color.DarkGreen);
					humanoid.Mannequin.UpperBodyWear = ItemFactory.CreateShirt(Color.White);
				}
			},
			(sexIsMale, familyName) =>
			{
				var maleNames = new List<string>
				{
					"Breasal",
					"Brian",
					"Conall",
					"Conan",
					"Kenneth",
					"Lorcan",
					"Niall",
					"Rian",
				};

				var femaleNames = new List<string>
				{
					"Eithne",
					"Deirdre",
					"Sorcha",
				};

				return GenerateRandomName(sexIsMale, maleNames, femaleNames, familyName);
			},
			Color.LightGray,
			new[] { Color.Black },
			new[]
			{
				"McCartney",
				"O'Sullivan",
				"O'Connor",
				"O'Neill",
				"O'Reilly",
				"Walsh",
			},
			profession => new Properties(10, 10, 30, 10, 10, 10),
			profession => Array.Empty<IItem>());

		public static readonly Race Jungleman = new Race(
			language => "Jungleman",
			humanoid =>
			{
				if (humanoid.SexIsMale)
				{
					humanoid.Mannequin.LowerBodyWear = ItemFactory.CreateTrousers(Color.DarkGreen);
					humanoid.Mannequin.UpperBodyWear = ItemFactory.CreateShirt(Color.LimeGreen);
				}
				else
				{
					humanoid.Mannequin.LowerBodyWear = ItemFactory.CreateSkirt(Color.DarkGreen);
					humanoid.Mannequin.UpperBodyWear = ItemFactory.CreateShirt(Color.LimeGreen);
				}
			},
			(sexIsMale, familyName) =>
			{
				var maleNames = new List<string>
				{
					"Amokstly",
					"Zolin",
					"Ikstly",
					"Kitlaly",
					"Koatle",
				};

				var femaleNames = new List<string>
				{
					"Zelcine",
					"Iskacine",
					"Papan",
					"Tlako",
				};

				return GenerateRandomName(sexIsMale, maleNames, femaleNames, familyName);
			},
			Color.RosyBrown,
			new[] { Color.Black },
			new[]
			{
				"Atl",
				"Ake",
				"kojotl",
			},
			profession => new Properties(10, 10, 30, 10, 10, 10),
			profession => Array.Empty<IItem>());

		public static readonly Race Nordman = new Race(
			language => "Nordman",
			humanoid =>
			{
				if (humanoid.SexIsMale)
				{
					humanoid.Mannequin.LowerBodyWear = ItemFactory.CreateTrousers(Color.Brown);
					humanoid.Mannequin.UpperBodyWear = ItemFactory.CreateShirt(Color.White);
				}
				else
				{
					humanoid.Mannequin.LowerBodyWear = ItemFactory.CreateSkirt(Color.Brown);
					humanoid.Mannequin.UpperBodyWear = ItemFactory.CreateShirt(Color.White);
				}
			},
			(sexIsMale, familyName) =>
			{
				var maleNames = new List<string>
				{
					"Oscar",
					"Liam",
					"Axel",
					"Noah",
					"Nils",
					"Arvid",
					"Theodor",
					"Olle",
					"Erik",
					"Viggo",
					"Ebbe",
					"Elton",
					"Otto",
				};

				var femaleNames = new List<string>
				{
					"Elsa",
					"Agnes",
					"Olivia",
					"Julia",
					"Ebba",
					"Linnea",
					"Freja",
					"Astrid",
					"Signe",
					"Tyra",
					"Tuva",
					"Tilde",
				};

				return GenerateRandomName(sexIsMale, maleNames, femaleNames, familyName);
			},
			Color.White,
			new[] { Color.White },
			new[]
			{
				"Andersson",
				"Johansson",
				"Karlsson",
				"Nilsson",
				"Eriksson",
				"Pettersson",
				"Lindberg",
				"Lindgren",
				"Bergström",
				"Fredriksson",
				"Björk",
			},
			profession => new Properties(10, 10, 30, 10, 10, 10),
			profession => Array.Empty<IItem>());

		public static readonly IReadOnlyCollection<Race> All = new[]
		{
			PlainsMan,
			Nomad,
			Highlander,
			Nordman,
			Jungleman,
		};

		#endregion
	}
}
