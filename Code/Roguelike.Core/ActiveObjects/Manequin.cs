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
			private set
			{
				if (value == null) throw new ArgumentNullException(nameof(value));
				_headWear = value;
				RaiseEquipmentChanged();
			}
		}

		public IUpperBodyWear UpperBodyWear
		{
			get { return _upperBodyWear; }
			private set
			{
				if (value == null) throw new ArgumentNullException(nameof(value));
				_upperBodyWear = value;
				RaiseEquipmentChanged();
			}
		}

		public ILowerBodyWear LowerBodyWear
		{
			get { return _lowerBodyWear; }
			private set
			{
				if (value == null) throw new ArgumentNullException(nameof(value));
				_lowerBodyWear = value;
				RaiseEquipmentChanged();
			}
		}

		public ICoverWear CoverWear
		{
			get { return _coverWear; }
			private set
			{
				if (value == null) throw new ArgumentNullException(nameof(value));
				_coverWear = value;
				RaiseEquipmentChanged();
			}
		}

		public IHandWear HandsWear
		{
			get { return _handsWear; }
			private set
			{
				if (value == null) throw new ArgumentNullException(nameof(value));
				_handsWear = value;
				RaiseEquipmentChanged();
			}
		}

		public IFootWear FootsWear
		{
			get { return _footsWear; }
			private set
			{
				if (value == null) throw new ArgumentNullException(nameof(value));
				_footsWear = value;
				RaiseEquipmentChanged();
			}
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
	}
}
