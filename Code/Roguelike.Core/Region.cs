using System.Collections.Generic;
using System.Linq;
using Roguelike.Core.Configuration;
using Roguelike.Core.Interfaces;

namespace Roguelike.Core
{
	public class Region
	{
		#region Properties

		public World World
		{ get; }

		public Weather Weather
		{ get; }

		public Vector Size
		{ get; }

		private readonly Cell[,,] _cells;

		#endregion

		public Region(World world, WorldSizeBalance balance)
		{
			World = world;
			Size = new Vector(
				balance.RegionXdimension,
				balance.RegionYdimension,
				balance.RegionZdimension);
			_cells = new Cell[Size.X, Size.Y, Size.Z];
			for (int x = 0; x < Size.X; x++)
			{
				for (int y = 0; y < Size.Y; y++)
				{
					for (int z = 0; z < Size.Z; z++)
					{
						_cells[x, y, z] = new Cell(this, new Vector(x, y, z), CellBackground.Grass);
					}
				}
			}
			Weather = new Weather(this);
		}

		#region GetCell-methods

		public Cell GetCell(Vector v)
		{
			return GetCell(v.X, v.Y, v.Z);
		}

		public Cell GetCell(int x, int y, int z)
		{
			return x >= 0 && y >= 0 && z >= 0 && x < Size.X && y < Size.Y && z < Size.Z
				? _cells[x, y, z]
				: null;
		}

		public Dictionary<Vector, double> GetCellsToMove(Vector v)
		{
			var result = new Dictionary<Vector, double>();
			for (int dx = -1; dx <= 1; dx++)
			{
				for (int dy = -1; dy <= 1; dy++)
				{
					for (int dz = -1; dz <= 1; dz++)
					{
						if (dx != 0 || dy != 0 || dz != 0)
						{
							var cell = GetCell(
								v.X + dx,
								v.Y + dy,
								v.Z + dz);
							if (cell != null)
							{
								result[cell.Position] = v.GetDistanceSquare(cell.Position);
							}
						}
					}
				}
			}
			return result;
			
		}

		public Dictionary<Vector, double> GetCellsToMove(int x, int y, int z)
		{
			return GetCellsToMove(new Vector(x, y, z));
		}

		public Dictionary<Direction, Cell> GetCellsAroundPoint(Vector v)
		{
			return GetCellsAroundPoint(v.X, v.Y, v.Z);
		}

		public Dictionary<Direction, Cell> GetCellsAroundPoint(int x, int y, int z)
		{
			var result = new Dictionary<Direction, Cell>();

			var cell = GetCell(x - 1, y + 1, z);
			if (cell != null)
			{
				result[Direction.UpLeft] = cell;
			}
			cell = GetCell(x, y + 1, z);
			if (cell != null)
			{
				result[Direction.Up] = cell;
			}
			cell = GetCell(x + 1, y + 1, z);
			if (cell != null)
			{
				result[Direction.UpRight] = cell;
			}
			cell = GetCell(x - 1, y, z);
			if (cell != null)
			{
				result[Direction.Left] = cell;
			}
			cell = GetCell(x, y, z);
			if (cell != null)
			{
				result[Direction.None] = cell;
			}
			cell = GetCell(x + 1, y, z);
			if (cell != null)
			{
				result[Direction.Right] = cell;
			}
			cell = GetCell(x - 1, y - 1, z);
			if (cell != null)
			{
				result[Direction.DownLeft] = cell;
			}
			cell = GetCell(x, y - 1, z);
			if (cell != null)
			{
				result[Direction.Down] = cell;
			}
			cell = GetCell(x + 1, y - 1, z);
			if (cell != null)
			{
				result[Direction.DownRight] = cell;
			}

			return result;
		}

		#endregion

		#region Step performing

		public void Start()
		{
			foreach (var a in GetActiveObjects(false))
			{
				a.NextActionTime = World.Time;
			}
		}

		internal Queue<Active> GetActiveObjects(bool orderByNextActionTime = true)
		{
			if (_activeCache == null)
			{
				var actives = new List<Active>();
				for (int x = 0; x < Size.X; x++)
				{
					for (int y = 0; y < Size.Y; y++)
					{
						for (int z = 0; z < Size.Z; z++)
						{
							actives.AddRange(_cells[x, y, z].Objects.OfType<Active>());
						}
					}
				}
				if (orderByNextActionTime)
				{
					actives = actives.OrderBy(a => a.NextActionTime).ToList();
				}

				foreach (var active in actives)
				{
					active.OnLogMessage += OnLogMessage;
				}

				_activeCache = new Queue<Active>(actives);
			}
			return _activeCache;
		}

		internal void ResetActiveCache()
		{
			if (_activeCache != null)
			{
				foreach (var active in _activeCache)
				{
					active.OnLogMessage -= OnLogMessage;
				}
			}
			_activeCache = null;
		}

		private Queue<Active> _activeCache;

		private void OnLogMessage(IObject sender, ICollection<string> messages)
		{
			foreach (string line in messages)
			{
				World.Game.WriteLog(line);
			}
		}

		#endregion
	}
}
