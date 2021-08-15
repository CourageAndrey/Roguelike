using System.Collections.Generic;
using System.Collections.ObjectModel;

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
	}
}
