using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;

using Roguelike.Core.Interfaces;

namespace Roguelike.Core.Aspects
{
	public class Inventory : IAspect, IVariableMassy
	{
		#region Properties

		private readonly IAlive _holder;

		public ICollection<IItem> Items
		{ get; }

		public decimal Weight
		{ get { return Items.Sum(item => item.Weight); } }

		public event ValueChangedEventHandler<IMassy, decimal> WeightChanged;

		#endregion

		public Inventory(IAlive holder, IEnumerable<IItem> items = null)
		{
			_holder = holder;

			void updateWeight(decimal delta)
			{
				var handler = Volatile.Read(ref WeightChanged);
				if (handler != null)
				{
					handler(this, Weight - delta, Weight);
				}
			}

			void updateOnItemChange(IMassy item, decimal oldWeight, decimal newWeight)
			{
				updateWeight(newWeight - oldWeight);
			}

			var _items = new EventCollection<IItem>(items ?? Array.Empty<IItem>());
			_items.ItemAdded += (sender, args) =>
			{
				updateWeight(args.Item.Weight);

				args.Item.WeightChanged += updateOnItemChange;

				args.Item.RaisePicked(_holder);
			};
			_items.ItemRemoved += (sender, args) =>
			{
				args.Item.RaiseDropped(_holder);

				args.Item.WeightChanged -= updateOnItemChange;

				updateWeight(-args.Item.Weight);
			};

			Items = _items;
		}
	}
}
