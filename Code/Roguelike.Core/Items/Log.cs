namespace Roguelike.Core.Items
{
	public class Log : Item
	{
		#region Properties

		public override double Weight
		{ get { return 0.5; } }

		public override ItemType Type
		{ get { return ItemType.Tool; } }

		#endregion
	}
}
