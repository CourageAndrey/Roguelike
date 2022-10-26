using System.Collections.Generic;

namespace Roguelike.Core.Interfaces
{
	public interface ICamera
	{
		Cell Cell
		{ get; }

		double Distance
		{ get; }

		ICollection<Cell> MapMemory
		{ get; }

		IDictionary<Cell, bool> VisibleCells
		{ get; }

		event EventHandler<ICamera, IDictionary<Cell, bool>> CellsVisibilityChanged;

		void RefreshVisibleCells();
	}

	public static class CameraHelper
	{
		public static Cell[][] SelectRegionCells(this ICamera camera, int screenWidth, int screenHeight)
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
	}
}
