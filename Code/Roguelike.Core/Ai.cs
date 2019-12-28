using System;
using System.Collections.Generic;
using System.Linq;

namespace Roguelike.Core
{
	public static class Ai
	{
		public static IEnumerable<Vector> CalculateRoute(Region region, Vector from, Vector to)
		{
			int aiDistanceSquare = region.World.Game.Balance.AiDistance;
			aiDistanceSquare *= aiDistanceSquare;

			var viewedCells = new Dictionary<Vector, Tuple<Vector, double>> { { from, new Tuple<Vector, double>(null, 0) } };
			var stack = new Stack<Vector>();
			stack.Push(from);
			do
			{
				var currentCell = stack.Pop();
				var currentPath = viewedCells[currentCell];
				if (currentPath.Item2 < aiDistanceSquare)
				{
					var cellsToCheck = region.GetCellsToMove(currentCell);
					foreach (var v in cellsToCheck.Keys.ToList())
					{
						double newDistance = currentPath.Item2 + cellsToCheck[v];
						Tuple<Vector, double> viewedNode;
						if (viewedCells.TryGetValue(v, out viewedNode))
						{
							if (viewedNode.Item2 > newDistance)
							{
								viewedCells[v] = new Tuple<Vector, double>(currentCell, newDistance);
							}
						}
						else
						{
							viewedCells[v] = new Tuple<Vector, double>(currentCell, newDistance);
							var objects = region.GetCell(v).Objects;
							if (objects.Count == 0 || objects.All(o => !o.IsSolid))
							{
								stack.Push(v);
							}
						}
					}
				}
			} while (stack.Count > 0);

			Tuple<Vector, double> result;
			if (viewedCells.TryGetValue(to, out result))
			{
				stack = new Stack<Vector>();
				while (result.Item1 != null)
				{
					stack.Push(result.Item1);
					result = viewedCells[result.Item1];
				}

				foreach (var vector in stack)
				{
					yield return vector;
				}
			}
		}
	}
}
