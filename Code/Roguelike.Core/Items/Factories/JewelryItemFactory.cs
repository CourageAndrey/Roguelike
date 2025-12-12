using System.Drawing;

using Roguelike.Core.Aspects;
using Roguelike.Core.Interfaces;
using Roguelike.Core.Mechanics;

namespace Roguelike.Core.Items.Factories
{
	internal class JewelryItemFactory
	{
		public IItem CreateRing()
		{
			return new Item(
				(language, alive) => language.Items.Jewelry.Ring,
				() => 0.02m,
				ItemType.Wear,
				Color.Aquamarine,
				Material.Metal,
				new Wear(WearSlot.Jewelry)
			);
		}

		public IItem CreateAmulet()
		{
			return new Item(
				(language, alive) => language.Items.Jewelry.Amulet,
				() => 0.1m,
				ItemType.Wear,
				Color.Gold,
				Material.Metal,
				new Wear(WearSlot.Jewelry)
			);
		}

		public IItem CreateNecklace()
		{
			return new Item(
				(language, alive) => language.Items.Jewelry.Necklace,
				() => 0.15m,
				ItemType.Wear,
				Color.Silver,
				Material.Metal,
				new Wear(WearSlot.Jewelry)
			);
		}

		public IItem CreateBracelet()
		{
			return new Item(
				(language, alive) => language.Items.Jewelry.Bracelet,
				() => 0.08m,
				ItemType.Wear,
				Color.Gold,
				Material.Metal,
				new Wear(WearSlot.Jewelry)
			);
		}
	}
}
