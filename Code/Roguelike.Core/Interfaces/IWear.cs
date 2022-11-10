namespace Roguelike.Core.Interfaces
{
	public interface IWear : IItem
	{
		event EventHandler<IWear, IAlive> Equipped;

		event EventHandler<IWear, IAlive> Unequipped;

		void RaiseEquipped(IAlive who);

		void RaiseUnequipped(IAlive who);
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