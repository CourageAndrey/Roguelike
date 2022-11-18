namespace Roguelike.Core.Interfaces
{
	public interface IHero : IHumanoid
	{
		ICamera Camera
		{ get; }
	}
}
