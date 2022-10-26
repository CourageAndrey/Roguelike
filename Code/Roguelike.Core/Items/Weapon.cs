using System.Threading;

using Roguelike.Core.Interfaces;

namespace Roguelike.Core.Items
{
	public abstract class Weapon : Item, IWeapon
	{
		public abstract bool IsRange
		{ get; }

		public event EventHandler<IWeapon, IAlive> PreparedForBattle;

		public event EventHandler<IWeapon, IAlive> StoppedBattle;

		public void RaisePreparedForBattle(IAlive who)
		{
			var handler = Volatile.Read(ref PreparedForBattle);
			if (handler != null)
			{
				handler(this, who);
			}
		}

		public void RaiseStoppedBattle(IAlive who)
		{
			var handler = Volatile.Read(ref StoppedBattle);
			if (handler != null)
			{
				handler(this, who);
			}
		}
	}
}
