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
	}
}
