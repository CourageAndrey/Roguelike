using System.Drawing;

using Roguelike.Core.Aspects;
using Roguelike.Core.Interfaces;
using Roguelike.Core.Mechanics;

namespace Roguelike.Core.Items.Factories
{
	internal class SpecialItemFactory
	{
		public IItem CreateGrass(int nutricity)
		{
			return new Item(
				(language, alive) => language.Items.Grass,
				() => 0,
				ItemType.Food,
				Color.DarkGreen,
				Material.Food,
				new Food(nutricity, nutricity / 8)
			);
		}
	}
}
