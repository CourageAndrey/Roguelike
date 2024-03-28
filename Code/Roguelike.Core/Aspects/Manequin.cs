﻿using System;
using System.Collections.Generic;
using System.Threading;

using Roguelike.Core.Configuration;
using Roguelike.Core.Interfaces;
using Roguelike.Core.Items;

namespace Roguelike.Core.Aspects
{
	public class Manequin : IAspect
	{
		#region Properties

		private readonly IHumanoid _holder;
		private readonly Naked _naked;

		public IItem HeadWear
		{
			get { return _headWear; }
			set { ChangeEquipmentPosition(value ?? _naked, WearSlot.Head, ref _headWear); }
		}

		public IItem UpperBodyWear
		{
			get { return _upperBodyWear; }
			set { ChangeEquipmentPosition(value ?? _naked, WearSlot.UpperBody, ref _upperBodyWear); }
		}

		public IItem LowerBodyWear
		{
			get { return _lowerBodyWear; }
			set { ChangeEquipmentPosition(value ?? _naked, WearSlot.LowerBody, ref _lowerBodyWear); }
		}

		public IItem CoverWear
		{
			get { return _coverWear; }
			set { ChangeEquipmentPosition(value ?? _naked, WearSlot.Cover, ref _coverWear); }
		}

		public IItem HandsWear
		{
			get { return _handsWear; }
			set { ChangeEquipmentPosition(value ?? _naked, WearSlot.Hands, ref _handsWear); }
		}

		public IItem FootsWear
		{
			get { return _footsWear; }
			set { ChangeEquipmentPosition(value ?? _naked, WearSlot.Foots, ref _footsWear); }
		}

		public ICollection<IItem> Jewelry
		{ get; }

		private IItem _headWear;
		private IItem _upperBodyWear;
		private IItem _lowerBodyWear;
		private IItem _coverWear;
		private IItem _handsWear;
		private IItem _footsWear;

		public event EventHandler<IHumanoid> EquipmentChanged;

		#endregion

		protected void RaiseEquipmentChanged()
		{
			var handler = Volatile.Read(ref EquipmentChanged);
			if (handler != null)
			{
				handler(_holder);
			}
		}

		public Manequin(
			IHumanoid holder,
			IItem headWear = null,
			IItem upperBodyWear = null,
			IItem lowerBodyWear = null,
			IItem coverWear = null,
			IItem handsWear = null,
			IItem footsWear = null,
			IEnumerable<IItem> jewelry = null)
		{
			if (holder == null) throw new ArgumentNullException(nameof(holder));

			_holder = holder;
			_naked = new Naked(_holder);

			HeadWear = headWear ?? _naked;
			UpperBodyWear = upperBodyWear ?? _naked;
			LowerBodyWear = lowerBodyWear ?? _naked;
			CoverWear = coverWear ?? _naked;
			HandsWear = handsWear ?? _naked;
			FootsWear = footsWear ?? _naked;

			var jewelryCollection = new EventCollection<IItem>(jewelry ?? Array.Empty<IItem>());
			jewelryCollection.ItemAdded += (sender, eventArgs) =>
			{
				eventArgs.Item.GetAspect<Wear>().RaiseEquipped(_holder);
				RaiseEquipmentChanged();
			};
			jewelryCollection.ItemRemoved += (sender, eventArgs) =>
			{
				RaiseEquipmentChanged();
				eventArgs.Item.GetAspect<Wear>().RaiseUnequipped(_holder);
			};
			Jewelry = jewelryCollection;
		}

		private void ChangeEquipmentPosition(IItem newItem, WearSlot slot, ref IItem itemSlot)
		{
			if (newItem == null) throw new ArgumentNullException(nameof(newItem));

			if (itemSlot != null && !(itemSlot is Naked))
			{
				itemSlot.GetAspect<Wear>().RaiseUnequipped(_holder);
				_holder.Inventory.Items.Add(itemSlot);
			}
			itemSlot = newItem;
			if (!(itemSlot is Naked))
			{
				if (newItem.GetAspect<Wear>().SuitableSlot == slot)
				{
					itemSlot.GetAspect<Wear>().RaiseEquipped(_holder);
					_holder.Inventory.Items.Remove(itemSlot);
				}
				else
				{
					throw new InvalidOperationException("Item does not suit slot type.");
				}
			}

			RaiseEquipmentChanged();
		}
	}

