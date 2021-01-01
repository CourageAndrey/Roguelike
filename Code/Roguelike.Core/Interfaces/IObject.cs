namespace Roguelike.Core.Interfaces
{
	public interface IObject
	{
		Cell CurrentCell
		{ get; }

		bool IsSolid
		{ get; }

		bool TryMoveTo(Cell cell);
	}
}
