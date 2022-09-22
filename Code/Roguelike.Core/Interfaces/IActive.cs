namespace Roguelike.Core.Interfaces
{
	public interface IActive
	{
		Time? NextActionTime
		{ get; }

		ActionResult Do();
	}
}
