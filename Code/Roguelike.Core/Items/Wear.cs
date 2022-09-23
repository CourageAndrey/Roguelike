using System.Threading;

using Roguelike.Core.Interfaces;

namespace Roguelike.Core.Items
{
	public abstract class Wear : Item, IWear
	{
		public event EventHandler<IWear, IAlive> Equipped;

		public event EventHandler<IWear, IAlive> Unequipped;

		protected void RaiseEquipped(IAlive who)
		{
			var handler = Volatile.Read(ref Equipped);
			if (handler != null)
			{
				handler(this, who);
			}
		}

		protected void RaiseUnequipped(IAlive who)
		{
			var handler = Volatile.Read(ref Unequipped);
			if (handler != null)
			{
				handler(this, who);
			}
		}
	}
}
