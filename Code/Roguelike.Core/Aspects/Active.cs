using System;

using Roguelike.Core.Interfaces;
using Roguelike.Core.Mechanics;

namespace Roguelike.Core.Aspects
{
	public class Active : AspectWithHolder<IObject>
	{
		public Time? NextActionTime
		{ get; private set; }

		private readonly Func<ActionResult> _do;

		public Active(IObject holder, Func<ActionResult> @do)
			: base(holder)
		{
			_do = @do ?? throw new ArgumentNullException(nameof(@do));
		}

		public ActionResult Do()
		{
			return _do();
		}

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
