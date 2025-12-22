namespace Roguelike.Core.Mechanics
{
	public class WeaponMastery
	{
		public bool IsRange
		{ get; }

		private WeaponMastery(bool isRange)
		{
			IsRange = isRange;
		}

		#region List

		public static readonly WeaponMastery HandToHand = new WeaponMastery(false);
		public static readonly WeaponMastery Blades = new WeaponMastery(false);
		public static readonly WeaponMastery Axes = new WeaponMastery(false);
		public static readonly WeaponMastery Maces = new WeaponMastery(false);
		public static readonly WeaponMastery Polearms = new WeaponMastery(false);
		public static readonly WeaponMastery Bows = new WeaponMastery(true);
		public static readonly WeaponMastery Throwing = new WeaponMastery(true);

		#endregion
	}
}
