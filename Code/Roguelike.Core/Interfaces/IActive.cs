namespace Roguelike.Core.Interfaces
{
	public interface IActive : IObject
	{
		Time? NextActionTime
		{ get; }

		ActionResult Do();
	}
}
