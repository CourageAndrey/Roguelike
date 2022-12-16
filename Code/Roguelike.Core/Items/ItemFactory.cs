using System;
using System.Drawing;

using Roguelike.Core.Aspects;
using Roguelike.Core.Interfaces;
using Roguelike.Core.Localization;

namespace Roguelike.Core.Items
{
	internal static class ItemFactory
	{
		public static IItem CreateArrow()
		{
			return new Item(
				(language, alive) => language.Items.Arrow,
				() => 0.1m,
				ItemType.Weapon,
				Color.White,
				Material.Wood,
				new Missile());
		}

		public static IItem CreateBook(Color coverColor, Func<LanguageBooks, string> getTitle, Func<LanguageBooks, string> getText)
		{
			return new Item(
				(language, alive) => language.Items.Book,
				() => 1,
				ItemType.Paper,
				coverColor,
				Material.Paper,
				new Paper(getTitle, getText)
			);
		}

		public static IItem CreateBottleOFWater()
		{
			return new Item(
				(language, alive) => language.Items.BottleOFWater,
				() => 0.6m,
				ItemType.Potion,
				Color.DodgerBlue,
				Material.Liquid,
				new Drink(0, 500)
			);
		}

		public static IItem CreateBow()
		{
			return new Item(
				(language, alive) => language.Items.Bow,
				() => 1,
				ItemType.Weapon,
				Material.Wood.Color,
				Material.Wood,
				new Weapon(true)
			);
		}

		public static IItem CreateHatchet()
		{
			return new Item(
				(language, alive) => language.Items.Hatchet,
				() => 2,
				ItemType.Weapon,
				Material.Metal.Color,
				Material.Metal,
				new Weapon(false), new TreeChopper()
			);
		}

		public static IItem CreateLoafOfBread()
		{
			return new Item(
				(language, alive) => language.Items.LoafOfBread,
				() => 0.4m,
				ItemType.Food,
				Color.Brown,
				Material.Food,
				new Food(500, -50)
			);
		}

		public static IItem CreateLog()
		{
			return new Item(
				(language, alive) => language.Items.LoafOfBread,
				() => 0.5m,
				ItemType.Tool,
				Color.Brown,
				Material.Wood
			);
		}

		public static IItem CreateRing()
		{
			return new Item(
				(language, alive) => language.Items.Ring,
				() => 0.02m,
				ItemType.Wear,
				Color.Aquamarine,
				Material.Metal,
				new Wear(WearSlot.Jewelry)
			);
		}

		public static IItem CreateShirt(Color clothColor)
		{
			return new Item(
				(language, alive) => language.Items.Shirt,
				() => 1,
				ItemType.Wear,
				clothColor,
				Material.Fabric,
				new Wear(WearSlot.UpperBody)
			);
		}

		public static IItem CreateSkirt(Color clothColor)
		{
			return new Item(
				(language, alive) => language.Items.Skirt,
				() => 1,
				ItemType.Wear,
				clothColor,
				Material.Fabric,
				new Wear(WearSlot.LowerBody)
			);
		}

		public static IItem CreateTrousers(Color clothColor)
		{
			return new Item(
				(language, alive) => language.Items.Trousers,
				() => 1,
				ItemType.Wear,
				clothColor,
				Material.Fabric,
				new Wear(WearSlot.LowerBody)
			);
		}

		public static IItem CreateGrass(int nutricity)
		{
			return new Item(
				(language, alive) => language.Items.Grass,
				() => 0,
				ItemType.Food,
				Color.DarkGreen,
				Material.Food,
				new Food(nutricity, nutricity / 8)
			);
		}
	}
}
