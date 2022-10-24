using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;

using Roguelike.Core.Interfaces;

namespace Roguelike.Core.ActiveObjects
{
	public class HeroCamera : ICamera
	{
		#region Properties

		private readonly Hero _hero;

		public Cell Cell
		{ get { return _hero.CurrentCell; } }

		public double Distance
		{ get { return _hero.Properties.Perception; } }

		public ICollection<Cell> MapMemory
		{ get; } = new HashSet<Cell>();

		public event EventHandler<ICamera> Changed;

		public void Refresh()
		{
			var handler = Volatile.Read(ref Changed);
			if (handler != null)
			{
				handler(this);
			}
		}

		#endregion

		public HeroCamera(Hero hero)
		{
			_hero = hero;
		}

		public ICollection<Cell> SelectVisibleCells()
		{
			var cameraPosition = Cell.Position;
			var cameraRegion = Cell.Region;
			double visibleDistance = Distance + 1;
			int viewRadius = (int) Math.Ceiling(Distance);
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
						result[cell] = radius == 1 || isNeigboorTransparent(cell.Position, result);
					}
					vector = new Vector(x, cameraPosition.Y + radius, cameraPosition.Z);
					cell = cameraRegion.GetCell(vector);
					if (cell != null && cameraPosition.GetDistance(vector) < visibleDistance)
					{
						result[cell] = radius == 1 || isNeigboorTransparent(cell.Position, result);
					}
				}
				for (int y = cameraPosition.Y - radius + 1; y <= cameraPosition.Y + radius - 1; y++)
				{
					vector = new Vector(cameraPosition.X - radius, y, cameraPosition.Z);
					cell = cameraRegion.GetCell(vector);
					if (cell != null && cameraPosition.GetDistance(vector) < visibleDistance)
					{
						result[cell] = radius == 1 || isNeigboorTransparent(cell.Position, result);
					}
					vector = new Vector(cameraPosition.X + radius, y, cameraPosition.Z);
					cell = cameraRegion.GetCell(vector);
					if (cell != null && cameraPosition.GetDistance(vector) < visibleDistance)
					{
						result[cell] = radius == 1 || isNeigboorTransparent(cell.Position, result);
					}
				}
			}

			var visibleCells = new HashSet<Cell>(result.Where(c => c.Value).Select(c => c.Key))
			{
				cameraRegion.GetCell(cameraPosition)
			};
			foreach (var visibleCell in visibleCells)
			{
				MapMemory.Add(visibleCell);
			}
			return visibleCells;
		}

		private bool isNeigboorTransparent(Vector cellPosition, IDictionary<Cell, bool> knownCells)
		{
			const double rateMinimum = 1d/3,
						 rateMaximum = 3,
						 rateMediumMinimum = 2d/3,
						 rateMediumMaximum = 1.5;
			int dx = Cell.Position.X - cellPosition.X,
				dy = Cell.Position.Y - cellPosition.Y;
			int signx = Math.Sign(dx);
			int signy = Math.Sign(dy);
			var previousCells = new List<Cell>();
			if (signx == 0 || signy == 0)
			{
				previousCells.Add(Cell.Region.GetCell(
					cellPosition.X + signx,
					cellPosition.Y + signy,
					Cell.Position.Z));
			}
			else
			{
				double rate = ((double)Math.Abs(dy)) / Math.Abs(dx);
				if (rate >= rateMediumMinimum && rate <= rateMediumMaximum)
				{
					previousCells.Add(Cell.Region.GetCell(
						cellPosition.X + signx,
						cellPosition.Y + signy,
						Cell.Position.Z));
				}
				else
				{
					if (rate <= rateMaximum)
					{
						previousCells.Add(Cell.Region.GetCell(
							cellPosition.X + signx,
							cellPosition.Y,
							Cell.Position.Z));
					}
					if (rate >= rateMinimum)
					{
						previousCells.Add(Cell.Region.GetCell(
							cellPosition.X,
							cellPosition.Y + signy,
							Cell.Position.Z));
					}
				}
			}
			bool previousIsVisible;
			return previousCells.Any(cell => knownCells.TryGetValue(cell, out previousIsVisible) && previousIsVisible && cell.IsTransparent);
		}
	}
}
