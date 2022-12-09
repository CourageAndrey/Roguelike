namespace Roguelike.Core.Interfaces
{
#warning Make active an aspect.
	public interface IActive : IObject
	{
		Time? NextActionTime
		{ get; }

		ActionResult Do();
	}
}
