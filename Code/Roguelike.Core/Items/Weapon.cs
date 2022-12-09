using System.Threading;

using Roguelike.Core.Interfaces;

namespace Roguelike.Core.Items
{
	public class Weapon : IAspect
	{
		public bool IsRange
		{ get; }

		public event EventHandler<Weapon, IAlive> PreparedForBattle;

		public event EventHandler<Weapon, IAlive> StoppedBattle;

		public Weapon(bool isRange)
		{
			IsRange = isRange;
		}

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