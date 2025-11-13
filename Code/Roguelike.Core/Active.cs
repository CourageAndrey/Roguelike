using Roguelike.Core.Interfaces;

namespace Roguelike.Core
{
	public abstract class Active : Object, IActive
	{
		public Time? NextActionTime
		{ get; private set; }

		public abstract ActionResult Do();

		public Time SetNextActionTime(Time longevity)
		{
			NextActionTime += longevity;
			return NextActionTime!.Value;
		}

		public void Initialize(World world)
		{
			NextActionTime = world.Time;
		}
	}
}
