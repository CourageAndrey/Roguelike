namespace Roguelike.Core.Interfaces
{
	public interface IMassy
	{
		decimal Weight
		{ get; }

		event ValueChangedEventHandler<IMassy, decimal> WeightChanged;
	}
}
