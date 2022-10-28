using System.Drawing;
using System.Threading;

using Roguelike.Core.Interfaces;

namespace Roguelike.Core
{
	public abstract class Item : IItem
	{
		#region Properties

		public abstract double Weight
		{ get; }

		public abstract ItemType Type
		{ get; }

		public abstract Color Color
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

		#endregion
	}
}
