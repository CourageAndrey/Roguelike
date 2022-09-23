using Roguelike.Core.Interfaces;

using System.Threading;

namespace Roguelike.Core
{
	public abstract class Item : IItem
	{
		#region Properties

		public abstract double Weight
		{ get; }

		public abstract ItemType Type
		{ get; }

		public event ValueChangedEventHandler<IRequireGravitation, double> WeightChanged;

		public event EventHandler<IItem, IAlive> Picked;

		public event EventHandler<IItem, IAlive> Dropped;

		protected void RaiseWeightChanged(double oldWeight, double newWeight)
		{
			var handler = Volatile.Read(ref WeightChanged);
			if (handler != null)
			{
				handler(this, oldWeight, newWeight);
			}
		}

		protected internal void RaisePicked(IAlive who)
		{
			var handler = Volatile.Read(ref Picked);
			if (handler != null)
			{
				handler(this, who);
			}
		}

		protected internal void RaiseDropped(IAlive who)
		{
			var handler = Volatile.Read(ref Dropped);
			if (handler != null)
			{
				handler(this, who);
			}
		}

		#endregion
	}
}
