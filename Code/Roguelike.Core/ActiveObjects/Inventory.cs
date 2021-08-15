using System.Collections.Generic;

using Roguelike.Core.Interfaces;

namespace Roguelike.Core.ActiveObjects
{
	public class Inventory : IInventory
	{
		private readonly List<IItem> items = new List<IItem>();

		public bool TryAdd(IItem item)
		{
			items.Add(item);
			return true;
		}

		public bool TryDelete(IItem item)
		{
			items.Remove(item);
			return true;
		}

		public IItem this[int index]
		{ get { return items[index]; } }

		public int Count
		{ get { return items.Count; } }

		public IEnumerator<IItem> GetEnumerator()
		{
			return items.GetEnumerator();
		}

		System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
		{
			return GetEnumerator();
		}
	}
}
