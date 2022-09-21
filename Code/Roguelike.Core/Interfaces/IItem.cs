namespace Roguelike.Core.Interfaces
{
	public interface IItem : IRequireGravitation
	{
		ItemType Type
		{ get; }
	}

	public interface IWear : IItem
	{ }

	public interface IWeapon : IItem
	{ }

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
