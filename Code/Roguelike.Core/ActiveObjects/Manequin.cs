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

		public IHeadWear HeadWear
		{
			get { return _headWear; }
			set { ChangeEquipmentPosition(value ?? new Naked(_owner), ref _headWear); }
		}

		public IUpperBodyWear UpperBodyWear
		{
			get { return _upperBodyWear; }
			set { ChangeEquipmentPosition(value ?? new Naked(_owner), ref _upperBodyWear); }
		}

		public ILowerBodyWear LowerBodyWear
		{
			get { return _lowerBodyWear; }
			set { ChangeEquipmentPosition(value ?? new Naked(_owner), ref _lowerBodyWear); }
		}

		public ICoverWear CoverWear
		{
			get { return _coverWear; }
			set { ChangeEquipmentPosition(value ?? new Naked(_owner), ref _coverWear); }
		}

		public IHandWear HandsWear
		{
			get { return _handsWear; }
			set { ChangeEquipmentPosition(value ?? new Naked(_owner), ref _handsWear); }
		}

		public IFootWear FootsWear
		{
			get { return _footsWear; }
			set { ChangeEquipmentPosition(value ?? new Naked(_owner), ref _footsWear); }
		}

		public ICollection<IJewelry> Jewelry
		{ get; }

		private IHeadWear _headWear;
		private IUpperBodyWear _upperBodyWear;
		private ILowerBodyWear _lowerBodyWear;
		private ICoverWear _coverWear;
		private IHandWear _handsWear;
		private IFootWear _footsWear;

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
			IHeadWear headWear = null,
			IUpperBodyWear upperBodyWear = null,
			ILowerBodyWear lowerBodyWear = null,
			ICoverWear coverWear = null,
			IHandWear handsWear = null,
			IFootWear footsWear = null,
			IEnumerable<IJewelry> jewelry = null)
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

			var jewelryCollection = new EventCollection<IJewelry>(jewelry ?? new IJewelry[0]);
			jewelryCollection.ItemAdded += (sender, eventArgs) =>
			{
				eventArgs.Item.RaiseEquipped(_owner);
				RaiseEquipmentChanged();
			};
			jewelryCollection.ItemRemoved += (sender, eventArgs) =>
			{
				RaiseEquipmentChanged();
				eventArgs.Item.RaiseUnequipped(_owner);
			};
			Jewelry = jewelryCollection;
		}

		private void ChangeEquipmentPosition<ItemT>(ItemT newItem, ref ItemT itemSlot)
			where ItemT : IWear
		{
			if (newItem == null) throw new ArgumentNullException(nameof(newItem));

			itemSlot?.RaiseUnequipped(_owner);
			itemSlot = newItem;
			itemSlot.RaiseEquipped(_owner);

			RaiseEquipmentChanged();
		}
	}
}
