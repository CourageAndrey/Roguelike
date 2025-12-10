using System;
using System.Drawing;

using Roguelike.Core.Aspects;
using Roguelike.Core.Interfaces;
using Roguelike.Core.Localization;
using Roguelike.Core.Mechanics;

namespace Roguelike.Core.Items
{
	internal static class ItemFactory
	{
		#region Missiles

		public static IItem CreateMissile(MissileType missileType)
		{
			return new Item(
				(language, alive) =>
				{
					switch (missileType)
					{
						case MissileType.Arrow:
							return language.Items.Arrow;
						case MissileType.Bolt:
							return language.Items.Bolt;
						case MissileType.Bullet:
							return language.Items.Bullet;
						default:
							throw new ArgumentOutOfRangeException(nameof(missileType), missileType, null);
					}
				},
				() => 0.1m,
				ItemType.Weapon,
				Color.White,
				Material.Wood,
				new Missile(missileType));
		}

		#endregion

		#region Papers

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

		#endregion

		#region Potions

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

		#endregion

		#region Range weapons

		public static IItem CreateBow()
		{
			return new Item(
				(language, alive) => language.Items.Bow,
				() => 1,
				ItemType.Weapon,
				Material.Wood.Color,
				Material.Wood,
				new RangeWeapon(MissileType.Arrow)
			);
		}

		public static IItem CreateCrossbow()
		{
			return new Item(
				(language, alive) => language.Items.Crossbow,
				() => 3,
				ItemType.Weapon,
				Material.Wood.Color,
				Material.Wood,
				new RangeWeapon(MissileType.Bolt)
			);
		}

		public static IItem CreateSling()
		{
			return new Item(
				(language, alive) => language.Items.Sling,
				() => 0.25m,
				ItemType.Weapon,
				Material.Skin.Color,
				Material.Skin,
				new RangeWeapon(MissileType.Bullet)
			);
		}

		#endregion

		#region Melee weapons

		public static IItem CreateHatchet()
		{
			return new Item(
				(language, alive) => language.Items.Hatchet,
				() => 2,
				ItemType.Weapon,
				Material.Metal.Color,
				Material.Metal,
				new MeleeWeapon(), new TreeChopper()
			);
		}

		public static IItem CreateSword()
		{
			return new Item(
				(language, alive) => language.Items.Sword,
				() => 1.5m,
				ItemType.Weapon,
				Material.Metal.Color,
				Material.Metal,
				new MeleeWeapon()
			);
		}

		public static IItem CreateMace()
		{
			return new Item(
				(language, alive) => language.Items.Mace,
				() => 2,
				ItemType.Weapon,
				Material.Wood.Color,
				Material.Wood,
				new MeleeWeapon()
			);
		}

		public static IItem CreateSpear()
		{
			return new Item(
				(language, alive) => language.Items.Spear,
				() => 3,
				ItemType.Weapon,
				Material.Wood.Color,
				Material.Wood,
				new MeleeWeapon()
			);
		}

		#endregion

		#region Food

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

		#endregion

		#region Tools

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

		#endregion

		#region Jewelry

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

		#endregion

		#region Clothes

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

		public static IItem CreateMantle(Color clothColor)
		{
			return new Item(
				(language, alive) => language.Items.Mantle,
				() => 2,
				ItemType.Wear,
				clothColor,
				Material.Fabric,
				new Wear(WearSlot.UpperBody)
			);
		}

		public static IItem CreateGown(Color clothColor)
		{
			return new Item(
				(language, alive) => language.Items.Gown,
				() => 3,
				ItemType.Wear,
				clothColor,
				Material.Fabric,
				new Wear(WearSlot.UpperBody)
			);
		}

		public static IItem CreateHoodedCloak(Color clothColor)
		{
			return new Item(
				(language, alive) => language.Items.HoodedCloak,
				() => 2,
				ItemType.Wear,
				clothColor,
				Material.Fabric,
				new Wear(WearSlot.Cover)
			);
		}

		#endregion

		#region Special

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

		#endregion
	}
}
