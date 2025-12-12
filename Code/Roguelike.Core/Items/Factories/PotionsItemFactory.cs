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
				(language, alive) => language.Items.BottleOFWater,
				() => 0.6m,
				ItemType.Potion,
				Color.DodgerBlue,
				Material.Liquid,
				new Drink(0, 500)
			);
		}
	}
}