	public static class ManequinExtensions
	{
		public static IEnumerable<IItem> GetAllItems(this Manequin manequin)
		{
			if (manequin.HeadWear != null)
			{
				yield return manequin.HeadWear;
			}

			if (manequin.UpperBodyWear != null)
			{
				yield return manequin.UpperBodyWear;
			}

			if (manequin.LowerBodyWear != null)
			{
				yield return manequin.LowerBodyWear;
			}

			if (manequin.CoverWear != null)
			{
				yield return manequin.CoverWear;
			}

			if (manequin.HandsWear != null)
			{
				yield return manequin.HandsWear;
			}

			if (manequin.FootsWear != null)
			{
				yield return manequin.FootsWear;
			}

			foreach (var jewelry in manequin.Jewelry)
			{
				yield return jewelry;
			}
		}

		public static void Dress(this Manequin manequin, IItem item)
		{
			var wear = item.GetAspect<Wear>();

			if (wear.SuitableSlot == WearSlot.Head)
			{
				manequin.HeadWear = item;
			}
			else if (wear.SuitableSlot == WearSlot.UpperBody)
			{
				manequin.UpperBodyWear = item;
			}
			else if (wear.SuitableSlot == WearSlot.LowerBody)
			{
				manequin.LowerBodyWear = item;
			}
			else if (wear.SuitableSlot == WearSlot.Cover)
			{
				manequin.CoverWear = item;
			}
			else if (wear.SuitableSlot == WearSlot.Hands)
			{
				manequin.HandsWear = item;
			}
			else if (wear.SuitableSlot == WearSlot.Foots)
			{
				manequin.FootsWear = item;
			}
			else if (wear.SuitableSlot == WearSlot.Jewelry)
			{
				manequin.Jewelry.Add(item);
			}
			else
			{
				throw new NotSupportedException();
			}
		}

		public static void Undress(this Manequin manequin, IItem item)
		{
			var wear = item.GetAspect<Wear>();

			if (wear.SuitableSlot == WearSlot.Head)
			{
				manequin.HeadWear = null;
			}
			else if (wear.SuitableSlot == WearSlot.UpperBody)
			{
				manequin.UpperBodyWear = null;
			}
			else if (wear.SuitableSlot == WearSlot.LowerBody)
			{
				manequin.LowerBodyWear = null;
			}
			else if (wear.SuitableSlot == WearSlot.Cover)
			{
				manequin.CoverWear = null;
			}
			else if (wear.SuitableSlot == WearSlot.Hands)
			{
				manequin.HandsWear = null;
			}
			else if (wear.SuitableSlot == WearSlot.Foots)
			{
				manequin.FootsWear = null;
			}
			else if (wear.SuitableSlot == WearSlot.Jewelry)
			{
				manequin.Jewelry.Remove(item);
			}
			else
			{
				throw new NotSupportedException();
			}
		}

		public static int GetDressTime(this IItem item, DressTimeBalance balance)
		{
			var wear = item.GetAspect<Wear>();

			if (wear.SuitableSlot == WearSlot.Head)
			{
				return balance.HeadWear;
			}
			else if (wear.SuitableSlot == WearSlot.UpperBody)
			{
				return balance.UpperBodyWear;
			}
			else if (wear.SuitableSlot == WearSlot.LowerBody)
			{
				return balance.LowerBodyWear;
			}
			else if (wear.SuitableSlot == WearSlot.Cover)
			{
				return balance.CoverWear;
			}
			else if (wear.SuitableSlot == WearSlot.Hands)
			{
				return balance.HandsWear;
			}
			else if (wear.SuitableSlot == WearSlot.Foots)
			{
				return balance.FootsWear;
			}
			else if (wear.SuitableSlot == WearSlot.Jewelry)
			{
				return balance.Jewelry;
			}
			else
			{
				throw new NotSupportedException();
			}
		}
	}
}
