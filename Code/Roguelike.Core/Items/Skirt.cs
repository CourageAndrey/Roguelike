using Roguelike.Core.Interfaces;

namespace Roguelike.Core.Items
{
	public class Skirt : Wear, ILowerBodyWear
	{
		public override double Weight
		{ get { return 1; } }

		public override ItemType Type
		{ get { return ItemType.Wear; } }
	}
}
