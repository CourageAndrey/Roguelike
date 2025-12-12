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

		public IItem CreateApple()
		{
			return new Item(
				(language, alive) => language.Items.Food.Apple,
				() => 0.1m,
				ItemType.Food,
				Color.Red,
				Material.Food,
				new Food(200, 50)
			);
		}

		public IItem CreateMeat()
		{
			return new Item(
				(language, alive) => language.Items.Food.Meat,
				() => 0.5m,
				ItemType.Food,
				Color.DarkRed,
				Material.Food,
				new Food(800, -100)
			);
		}

		public IItem CreateCheese()
		{
			return new Item(
				(language, alive) => language.Items.Food.Cheese,
				() => 0.2m,
				ItemType.Food,
				Color.Yellow,
				Material.Food,
				new Food(400, -30)
			);
		}

		public IItem CreateFish()
		{
			return new Item(
				(language, alive) => language.Items.Food.Fish,
				() => 0.3m,
				ItemType.Food,
				Color.Cyan,
				Material.Food,
				new Food(600, -40)
			);
		}

		public IItem CreateBerries()
		{
			return new Item(
				(language, alive) => language.Items.Food.Berries,
				() => 0.05m,
				ItemType.Food,
				Color.Purple,
				Material.Food,
				new Food(150, 30)
			);
		}
	}
}
