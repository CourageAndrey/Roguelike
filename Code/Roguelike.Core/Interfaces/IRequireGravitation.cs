namespace Roguelike.Core.Interfaces
{
	public interface IRequireGravitation
	{
		double Weight
		{ get; }

		event ValueChangedEventHandler<IRequireGravitation, double> WeightChanged;
	}
}
