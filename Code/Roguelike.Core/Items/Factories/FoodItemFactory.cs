using System.Drawing;

using Roguelike.Core.Aspects;
using Roguelike.Core.Interfaces;
using Roguelike.Core.Mechanics;

namespace Roguelike.Core.Items.Factories
{
	internal class FoodItemFactory
	{
		public IItem CreateLoafOfBread()
		{
			return new Item(
				(language, alive) => language.Items.Food.LoafOfBread,
				() => 0.4m,
				ItemType.Food,
				Color.Brown,
				Material.Food,
				new Food(500, -50)
			);
		}
	}
}
