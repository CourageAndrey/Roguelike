using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;

using Roguelike.Core.Interfaces;

namespace Roguelike.Core.StaticObjects
{
	public class ItemsPile : Object, IInteractive
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

		#region Implementation of IInteractive

		public void AddItem(IItem item)
		{
			_items.Add(item);
		}

		public List<Interaction> GetAvailableInteractions(Object actor)
		{
			var game = CurrentCell.Region.World.Game;
			var balance = game.Balance;
			var language = game.Language;
			return new List<Interaction>
			{
				new Interaction(language.Interactions.PickItem, actor is IAlive, a =>
				{
					IItem added = null;
					if (_items.Count == 1)
					{
						added = _items[0];
					}
					else // if (_items.Count > 1)
					{
						var items = _items.Select(i => new ListItem<IItem>(i, i.ToString()));

						ListItem selectedItem;
						if (game.UserInterface.TrySelectItem(game, language.Promts.SelectItemToPick, items, out selectedItem))
						{
							added = (IItem) selectedItem.ValueObject;
						}
					}

					if (added != null)
					{
						(a as IAlive).Inventory.Add(added);
						_items.Remove(added);
						if (_items.Count == 0)
						{
							CurrentCell.RemoveObject(this);
						}
						else
						{
							CurrentCell.RefreshView(false);
						}

						return new ActionResult(
							Time.FromTicks(balance.Time, balance.ActionLongevity.PickItem),
							string.Format(CultureInfo.InvariantCulture, language.LogActionFormats.PickItem, a, added));
					}
					else
					{
						return new ActionResult(
							Time.FromTicks(balance.Time, balance.ActionLongevity.Disabled),
							string.Format(CultureInfo.InvariantCulture, language.LogActionFormats.PickItemDisabled, a));
					}
				}),
			};
		}

		#endregion
	}
}
