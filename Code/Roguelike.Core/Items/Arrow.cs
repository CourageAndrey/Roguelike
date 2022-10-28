using System.Drawing;

using Roguelike.Core.Interfaces;

namespace Roguelike.Core.Items
{
	public class Arrow : Item, IMissile
	{
		#region Properties

		public override Color Color
		{ get { return Color.White; } }

		public override ItemType Type
		{ get { return ItemType.Weapon; } }

		public override double Weight
		{ get { return 0.1; } }

		#endregion
	}
}
