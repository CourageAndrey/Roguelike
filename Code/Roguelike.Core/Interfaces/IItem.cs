namespace Roguelike.Core.Interfaces
{
	public interface IItem : IRequireGravitation
	{
		ItemType Type
		{ get; }

		event EventHandler<IItem, IAlive> Picked;

		event EventHandler<IItem, IAlive> Dropped;
	}

	public interface IWear : IItem
	{
		event EventHandler<IWear, IAlive> Equipped;

		event EventHandler<IWear, IAlive> Unequipped;

		void RaiseEquipped(IAlive who);

		void RaiseUnequipped(IAlive who);
	}

	public interface IWeapon : IItem
	{
		event EventHandler<IWeapon, IAlive> PreparedForBattle;

		event EventHandler<IWeapon, IAlive> StoppedBattle;

		void RaisePreparedForBattle(IAlive who);

		void RaiseStoppedBattle(IAlive who);
	}

	#region Wear interfaces

	public interface IHeadWear : IWear
	{ }

	public interface IUpperBodyWear : IWear
	{ }

	public interface ILowerBodyWear : IWear
	{ }

	public interface ICoverWear : IWear
	{ }

	public interface IHandWear : IWear
	{ }

	public interface IFootWear : IWear
	{ }

	public interface IJewelry : IWear
	{ }

	#endregion
}
