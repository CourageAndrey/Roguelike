namespace Roguelike.Core.Interfaces
{
	public interface IItem : IRequireGravitation
	{
		ItemType Type
		{ get; }
	}

	#region Item interfaces

	public interface IHeadWear : IItem
	{ }

	public interface IUpperBodyWear : IItem
	{ }

	public interface ILowerBodyWear : IItem
	{ }

	public interface ICoverWear : IItem
	{ }

	public interface IHandWear : IItem
	{ }

	public interface IFootWear : IItem
	{ }

	public interface INecklace : IItem
	{ }

	public interface IRing : IItem
	{ }

	#endregion
}
