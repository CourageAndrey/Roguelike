using Roguelike.Core.Interfaces;
using Roguelike.Core.Localization;

using System;
using System.Collections.Generic;
using System.Drawing;
using System.Threading;

namespace Roguelike.Core.Items
{
	internal class Pack : IItem
	{
		#region Properties
		
		public IItem Item
		{ get; }

		public uint Count
		{ get; private set; }

		public decimal Weight
		{ get { return Item.Weight * Count; } }

		public IReadOnlyCollection<IAspect> Aspects
		{ get { return Item.Aspects; } }

		public Color Color
		{ get { return Item.Color; } }

		public ItemType Type
		{ get { return Item.Type; } }

		public Material Material
		{ get { return Item.Material; } }

		public event ValueChangedEventHandler<IMassy, decimal>? WeightChanged;
		public event EventHandler<IItem, IAlive>? Picked;
		public event EventHandler<IItem, IAlive>? Dropped;

		#endregion

		internal Pack(IItem item, uint count)
		{
			Item = item ?? throw new ArgumentNullException(nameof(item));
			Count = count > 0 ? count : throw new ArgumentException("Count must be > 0.", nameof(count));
		}

		public string GetDescription(Language language, IAlive forWhom)
		{
			return $"{Count} x {Item.GetDescription(language, forWhom)}";
		}

		public void RaisePicked(IAlive who)
		{
			var handler = Volatile.Read(ref Picked);
			if (handler != null)
			{
				handler(this, who);
			}
		}

		public void RaiseDropped(IAlive who)
		{
			var handler = Volatile.Read(ref Dropped);
			if (handler != null)
			{
				handler(this, who);
			}
		}

		public uint ChangeCount(int delta)
		{
			var weight = Weight;

			if (delta > 0 || Count > -delta)
			{
				Count = (uint)(Count + delta);
			}
			else
			{
				throw new InvalidOperationException("Count of items oin pack cannot be less than 0.");
			}

			var handler = Volatile.Read(ref WeightChanged);
			if (handler != null)
			{
				handler(this, weight, Weight);
			}

			return Count;
		}
	}
}
