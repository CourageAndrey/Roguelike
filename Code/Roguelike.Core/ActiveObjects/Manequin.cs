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

		public IHeadWear HeadWear
		{
			get { return _headWear; }
			private set { ChangeEquipmentPosition(value, ref _headWear); }
		}

		public IUpperBodyWear UpperBodyWear
		{
			get { return _upperBodyWear; }
			private set { ChangeEquipmentPosition(value, ref _upperBodyWear); }
		}

		public ILowerBodyWear LowerBodyWear
		{
			get { return _lowerBodyWear; }
			private set { ChangeEquipmentPosition(value, ref _lowerBodyWear); }
		}

		public ICoverWear CoverWear
		{
			get { return _coverWear; }
			private set { ChangeEquipmentPosition(value, ref _coverWear); }
		}

		public IHandWear HandsWear
		{
			get { return _handsWear; }
			private set { ChangeEquipmentPosition(value, ref _handsWear); }
		}

		public IFootWear FootsWear
		{
			get { return _footsWear; }
			private set { ChangeEquipmentPosition(value, ref _footsWear); }
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

			HeadWear = headWear ?? new Naked(owner);
			UpperBodyWear = upperBodyWear ?? new Naked(owner);
			LowerBodyWear = lowerBodyWear ?? new Naked(owner);
			CoverWear = coverWear ?? new Naked(owner);
			HandsWear = handsWear ?? new Naked(owner);
			FootsWear = footsWear ?? new Naked(owner);

			var jewelryCollection = new EventCollection<IJewelry>(jewelry ?? new IJewelry[0]);
			void jewelryChanged(object sender, ItemEventArgs<IJewelry> eventArgs)
			{
				RaiseEquipmentChanged();
			}
			jewelryCollection.ItemAdded += jewelryChanged;
			jewelryCollection.ItemRemoved += jewelryChanged;
			Jewelry = jewelryCollection;
		}

		private void ChangeEquipmentPosition<ItemT>(ItemT newItem, ref ItemT itemSlot)
			where ItemT : IWear
		{
			if (newItem == null) throw new ArgumentNullException(nameof(newItem));

			itemSlot = newItem;

			RaiseEquipmentChanged();
		}
	}
}
