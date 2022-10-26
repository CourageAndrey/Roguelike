using System.Collections.Generic;

namespace Roguelike.Core.Interfaces
{
	public interface IItemsContainer
	{
		IReadOnlyCollection<IItem> Items
		{ get; }

		void PickItem(IItem item, ICollection<IItem> inventory);

		void PutItem(IItem item);
	}
}
