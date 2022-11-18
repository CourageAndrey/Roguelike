using System.Drawing;

namespace Roguelike.Core.Interfaces
{
	public interface IItem : IRequireGravitation, IDescriptive, IAspectHolder<IItemAspect>
	{
		ItemType Type
		{ get; }

		Color Color
		{ get; }

		Material Material
		{ get; }

		event EventHandler<IItem, IAlive> Picked;

		event EventHandler<IItem, IAlive> Dropped;

		void RaisePicked(IAlive who);

		void RaiseDropped(IAlive who);
	}

	public interface IItemAspect : IAspect
	{ }
}
