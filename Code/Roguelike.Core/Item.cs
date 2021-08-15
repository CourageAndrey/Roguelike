using Roguelike.Core.Interfaces;

namespace Roguelike.Core
{
	public abstract class Item : IItem
	{
		#region Properties

		public abstract double Weight
		{ get; }

		public abstract ItemType Type
		{ get; }

		#endregion
	}
}
