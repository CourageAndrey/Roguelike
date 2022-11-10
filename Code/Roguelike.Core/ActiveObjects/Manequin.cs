using System;
using System.Collections.Generic;
using System.Threading;

using Roguelike.Core.Interfaces;
using Roguelike.Core.Items;

namespace Roguelike.Core.ActiveObjects
{
	public class Manequin : IManequin
	{
		#region Properties

		private readonly IHumanoid _owner;
		private readonly Naked _naked;

		public IWear HeadWear
		{
			get { return _headWear; }
			set { ChangeEquipmentPosition(value ?? new Naked(_owner), WearSlot.Head, ref _headWear); }
		}

		public IWear UpperBodyWear
		{
			get { return _upperBodyWear; }
			set { ChangeEquipmentPosition(value ?? new Naked(_owner), WearSlot.UpperBody, ref _upperBodyWear); }
		}

		public IWear LowerBodyWear
		{
			get { return _lowerBodyWear; }
			set { ChangeEquipmentPosition(value ?? new Naked(_owner), WearSlot.LowerBody, ref _lowerBodyWear); }
		}

		public IWear CoverWear
		{
			get { return _coverWear; }
			set { ChangeEquipmentPosition(value ?? new Naked(_owner), WearSlot.Cover, ref _coverWear); }
		}

		public IWear HandsWear
		{
			get { return _handsWear; }
			set { ChangeEquipmentPosition(value ?? new Naked(_owner), WearSlot.Hands, ref _handsWear); }
		}

		public IWear FootsWear
		{
			get { return _footsWear; }
			set { ChangeEquipmentPosition(value ?? new Naked(_owner), WearSlot.Foots, ref _footsWear); }
		}

		public ICollection<IWear> Jewelry
		{ get; }

		private IWear _headWear;
		private IWear _upperBodyWear;
		private IWear _lowerBodyWear;
		private IWear _coverWear;
		private IWear _handsWear;
		private IWear _footsWear;

		public event EventHandler<IManequin> EquipmentChanged;

		#endregion

		protected void RaiseEquipmentChanged()
		{
			var handler = Volatile.Read(ref EquipmentChanged);
			if (handler != null)
			{
				handler(this);
			}
		}

		public Manequin(
			IHumanoid owner,
			IWear headWear = null,
			IWear upperBodyWear = null,
			IWear lowerBodyWear = null,
			IWear coverWear = null,
			IWear handsWear = null,
			IWear footsWear = null,
			IEnumerable<IWear> jewelry = null)
		{
			if (owner == null) throw new ArgumentNullException(nameof(owner));

			_owner = owner;
			_naked = new Naked(_owner);

			HeadWear = headWear ?? _naked;
			UpperBodyWear = upperBodyWear ?? _naked;
			LowerBodyWear = lowerBodyWear ?? _naked;
			CoverWear = coverWear ?? _naked;
			HandsWear = handsWear ?? _naked;
			FootsWear = footsWear ?? _naked;

			var jewelryCollection = new EventCollection<IWear>(jewelry ?? new IWear[0]);
			jewelryCollection.ItemAdded += (sender, eventArgs) =>
			{
				_owner.Inventory.Remove(eventArgs.Item);
				eventArgs.Item.RaiseEquipped(_owner);
				RaiseEquipmentChanged();
			};
			jewelryCollection.ItemRemoved += (sender, eventArgs) =>
			{
				_owner.Inventory.Add(eventArgs.Item);
				RaiseEquipmentChanged();
				eventArgs.Item.RaiseUnequipped(_owner);
			};
			Jewelry = jewelryCollection;
		}

		private void ChangeEquipmentPosition(IWear newItem, WearSlot slot, ref IWear itemSlot)
		{
			if (newItem == null) throw new ArgumentNullException(nameof(newItem));

			if (itemSlot != null && !(itemSlot is Naked))
			{
				itemSlot.RaiseUnequipped(_owner);
				_owner.Inventory.Add(itemSlot);
			}
			itemSlot = newItem;
			if (!(itemSlot is Naked))
			{
				if (newItem.SuitableSlot == slot)
				{
					itemSlot.RaiseEquipped(_owner);
					_owner.Inventory.Remove(itemSlot);
				}
				else
				{
					throw new InvalidOperationException("Item doe not suit slot type.");
				}
			}

			RaiseEquipmentChanged();
		}
	}
}
