using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;

using Roguelike.Core.Interfaces;

namespace Roguelike.Core.Aspects
{
	public class Camera : AspectWithHolder<IObject>
	{
		#region Properties

		public Cell Cell
		{ get { return Holder.CurrentCell!; } }

		public double Distance
		{ get { return _getDistance(); } }

		private readonly Func<double> _getDistance;

		public ICollection<Cell> MapMemory
		{ get; }

		public IDictionary<Cell, bool> VisibleCells
		{ get; private set; }

		public event EventHandler<Camera, IDictionary<Cell, bool>>? CellsVisibilityChanged;

		#endregion

		public Camera(IObject holder, Func<double> getDistance)
			: base(holder)
		{
			_getDistance = getDistance;
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
						ProcessCellVisibility(cell, IsNeighborTransparent(cell.Position), previouslyVisibleCells, delta);
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

		private bool IsNeighborTransparent(Vector cellPosition)
		{
			var position = Holder.GetPosition();
			var region = Holder.GetRegion();

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

	public static class CameraHelper
	{
		public static Cell[][] SelectRegionCells(this Camera camera, int screenWidth, int screenHeight)
		{
			var position = camera.Cell.Position;

			var result = new Cell[screenHeight][];
			int centerX = screenWidth / 2,
				centerY = screenHeight / 2,
				cameraZ = position.Z,
				cameraY = position.Y + centerY;

			for (int r = 0; r < screenHeight; r++)
			{
				result[r] = new Cell[screenWidth];
				int cameraX = position.X - centerX;
				for (int c = 0; c < screenWidth; c++)
				{
					result[r][c] = camera.Cell.Region.GetCell(cameraX, cameraY, cameraZ);
					cameraX++;
				}
				cameraY--;
			}

			return result;
		}

		public static void MakeMapKnown(this Camera camera, int viewDistance)
		{
			var region = camera.Cell.Region;
			var position = camera.Cell.Position;

			for (int x = position.X - viewDistance; x <= position.X + viewDistance; x++)
			{
				for (int y = position.Y - viewDistance; y <= position.Y + viewDistance; y++)
				{
					var vector = new Vector(x, y, position.Z);
					var cell = region.GetCell(vector);
					if (cell != null)
					{
						camera.MapMemory.Add(cell);
					}
				}
			}

			camera.RefreshVisibleCells();
		}
	}
}
