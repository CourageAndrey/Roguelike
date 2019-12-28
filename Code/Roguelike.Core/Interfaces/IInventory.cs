using System.Collections.Generic;
using System.Linq;

namespace Roguelike.Core.Interfaces
{
	public interface IInventory : IReadOnlyList<Item>
	{
		bool TryAdd(Item item);

		bool TryDelete(Item item);
	}

	public static class InventoryHelper
	{
		public static double GetItemsWeight(this IInventory inventory)
		{
			return inventory.Sum(i => i.Weight);
		}
	}
}
