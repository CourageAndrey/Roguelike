using System.Collections.ObjectModel;

namespace Roguelike.Core.Mechanics
{
	public class Cell
	{
		#region Properties

		public Vector Position
		{ get; }

		public Region Region
		{ get; }

		public CellEnvironment Environment
		{ get; private set; }

		public CellBackground Background
		{ get; private set; }

		public IReadOnlyCollection<Object> Objects
		{ get { return _objectsView ??= new ReadOnlyCollection<Object>(_objects); } }

		public bool IsTransparent
		{ get { return Objects.All(o => !o.IsSolid); } }

		public event EventHandler<Cell, bool>? ViewChanged;

		private readonly List<Object> _objects = new List<Object>();
		private IReadOnlyCollection<Object>? _objectsView;

		#endregion

		public Cell(Region region, Vector position, CellBackground background)
		{
			Region = region;
			Environment = region.DefaultCellEnvironment;
			Position = position;
			Background = background;
		}

		internal void RemoveObject(Object o, bool refreshTransparency = true)
		{
			bool isTransparent = IsTransparent;
			_objects.Remove(o);
			RefreshView(refreshTransparency && isTransparent != IsTransparent);
		}

		internal void AddObject(Object o, bool refreshTransparency = true)
		{
			bool isTransparent = IsTransparent;
			_objects.Add(o);
			RefreshView(refreshTransparency && isTransparent != IsTransparent);
		}

		internal void RefreshView(bool transparencyChanged)
		{
			_objectsView = null;
			var handler = Volatile.Read(ref ViewChanged);
			if (handler != null)
			{
				handler(this, transparencyChanged);
			}
		}

		public Object? GetTopVisibleObject()
		{
			Object? found = null;
			foreach (var o in _objects)
			{
				if (o.IsSolid)
				{
					return o;
				}
				else
				{
					found = o;
				}
			}
			return found;
		}

		public void ChangeBackground(CellBackground background)
		{
			if (Background != background)
			{
				Background = background;
				RefreshView(false);
			}
		}

		public void MakeInterior(InteriorCellEnvironment environment)
		{
			Environment = environment;
		}
	}

	public static class CellExtensions
	{
		public static IEnumerable<Cell> EnumerateCellsAround(this Cell center, int radius)
		{
			if (center == null) throw new ArgumentNullException(nameof(center));
			if (radius == 0) throw new ArgumentOutOfRangeException(nameof(radius));

			var position = center.Position;
			var region = center.Region;

			Vector vector;
			Cell? cell;

			for (int x = position.X - radius; x <= position.X + radius; x++)
			{
				vector = new Vector(x, position.Y - radius, position.Z);
				cell = region.GetCell(vector);
				if (cell != null)
				{
					yield return cell;
				}

				vector = new Vector(x, position.Y + radius, position.Z);
				cell = region.GetCell(vector);
				if (cell != null)
				{
					yield return cell;
				}
			}

			for (int y = position.Y - radius; y <= position.Y + radius; y++)
			{
				vector = new Vector(position.X - radius, y, position.Z);
				cell = region.GetCell(vector);
				if (cell != null)
				{
					yield return cell;
				}

				vector = new Vector(position.X + radius, y, position.Z);
				cell = region.GetCell(vector);
				if (cell != null)
				{
					yield return cell;
				}
			}
		}

		public static Cell? FindClosest(this Cell center, Func<Cell, bool> predicate, int maxRadius = int.MaxValue)
		{
			if (center == null) throw new ArgumentNullException(nameof(center));
			if (predicate == null) throw new ArgumentNullException(nameof(predicate));
			if (maxRadius <= 0) throw new ArgumentOutOfRangeException(nameof(maxRadius));

			for (int radius = 1; radius <= maxRadius; radius ++)
			{
				foreach (var cell in center.EnumerateCellsAround(radius))
				{
					if (predicate(cell))
					{
						return cell;
					}
				}
			}

			return null;
		}
	}
}
