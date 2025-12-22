using Roguelike.Core.Interfaces;
using Roguelike.Core.Mechanics;

namespace Roguelike.Core.Aspects
{
	public abstract class Weapon : IAspect
	{
		#region Properties

		public abstract bool IsRange
		{ get; }

		public event EventHandler<Weapon, IAlive>? PreparedForBattle;

		public event EventHandler<Weapon, IAlive>? StoppedBattle;

		public DamageType DamageType
		{ get; }

		public WeaponMastery WeaponMastery
		{ get; }

		#endregion

		protected Weapon(DamageType damageType, WeaponMastery weaponMastery)
		{
			DamageType = damageType;
			WeaponMastery = weaponMastery;
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

	public class MeleeWeapon : Weapon
	{
		#region Properties

		public override bool IsRange
		{ get { return false; } }

		#endregion

		public MeleeWeapon(DamageType damageType, WeaponMastery weaponMastery)
			: base(damageType, weaponMastery)
		{
			if (weaponMastery.IsRange) throw new ArgumentException(nameof(weaponMastery));
		}
	}

	public class RangeWeapon : Weapon
	{
		#region Properties

		public override bool IsRange
		{ get { return true; } }

		public MissileType Type
		{ get; }

		#endregion

		public RangeWeapon(DamageType damageType, WeaponMastery weaponMastery, MissileType type)
			: base(damageType, weaponMastery)
		{
			if (!weaponMastery.IsRange) throw new ArgumentException(nameof(weaponMastery));

			Type = type;
		}
	}
}