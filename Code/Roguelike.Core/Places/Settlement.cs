using System;
using System.Collections.Generic;
using System.Linq;

using Roguelike.Core.Localization;
using Roguelike.Core.Mechanics;

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
			for (int z = minZ; z <= maxZ; z++)
			{
				foreach (var cell in region.GetCells(minX, maxX, minY, maxY, z))
				{
					yield return cell;
				}
			}
		}
	}
}
