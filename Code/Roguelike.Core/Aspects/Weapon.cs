using System.Threading;

using Roguelike.Core.Interfaces;

namespace Roguelike.Core.Aspects
{
	public abstract class Weapon : IAspect
	{
		#region Properties

		public abstract bool IsRange
		{ get; }

		public event EventHandler<Weapon, IAlive> PreparedForBattle;

		public event EventHandler<Weapon, IAlive> StoppedBattle;

		#endregion

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

	public class MeleeWeapon : Weapon
	{
		#region Properties

		public override bool IsRange
		{ get { return false; } }

		#endregion
	}

	public class RangeWeapon : Weapon
	{
		#region Properties

		public override bool IsRange
		{ get { return true; } }

		public MissileType Type
		{ get; }

		#endregion

		public RangeWeapon(MissileType type)
		{
			Type = type;
		}
	}
}