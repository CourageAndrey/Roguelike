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

		public List<IItem> Items
		{ get; }

		#endregion

		public ItemsPile(IItem firstItem)
		{
			if (firstItem == null) throw new ArgumentNullException(nameof(firstItem));

			Items = new List<IItem> { firstItem };
		}

		#region Implementation of IInteractive

		public List<Interaction> GetAvailableInteractions(Object actor)
		{
			var game = CurrentCell.Region.World.Game;
			var balance = game.Balance;
			var language = game.Language;
			return new List<Interaction>
			{
				new Interaction(language.InteractionPickItem, actor is IAlive, a =>
				{
					IItem added = null;
					if (Items.Count == 1)
					{
						added = Items[0];
					}
					else // if (Items.Count > 1)
					{
						var items = Items.Select(i => new ListItem<IItem>(i, i.ToString()));

						ListItem selectedItem;
						if (game.UserInterface.TrySelectItem(game, language.SelectItemToPickPromt, items, out selectedItem))
						{
							added = (IItem) selectedItem.ValueObject;
						}
					}

					if (added != null)
					{
						(a as IAlive).Inventory.Add(added);
						Items.Remove(added);
						if (Items.Count == 0)
						{
							CurrentCell.RemoveObject(this);
						}
						else
						{
							CurrentCell.RefreshView(false);
						}

						return new ActionResult(
							Time.FromTicks(balance.Time, balance.ActionLongevity.PickItem),
							string.Format(CultureInfo.InvariantCulture, language.LogActionFormatPickItem, a, added));
					}
					else
					{
						return new ActionResult(
							Time.FromTicks(balance.Time, balance.ActionLongevity.Disabled),
							string.Format(CultureInfo.InvariantCulture, language.LogActionFormatPickItemDisabled, a));
					}
				}),
			};
		}

		#endregion
	}
}
