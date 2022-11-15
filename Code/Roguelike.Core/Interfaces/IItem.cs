using System.Collections.Generic;
using System.Drawing;
using System.Linq;

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

	public interface IItemAspect
	{ }

	public static class ItemExtensions
	{
		public static bool Is<AspectT>(this IItem item)
			where AspectT : IItemAspect
		{
			return item.Aspects.OfType<AspectT>().Any();
		}

		public static IEnumerable<IItem> Select<AspectT>(this IEnumerable<IItem> items)
			where AspectT : IItemAspect
		{
			return items.Where(item => item.Is<AspectT>());
		}

		public static AspectT GetAspect<AspectT>(this IItem item)
			where AspectT : IItemAspect
		{
			return item.Aspects.OfType<AspectT>().Single();
		}
	}
}
