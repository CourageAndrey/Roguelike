using Roguelike.Core.Aspects;

namespace Roguelike.Core.Interfaces
{
	public interface IHero : IHumanoid
	{
		Camera Camera
		{ get; }
	}
}
