using System.Collections.Generic;
using System.Drawing;

namespace Roguelike.Core.Interfaces
{
	public interface IItem : IRequireGravitation, IDescriptive
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

		IReadOnlyCollection<IItemAspect> Aspects
		{ get; }
	}

	public interface IItemAspect : IAspect
	{ }
}
