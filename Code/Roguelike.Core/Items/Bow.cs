namespace Roguelike.Core.Items
{
	public class Bow : Weapon
	{
		#region Properties

		public override bool IsRange
		{ get { return true; } }

		public override double Weight
		{ get { return 1; } }

		#endregion

		public Bow()
			: base(Material.Wood)
		{ }
	}
}
