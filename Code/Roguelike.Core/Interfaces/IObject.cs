using Roguelike.Core.ActiveObjects;

namespace Roguelike.Core.Interfaces
{
	public interface IObject : IDescriptive, IAspectHolder<IObjectAspect>
	{
		Cell CurrentCell
		{ get; }

		bool IsSolid
		{ get; }

		void MoveTo(Cell cell);

		event ValueChangedEventHandler<IObject, bool> IsSolidChanged;

		event ValueChangedEventHandler<IObject, Cell> CellChanged;
	}

	public interface IObjectAspect : IAspect
	{ }

	public static class ObjectExtensions
	{
		public static Vector GetPosition(this IObject obj)
		{
			return obj.CurrentCell?.Position;
		}

		public static Region GetRegion(this IObject obj)
		{
			return obj.CurrentCell?.Region;
		}

		public static World GetWorld(this IObject obj)
		{
			return obj.GetRegion()?.World;
		}

		public static Game GetGame(this IObject obj)
		{
			return obj.GetWorld()?.Game;
		}

		public static Hero GetHero(this IObject obj)
		{
			return obj.GetWorld()?.Hero;
		}
	}
}
