using System;
using System.Collections.Generic;
using System.Drawing;
using System.Threading;

using Roguelike.Core.Interfaces;
using Roguelike.Core.Localization;

namespace Roguelike.Core
{
	public class Item : IItem
	{
		#region Properties

		public decimal Weight
		{ get { return _getWeight(); } }

		public ItemType Type
		{ get { return _type; } }

		public Color Color
		{ get { return _color; } }

		public Material Material
		{ get { return _material; } }

		public event ValueChangedEventHandler<IRequireGravitation, decimal> WeightChanged;

		public event EventHandler<IItem, IAlive> Picked;

		public event EventHandler<IItem, IAlive> Dropped;

		public IReadOnlyCollection<IItemAspect> Aspects
		{ get; }

		private readonly Func<Language, IAlive, string> _getDescription;
		private readonly Func<decimal> _getWeight;
		private readonly ItemType _type;
		private readonly Color _color;
		private readonly Material _material;

		#endregion

		internal Item(
			Func<Language, IAlive, string> getDescription,
			Func<decimal> getWeight,
			ItemType type,
			Color color,
			Material material,
			params IItemAspect[] aspects)
		{
			_getDescription = getDescription;
			_getWeight = getWeight;
			_type = type;
			_color = color;
			_material = material;
			Aspects = aspects;
		}

		protected void RaiseWeightChanged(decimal oldWeight, decimal newWeight)
		{
			var handler = Volatile.Read(ref WeightChanged);
			if (handler != null)
			{
				handler(this, oldWeight, newWeight);
			}
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

		public string GetDescription(Language language, IAlive forWhom)
		{
			return _getDescription(language, forWhom);
		}
	}
}
