using System.Collections.Generic;
using System.Collections.ObjectModel;

using Roguelike.Core.ActiveObjects;

namespace Roguelike.Core
{
	public static class WorldGenerator
	{
		public static IReadOnlyCollection<Region> GenerateRegions(this World world, int count)
		{
			var regions = new Region[count];
			for (int r = 0; r < count; r++)
			{
				regions[r] = new Region(world);
			}
			return new ReadOnlyCollection<Region>(regions);
		}

		public static void MakeMapKnown(this Hero hero, int viewDistance)
		{
			for (int x = 0; x < viewDistance; x++)
			{
				for (int y = 0; y < viewDistance; y++)
				{
					hero.MapMemory.Add(hero.CurrentCell.Region.GetCell(x, y, 0));
				}
			}
		}
	}
}
