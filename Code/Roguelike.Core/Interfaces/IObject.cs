namespace Roguelike.Core.Interfaces
{
	public interface IObject
	{
		Cell CurrentCell
		{ get; }

		bool IsSolid
		{ get; }

		bool TryMoveTo(Cell cell);

		event ValueChangedEventHandler<IObject, bool> IsSolidChanged;

		event ValueChangedEventHandler<IObject, Cell> CellChanged;
	}
}
