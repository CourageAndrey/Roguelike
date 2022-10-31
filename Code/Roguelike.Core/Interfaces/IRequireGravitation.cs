namespace Roguelike.Core.Interfaces
{
	public interface IRequireGravitation
	{
		decimal Weight
		{ get; }

		event ValueChangedEventHandler<IRequireGravitation, decimal> WeightChanged;
	}
}
