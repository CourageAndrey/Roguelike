namespace Roguelike.Core.Interfaces
{
	public interface IWeapon : IItem
	{
		bool IsRange
		{ get; }

		event EventHandler<IWeapon, IAlive> PreparedForBattle;

		event EventHandler<IWeapon, IAlive> StoppedBattle;

		void RaisePreparedForBattle(IAlive who);

		void RaiseStoppedBattle(IAlive who);
	}
}