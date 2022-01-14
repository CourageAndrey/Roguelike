using System;
using System.Collections.Generic;
using System.Linq;

namespace Roguelike.Core.Interfaces
{
	public interface ICamera
	{
		#region Properties

		Cell Cell
		{ get; }

		double Distance
		{ get; }

		HashSet<Cell> MapMemory
		{ get; }

		event Action<ICamera> Changed;

		#endregion
	}

	public static class CameraHelper
	{
		public static Cell[][] SelectRegionCells(this ICamera camera, int screenWidth, int screenHeight)
		{
			var result = new Cell[screenHeight][];
			int centerX = screenWidth / 2,
				centerY = screenHeight / 2,
				cameraZ = camera.Cell.Position.Z,
				cameraY = camera.Cell.Position.Y + centerY;
			for (int r = 0; r < screenHeight; r++)
			{
				result[r] = new Cell[screenWidth];
				int cameraX = camera.Cell.Position.X - centerX;
				for (int c = 0; c < screenWidth; c++)
				{
					result[r][c] = camera.Cell.Region.GetCell(cameraX, cameraY, cameraZ);
					cameraX++;
				}
				cameraY--;
			}
			return result;
		}

		public static ICollection<Cell> SelectVisibleCells(this ICamera camera)
		{
			var cameraPosition = camera.Cell.Position;
			var cameraRegion = camera.Cell.Region;
			double visibleDistance = camera.Distance + 1;
			int viewRadius = (int) Math.Ceiling(camera.Distance);
			var result = new Dictionary<Cell, bool>();

			for (int radius = 1; radius < viewRadius; radius++)
			{
				Vector vector;
				Cell cell;
				for (int x = cameraPosition.X - radius; x <= cameraPosition.X + radius; x++)
				{
					vector = new Vector(x, cameraPosition.Y - radius, cameraPosition.Z);
					cell = cameraRegion.GetCell(vector);
					if (cell != null && cameraPosition.GetDistance(vector) < visibleDistance)
					{
						result[cell] = radius == 1 || isNeigboorTransparent(cell.Position, camera, result);
					}
					vector = new Vector(x, cameraPosition.Y + radius, cameraPosition.Z);
					cell = cameraRegion.GetCell(vector);
					if (cell != null && cameraPosition.GetDistance(vector) < visibleDistance)
					{
						result[cell] = radius == 1 || isNeigboorTransparent(cell.Position, camera, result);
					}
				}
				for (int y = cameraPosition.Y - radius + 1; y <= cameraPosition.Y + radius - 1; y++)
				{
					vector = new Vector(cameraPosition.X - radius, y, cameraPosition.Z);
					cell = cameraRegion.GetCell(vector);
					if (cell != null && cameraPosition.GetDistance(vector) < visibleDistance)
					{
						result[cell] = radius == 1 || isNeigboorTransparent(cell.Position, camera, result);
					}
					vector = new Vector(cameraPosition.X + radius, y, cameraPosition.Z);
					cell = cameraRegion.GetCell(vector);
					if (cell != null && cameraPosition.GetDistance(vector) < visibleDistance)
					{
						result[cell] = radius == 1 || isNeigboorTransparent(cell.Position, camera, result);
					}
				}
			}

			var visibleCells = new HashSet<Cell>(result.Where(c => c.Value).Select(c => c.Key))
			{
				cameraRegion.GetCell(cameraPosition)
			};
			foreach (var visibleCell in visibleCells)
			{
				camera.MapMemory.Add(visibleCell);
			}
			return visibleCells;
		}

		private static bool isNeigboorTransparent(Vector cellPosition, ICamera camera, IDictionary<Cell, bool> knownCells)
		{
			const double rateMinimum = 1d/3,
						 rateMaximum = 3,
						 rateMediumMinimum = 2d/3,
						 rateMediumMaximum = 1.5;
			int dx = camera.Cell.Position.X - cellPosition.X,
				dy = camera.Cell.Position.Y - cellPosition.Y;
			int signx = Math.Sign(dx);
			int signy = Math.Sign(dy);
			var previousCells = new List<Cell>();
			if (signx == 0 || signy == 0)
			{
				previousCells.Add(camera.Cell.Region.GetCell(
					cellPosition.X + signx,
					cellPosition.Y + signy,
					camera.Cell.Position.Z));
			}
			else
			{
				double rate = ((double)Math.Abs(dy)) / Math.Abs(dx);
				if (rate >= rateMediumMinimum && rate <= rateMediumMaximum)
				{
					previousCells.Add(camera.Cell.Region.GetCell(
						cellPosition.X + signx,
						cellPosition.Y + signy,
						camera.Cell.Position.Z));
				}
				else
				{
					if (rate <= rateMaximum)
					{
						previousCells.Add(camera.Cell.Region.GetCell(
							cellPosition.X + signx,
							cellPosition.Y,
							camera.Cell.Position.Z));
					}
					if (rate >= rateMinimum)
					{
						previousCells.Add(camera.Cell.Region.GetCell(
							cellPosition.X,
							cellPosition.Y + signy,
							camera.Cell.Position.Z));
					}
				}
			}
			bool previousIsVisible;
			return previousCells.Any(cell => knownCells.TryGetValue(cell, out previousIsVisible) && previousIsVisible && cell.Objects.All(o => !o.IsSolid));
		}
	}
}
