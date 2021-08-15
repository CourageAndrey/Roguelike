namespace Roguelike.Core.Interfaces
{
	public interface IItem : IRequireGravitation
	{
		ItemType Type
		{ get; }
	}
}
