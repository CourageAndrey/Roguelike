namespace Roguelike.Core.Interfaces
{
	public interface IHero : IActive
	{
		ICamera Camera
		{ get; }
	}
}
