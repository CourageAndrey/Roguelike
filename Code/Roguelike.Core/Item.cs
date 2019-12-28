using Roguelike.Core.Interfaces;

namespace Roguelike.Core
{
	public abstract class Item : IRequireGravitation
	{
		#region Properties

		public abstract double Weight
		{ get; }

		public abstract ItemType Type
		{ get; }

		#endregion
	}
}
