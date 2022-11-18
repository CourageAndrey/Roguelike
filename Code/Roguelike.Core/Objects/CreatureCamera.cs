using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;

using Roguelike.Core.Interfaces;

namespace Roguelike.Core.Objects
{
	public class CreatureCamera : ICamera
	{
		#region Properties

		private readonly IAlive _holder;

		public Cell Cell
		{ get { return _holder.CurrentCell; } }

		public double Distance
		{ get { return _holder.Properties.Perception; } }

		public ICollection<Cell> MapMemory
		{ get; }

		public IDictionary<Cell, bool> VisibleCells
		{ get; private set; }

		public event EventHandler<ICamera, IDictionary<Cell, bool>> CellsVisibilityChanged;

		#endregion

		public CreatureCamera(IAlive holder)
		{
			_holder = holder;
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
			var position = _holder.CurrentCell.Position;
			var region = _holder.CurrentCell.Region;

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
	}
}
