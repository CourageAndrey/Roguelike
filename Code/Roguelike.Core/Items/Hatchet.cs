namespace Roguelike.Core.Items
{
	public class Hatchet : Weapon
	{
		#region Properties

		public override double Weight
		{ get { return 2; } }

		public override ItemType Type
		{ get { return ItemType.Tool; } }

		#endregion
	}
}
