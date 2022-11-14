using System.Drawing;
using System.Threading;

using Roguelike.Core.Interfaces;
using Roguelike.Core.Localization;

namespace Roguelike.Core
{
	public abstract class Item : IItem
	{
		#region Properties

		public abstract decimal Weight
		{ get; }

		public abstract ItemType Type
		{ get; }

		public abstract Color Color
		{ get; }

		public abstract Material Material
		{ get; }

		public event ValueChangedEventHandler<IRequireGravitation, decimal> WeightChanged;

		public event EventHandler<IItem, IAlive> Picked;

		public event EventHandler<IItem, IAlive> Dropped;

		#endregion

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

		public abstract string GetDescription(Language language, IAlive forWhom);
	}
}
