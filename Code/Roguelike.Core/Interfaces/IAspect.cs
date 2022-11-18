using System.Collections.Generic;
using System.Linq;

namespace Roguelike.Core.Interfaces
{
	public interface IAspect
	{ }

	public static class AspectExtensions
	{
		public static bool Is<AspectT>(this IItem item)
			where AspectT : IAspect
		{
			return item.Aspects.OfType<AspectT>().Any();
		}

		public static IEnumerable<IItem> Select<AspectT>(this IEnumerable<IItem> items)
			where AspectT : IAspect
		{
			return items.Where(item => item.Is<AspectT>());
		}

		public static AspectT GetAspect<AspectT>(this IItem item)
			where AspectT : IAspect
		{
			return item.Aspects.OfType<AspectT>().Single();
		}
	}
}
