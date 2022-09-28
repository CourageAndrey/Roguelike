using System.Threading;

using Roguelike.Core.Interfaces;

namespace Roguelike.Core.Items
{
	public abstract class Wear : Item, IWear
	{
		public event EventHandler<IWear, IAlive> Equipped;

		public event EventHandler<IWear, IAlive> Unequipped;

		public void RaiseEquipped(IAlive who)
		{
			var handler = Volatile.Read(ref Equipped);
			if (handler != null)
			{
				handler(this, who);
			}
		}

		public void RaiseUnequipped(IAlive who)
		{
			var handler = Volatile.Read(ref Unequipped);
			if (handler != null)
			{
				handler(this, who);
			}
		}
	}
}
