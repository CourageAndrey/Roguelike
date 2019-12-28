using System.Collections.Generic;

using Roguelike.Core.Interfaces;

namespace Roguelike.Core.ActiveObjects
{
	public class Inventory : IInventory
	{
		private readonly List<Item> items = new List<Item>();

		public bool TryAdd(Item item)
		{
			items.Add(item);
			return true;
		}

		public bool TryDelete(Item item)
		{
			items.Remove(item);
			return true;
		}

		public Item this[int index]
		{ get { return items[index]; } }

		public int Count
		{ get { return items.Count; } }

		public IEnumerator<Item> GetEnumerator()
		{
			return items.GetEnumerator();
		}

		System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
		{
			return GetEnumerator();
		}
	}
}
