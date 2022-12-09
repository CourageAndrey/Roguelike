using System;
using System.Collections.Generic;
using System.Threading;

using Roguelike.Core.Aspects;
using Roguelike.Core.Interfaces;
using Roguelike.Core.Items;

namespace Roguelike.Core.ActiveObjects
{
	public class Manequin : IManequin
	{
		#region Properties

		private readonly IHumanoid _owner;
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
			IItem headWear = null,
			IItem upperBodyWear = null,
			IItem lowerBodyWear = null,
			IItem coverWear = null,
			IItem handsWear = null,
			IItem footsWear = null,
			IEnumerable<IItem> jewelry = null)
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

			var jewelryCollection = new EventCollection<IItem>(jewelry ?? new IItem[0]);
			jewelryCollection.ItemAdded += (sender, eventArgs) =>
			{
				eventArgs.Item.GetAspect<Wear>().RaiseEquipped(_owner);
				RaiseEquipmentChanged();
			};
			jewelryCollection.ItemRemoved += (sender, eventArgs) =>
			{
				RaiseEquipmentChanged();
				eventArgs.Item.GetAspect<Wear>().RaiseUnequipped(_owner);
			};
			Jewelry = jewelryCollection;
		}

		private void ChangeEquipmentPosition(IItem newItem, WearSlot slot, ref IItem itemSlot)
		{
			if (newItem == null) throw new ArgumentNullException(nameof(newItem));

			if (itemSlot != null && !(itemSlot is Naked))
			{
				itemSlot.GetAspect<Wear>().RaiseUnequipped(_owner);
				_owner.Inventory.Add(itemSlot);
			}
			itemSlot = newItem;
			if (!(itemSlot is Naked))
			{
				if (newItem.GetAspect<Wear>().SuitableSlot == slot)
				{
					itemSlot.GetAspect<Wear>().RaiseEquipped(_owner);
					_owner.Inventory.Remove(itemSlot);
				}
				else
				{
					throw new InvalidOperationException("Item does not suit slot type.");
				}
			}

			RaiseEquipmentChanged();
		}
	}
}
