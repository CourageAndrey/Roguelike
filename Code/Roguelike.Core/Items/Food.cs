using Roguelike.Core.Interfaces;

namespace Roguelike.Core.Items
{
	public class Food : IItemAspect
	{
		public int Nutricity
		{ get; }

		public int Water
		{ get; }

		public Food(int nutricity, int water)
		{
			Nutricity = nutricity;
			Water = water;
		}
	}
}
