using Roguelike.Core.ActiveObjects;
using Roguelike.Core.Interfaces;
using Roguelike.Core.Items;
using Roguelike.Core.Localization;

namespace Roguelike.Core.StaticObjects
{
	public class Pool : Object, IWaterSource
	{
		#region Properties

		public override bool IsSolid
		{ get { return false; } }

		#endregion

		public ActionResult Drink(IAlive who)
		{
#warning Avoid typecast below.
			int water = ((State) who.State).WaterToFull;
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

		public override string GetDescription(Language language, IAlive forWhom)
		{
			return language.Objects.Pool;
		}
	}
}
