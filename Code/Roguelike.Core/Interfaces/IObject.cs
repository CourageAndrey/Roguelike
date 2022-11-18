namespace Roguelike.Core.Interfaces
{
	public interface IObject : IDescriptive, IAspectHolder<IObjectAspect>
	{
		Cell CurrentCell
		{ get; }

		bool IsSolid
		{ get; }

		bool TryMoveTo(Cell cell);

		event ValueChangedEventHandler<IObject, bool> IsSolidChanged;

		event ValueChangedEventHandler<IObject, Cell> CellChanged;
	}

	public interface IObjectAspect : IAspect
	{ }
}
