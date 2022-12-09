using Roguelike.Core.Interfaces;

namespace Roguelike.Core.Aspects
{
	public class Nutricious : IAspect
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
