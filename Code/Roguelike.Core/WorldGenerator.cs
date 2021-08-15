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
			var region = hero.CurrentCell.Region;
			for (int x = -viewDistance; x < viewDistance; x++)
			{
				for (int y = -viewDistance; y < viewDistance; y++)
				{
					var cell = region.GetCell(hero.CurrentCell.Position.X + x, hero.CurrentCell.Position.Y + y, 0);
					if (cell != null)
					{
						hero.MapMemory.Add(cell);
					}
				}
			}
		}
	}
}
