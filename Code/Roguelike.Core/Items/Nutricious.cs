using Roguelike.Core.Interfaces;

namespace Roguelike.Core.Items
{
	public class Nutricious : IItemAspect
	{
		public int Nutricity
		{ get; }

		public int Water
		{ get; }

		public Nutricious(int nutricity, int water)
		{
			Nutricity = nutricity;
			Water = water;
		}
	}
}
