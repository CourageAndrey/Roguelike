using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading;

namespace Roguelike.Core
{
	public class Cell
	{
		#region Properties

		public Vector Position
		{ get; }

		public Region Region
		{ get; }

		public CellBackground Background
		{ get; private set; }

		public IReadOnlyCollection<Object> Objects
		{ get { return objectsView ?? (objectsView = new ReadOnlyCollection<Object>(objects)); } }

		public event Action<Cell> ViewChanged;

		private readonly List<Object> objects = new List<Object>();
		private IReadOnlyCollection<Object> objectsView;

		#endregion

		public Cell(Region region, Vector position, CellBackground background)
		{
			Region = region;
			Position = position;
			Background = background;
		}

		internal void RemoveObject(Object o)
		{
			objects.Remove(o);
			RefreshView();
		}

		internal void AddObject(Object o)
		{
			objects.Add(o);
			RefreshView();
		}

		internal void RefreshView()
		{
			objectsView = null;
			var handler = Volatile.Read(ref ViewChanged);
			if (handler != null)
			{
				handler(this);
			}
		}

		public Object GetTopVisibleObject()
		{
			Object found = null;
			foreach (var o in objects)
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
				RefreshView();
			}
		}
	}
}
