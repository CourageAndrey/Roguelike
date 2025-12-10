using Roguelike.Core.Interfaces;
using Roguelike.Core.Mechanics;

namespace Roguelike.Core.Aspects
{
	public class WaterSource : IAspect
	{
		public ActionResult? Drink(IAlive who)
		{
			int water = who.State.GetWaterToFull();
			if (water > 0)
			{
				return who.Drink(CreateDrink(water));
			}
			else
			{
				return null;
			}
#warning Take vermins into account.
		}

		private static IItem CreateDrink(int water)
		{
			return new Item(
				(language, alive) => language.Objects.Pool,
				() => 0,
				ItemType.Potion,
				Material.Liquid.Color,
				Material.Liquid,
				new Drink(0, water));
		}
	}
}
