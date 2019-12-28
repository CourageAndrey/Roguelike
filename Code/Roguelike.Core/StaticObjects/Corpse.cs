using Roguelike.Core.ActiveObjects;

namespace Roguelike.Core.StaticObjects
{
	public class Corpse : Object
	{
		#region Properties

		public override bool IsSolid
		{ get { return false; } }

		public AliveObject Alive
		{ get; }

		#endregion

		public Corpse(AliveObject alive)
		{
			Alive = alive;
		}
	}
}
