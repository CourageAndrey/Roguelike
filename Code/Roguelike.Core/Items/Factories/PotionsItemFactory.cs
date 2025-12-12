using System.Drawing;

using Roguelike.Core.Aspects;
using Roguelike.Core.Interfaces;
using Roguelike.Core.Mechanics;

namespace Roguelike.Core.Items.Factories
{
	internal class PotionsItemFactory
	{
		public IItem CreateBottleOFWater()
		{
			return new Item(
				(language, alive) => language.Items.Potions.BottleOFWater,
				() => 0.6m,
				ItemType.Potion,
				Color.DodgerBlue,
				Material.Liquid,
				new Drink(0, 500)
			);
		}

		public IItem CreateHealingPotion()
		{
			return new Item(
				(language, alive) => language.Items.Potions.HealingPotion,
				() => 0.5m,
				ItemType.Potion,
				Color.Red,
				Material.Liquid,
				new Drink(100, 100)
			);
		}

		public IItem CreateManaPotion()
		{
			return new Item(
				(language, alive) => language.Items.Potions.ManaPotion,
				() => 0.5m,
				ItemType.Potion,
				Color.Blue,
				Material.Liquid,
				new Drink(50, 50)
			);
		}

		public IItem CreateStrengthPotion()
		{
			return new Item(
				(language, alive) => language.Items.Potions.StrengthPotion,
				() => 0.5m,
				ItemType.Potion,
				Color.Orange,
				Material.Liquid,
				new Drink(200, -50)
			);
		}
	}
}
