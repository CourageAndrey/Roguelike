using System;
using System.Collections.Generic;
using System.Linq;

using Roguelike.Core.Localization;

namespace Roguelike.Core.Places
{
	public class Settlement : Place
	{
		private readonly Func<Language, string> _getName;

		public Settlement(IEnumerable<House> houses, Func<Language, string> getName)
			: base(GetBounds(houses))
		{
			_getName = getName;
		}

		public string GetName(Language language)
		{
			return _getName(language);
		}

		private static IEnumerable<Cell> GetBounds(IEnumerable<House> houses)
		{
			int minX = int.MaxValue, minY = int.MaxValue, minZ = int.MaxValue, maxX = int.MinValue, maxY = int.MinValue, maxZ = int.MinValue;
			foreach (var house in houses)
			{
				foreach (var cell in house.Cells)
				{
					var position = cell.Position;

					if (position.X < minX)
					{
						minX = position.X;
					}
					if (position.X > maxX)
					{
						maxX = position.X;
					}

					if (position.Y < minY)
					{
						minY = position.Y;
					}
					if (position.Y > maxY)
					{
						maxY = position.Y;
					}

					if (position.Z < minZ)
					{
						minZ = position.Z;
					}
					if (position.Z > maxZ)
					{
						maxZ = position.Z;
					}
				}
			}

			var region = houses.First().Cells.First().Region;
			for (int x = minX; x <= maxX; x++)
			{
				for (int y = minY; y <= maxY; y++)
				{
					for (int z = minZ; z <= maxZ; z++)
					{
						var cell = region.GetCell(x, y, z);
						if (cell != null)
						{
							yield return cell;
						}
					}
				}
			}
		}
	}
}
