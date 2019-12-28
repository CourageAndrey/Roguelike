using Roguelike.Core.Interfaces;

namespace Roguelike.Core.StaticObjects
{
	public class Bed : Object, ISleepingArea
	{
		#region Properties

		public override bool IsSolid
		{ get { return false; } }

		#endregion
	}
}
