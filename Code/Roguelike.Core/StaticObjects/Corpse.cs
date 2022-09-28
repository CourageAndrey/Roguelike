using Roguelike.Core.ActiveObjects;

namespace Roguelike.Core.StaticObjects
{
	public class Corpse : Object
	{
		#region Properties

		public override bool IsSolid
		{ get { return false; } }

		public Alive Alive
		{ get; }

		#endregion

		public Corpse(Alive alive)
		{
			Alive = alive;
		}
	}
}
