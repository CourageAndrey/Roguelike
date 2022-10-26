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
		{ get; }

		public IDictionary<Cell, bool> VisibleCells
		{ get; private set; }

		public event EventHandler<ICamera, IDictionary<Cell, bool>> CellsVisibilityChanged;

		#endregion

		public HeroCamera(Hero hero)
		{
			_hero = hero;
			MapMemory = new HashSet<Cell>();
			VisibleCells = new Dictionary<Cell, bool>();
		}

		public void RefreshVisibleCells()
		{
			int viewRadius = (int) Math.Ceiling(Distance);
			double distanceSquare = Distance * Distance;
			var position = Cell.Position;

			var previouslyVisibleCells = VisibleCells;
			VisibleCells = new Dictionary<Cell, bool>();
			var delta = new Dictionary<Cell, bool>();

			ProcessCellVisibility(Cell, true, previouslyVisibleCells, delta);

			foreach (var cell in Cell.EnumerateCellsAround(1))
			{
				ProcessCellVisibility(cell, true, previouslyVisibleCells, delta);
			}

			for (int radius = 2; radius <= viewRadius; radius++)
			{
				foreach (var cell in Cell.EnumerateCellsAround(radius))
				{
					if (position.GetDistanceSquare(cell.Position) <= distanceSquare)
					{
						ProcessCellVisibility(cell, IsNeigboorTransparent(cell.Position), previouslyVisibleCells, delta);
					}
				}
			}

			foreach (var cell in previouslyVisibleCells
				.Where(p => p.Value && !VisibleCells.ContainsKey(p.Key) && !delta.ContainsKey(p.Key) /*&& position.GetDistanceSquare(p.Key.Position) > distanceSquare*/)
				.Select(p => p.Key))
			{
				delta[cell] = false;
			}

			var handler = Volatile.Read(ref CellsVisibilityChanged);
			if (handler != null)
			{
				handler(this, delta);
			}
		}

		private void ProcessCellVisibility(
			Cell cell,
			bool isVisible,
			IDictionary<Cell, bool> previousVisibility,
			IDictionary<Cell, bool> delta)
		{
			bool previous;
			if (isVisible)
			{
				VisibleCells[cell] = true;
				if (previousVisibility.TryGetValue(cell, out previous))
				{
					if (!previous)
					{
						delta[cell] = true;
					}
				}
				else
				{
					delta[cell] = true;
					MapMemory.Add(cell);
				}
			}
			else
			{
				if (previousVisibility.TryGetValue(cell, out previous))
				{
					VisibleCells[cell] = false;
					if (previous)
					{
						delta[cell] = false;
					}
				}
				else
				{
					if (MapMemory.Contains(cell))
					{
						VisibleCells[cell] = false;
					}
				}
			}
		}

		private bool IsNeigboorTransparent(Vector cellPosition)
		{
			var position = _hero.CurrentCell.Position;
			var region = _hero.CurrentCell.Region;

			const double rateMinimum = 1d/3,
						 rateMaximum = 3,
						 rateMediumMinimum = 2d/3,
						 rateMediumMaximum = 1.5;
			int dx = position.X - cellPosition.X,
				dy = position.Y - cellPosition.Y;
			int signx = Math.Sign(dx);
			int signy = Math.Sign(dy);
			var previousCells = new List<Cell>();
			if (signx == 0 || signy == 0)
			{
				previousCells.Add(region.GetCell(
					cellPosition.X + signx,
					cellPosition.Y + signy,
					position.Z));
			}
			else
			{
				double rate = ((double) Math.Abs(dy)) / Math.Abs(dx);
				if (rate >= rateMediumMinimum && rate <= rateMediumMaximum)
				{
					previousCells.Add(region.GetCell(
						cellPosition.X + signx,
						cellPosition.Y + signy,
						position.Z));
				}
				else
				{
					if (rate <= rateMaximum)
					{
						previousCells.Add(region.GetCell(
							cellPosition.X + signx,
							cellPosition.Y,
							position.Z));
					}
					if (rate >= rateMinimum)
					{
						previousCells.Add(region.GetCell(
							cellPosition.X,
							cellPosition.Y + signy,
							position.Z));
					}
				}
			}
			bool previousIsVisible;
			return previousCells.Any(cell => VisibleCells.TryGetValue(cell, out previousIsVisible) && previousIsVisible && cell.IsTransparent);
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
