namespace Roguelike.Core.Interfaces
{
	public interface IWaterSource
	{
		ActionResult Drink(IAlive who);
	}
}
