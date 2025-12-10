using Roguelike.Core.Interfaces;
using Roguelike.Core.Localization;

namespace Roguelike.Core.Objects
{
	public class ItemsPile : Mechanics.Object, IItemsContainer
	{
		#region Properties

		public override bool IsSolid
		{ get { return false; } }

		public IReadOnlyCollection<IItem> Items
		{ get { return _items; } }

		private readonly List<IItem> _items;

		#endregion

		public ItemsPile(IItem firstItem)
		{
			if (firstItem == null) throw new ArgumentNullException(nameof(firstItem));

			_items = new List<IItem> { firstItem };
		}

		public void PickItem(IItem item, ICollection<IItem> inventory)
		{
			inventory.Add(item);
			_items.Remove(item);
			if (Items.Count == 0)
			{
				CurrentCell.RemoveObject(this);
			}
			// Do not update cell, because inventory owner hides current (items pile's) cell!
		}

		public void PutItem(IItem item)
		{
			_items.Add(item);
			// Do not update cell, because inventory owner hides current (items pile's) cell!
		}

		public override string GetDescription(Language language, IAlive forWhom)
		{
			return language.Objects.ItemsPile;
		}
	}
}
