﻿using System;
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

		public static readonly Race SinglePossible = new Race(
			language => "Human",
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

				var names = sexIsMale ? maleNames : femaleNames;
				var random = new Random(DateTime.Now.Millisecond);
				return names[random.Next(0, names.Count - 1)] + " " + familyName;
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

		public static readonly IReadOnlyCollection<Race> All = new[]
		{
			SinglePossible,
		};

		#endregion
	}
}
