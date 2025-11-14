using System.Drawing;

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

		private readonly Func<LanguageRaces, LanguageRace> _getRaceInfo;
		private readonly Action<Humanoid> _dressCostume;
		private readonly Func<Profession, Properties> _getProperties;
		private readonly Func<Profession, IEnumerable<IItem>> _getItems;

		public Color SkinColor
		{ get; }

		public IReadOnlyCollection<Color> HairColors
		{ get; }

		#endregion

		private Race(
			Func<LanguageRaces, LanguageRace> getRaceInfo,
			Action<Humanoid> dressCostume,
			Color skinColor,
			IEnumerable<Color> hairColors,
			Func<Profession, Properties> getProperties,
			Func<Profession, IEnumerable<IItem>> getItems)
		{
			_getRaceInfo = getRaceInfo;
			_dressCostume = dressCostume;
			SkinColor = skinColor;
			HairColors = hairColors.ToArray();
			_getProperties = getProperties;
			_getItems = getItems;
		}

		public string GetName(LanguageRaces language)
		{
			return _getRaceInfo(language).Name;
		}

		public void DressCostume(Humanoid humanoid)
		{
			_dressCostume(humanoid);
		}

		public string GenerateName(bool sexIsMale, string familyName, LanguageRaces language)
		{
			var raceInfo = _getRaceInfo(language);
			var names = sexIsMale ? raceInfo.Names.Male : raceInfo.Names.Female;
			var random = new Random(DateTime.Now.Millisecond);
			return names[random.Next(0, names.Length - 1)] + " " + familyName;
		}

		public IReadOnlyList<string> GetSurnames(LanguageRaces language)
		{
			return _getRaceInfo(language).Surnames;
		}

		public string GetDefaultSettlementName(LanguageRaces language)
		{
			return _getRaceInfo(language).DefaultSettlementName;
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

		public static readonly Race PlainsMan = new Race(
			language => language.Plainsman,
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
			Color.White,
			new[] { Color.Black },
			profession => new Properties(10, 10, 30, 10, 10, 10),
			profession => Array.Empty<IItem>());

		public static readonly Race Nomad = new Race(
			language => language.Nomad,
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
			Color.SaddleBrown,
			new[] { Color.Black },
			profession => new Properties(10, 10, 30, 10, 10, 10),
			profession => Array.Empty<IItem>());

		public static readonly Race Highlander = new Race(
			language => language.Highlander,
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
			Color.LightGray,
			new[] { Color.Black },
			profession => new Properties(10, 10, 30, 10, 10, 10),
			profession => Array.Empty<IItem>());

		public static readonly Race Jungleman = new Race(
			language => language.Jungleman,
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
			Color.RosyBrown,
			new[] { Color.Black },
			profession => new Properties(10, 10, 30, 10, 10, 10),
			profession => Array.Empty<IItem>());

		public static readonly Race Nordman = new Race(
			language => language.Nordman,
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
			Color.White,
			new[] { Color.White },
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
