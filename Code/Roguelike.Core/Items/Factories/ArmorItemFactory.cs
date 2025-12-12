using System.Drawing;

using Roguelike.Core.Aspects;
using Roguelike.Core.Interfaces;
using Roguelike.Core.Mechanics;

namespace Roguelike.Core.Items.Factories
{
	internal class ArmorItemFactory
	{
		public IItem CreateHelmet(Material material)
		{
			return new Item(
				(language, alive) => language.Items.Armor.Helmet,
				() => 1.5m,
				ItemType.Wear,
				material.Color,
				material,
				new Wear(WearSlot.Head)
			);
		}

		public IItem CreateChestplate(Material material)
		{
			return new Item(
				(language, alive) => language.Items.Armor.Chestplate,
				() => 5m,
				ItemType.Wear,
				material.Color,
				material,
				new Wear(WearSlot.UpperBody)
			);
		}

		public IItem CreateLeggings(Material material)
		{
			return new Item(
				(language, alive) => language.Items.Armor.Leggings,
				() => 3m,
				ItemType.Wear,
				material.Color,
				material,
				new Wear(WearSlot.LowerBody)
			);
		}

		public IItem CreateBoots(Material material)
		{
			return new Item(
				(language, alive) => language.Items.Armor.Boots,
				() => 1.5m,
				ItemType.Wear,
				material.Color,
				material,
				new Wear(WearSlot.Foots)
			);
		}

		public IItem CreateGauntlets(Material material)
		{
			return new Item(
				(language, alive) => language.Items.Armor.Gauntlets,
				() => 1m,
				ItemType.Wear,
				material.Color,
				material,
				new Wear(WearSlot.Hands)
			);
		}

		public IItem CreateShield(Material material)
		{
			return new Item(
				(language, alive) => language.Items.Armor.Shield,
				() => 3m,
				ItemType.Wear,
				material.Color,
				material,
				new Wear(WearSlot.Hands)
			);
		}
	}
}
