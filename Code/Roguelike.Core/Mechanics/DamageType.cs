namespace Roguelike.Core.Mechanics
{
	public class DamageType
	{
		private DamageType()
		{ }

		#region List

		public static readonly DamageType Piercing = new DamageType();
		public static readonly DamageType Slashing = new DamageType();
		public static readonly DamageType Chopping = new DamageType();
		public static readonly DamageType Bludgeoning = new DamageType();
		public static readonly DamageType Elemental = new DamageType();

		#endregion
	}
}
