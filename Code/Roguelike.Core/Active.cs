using Roguelike.Core.Interfaces;

namespace Roguelike.Core
{
	public abstract class Active : Object, IActive
	{
		public Time? NextActionTime
		{ get; internal set; }

		public abstract ActionResult Do();
	}
}
