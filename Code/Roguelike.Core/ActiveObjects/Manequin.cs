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

		public IHeadWear HeadWear
		{
			get { return _headWear; }
			set { ChangeEquipmentPosition(value, ref _headWear); }
		}

		public IUpperBodyWear UpperBodyWear
		{
			get { return _upperBodyWear; }
			set { ChangeEquipmentPosition(value, ref _upperBodyWear); }
		}

		public ILowerBodyWear LowerBodyWear
		{
			get { return _lowerBodyWear; }
			set { ChangeEquipmentPosition(value, ref _lowerBodyWear); }
		}

		public ICoverWear CoverWear
		{
			get { return _coverWear; }
			set { ChangeEquipmentPosition(value, ref _coverWear); }
		}

		public IHandWear HandsWear
		{
			get { return _handsWear; }
			set { ChangeEquipmentPosition(value, ref _handsWear); }
		}

		public IFootWear FootsWear
		{
			get { return _footsWear; }
			set { ChangeEquipmentPosition(value, ref _footsWear); }
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

			HeadWear = headWear ?? new Naked(owner);
			UpperBodyWear = upperBodyWear ?? new Naked(owner);
			LowerBodyWear = lowerBodyWear ?? new Naked(owner);
			CoverWear = coverWear ?? new Naked(owner);
			HandsWear = handsWear ?? new Naked(owner);
			FootsWear = footsWear ?? new Naked(owner);

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
