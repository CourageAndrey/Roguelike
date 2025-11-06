namespace Roguelike.Core.Interfaces
{
	public interface IMassy
	{
		decimal Weight
		{ get; }
	}

	public interface IVariableMassy : IMassy
	{
		event ValueChangedEventHandler<IMassy, decimal>? WeightChanged;
	}
}
