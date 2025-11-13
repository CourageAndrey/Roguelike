using System.Collections.Generic;
using System.Linq;

namespace Roguelike.Core.Interfaces
{
	public interface IAspect
	{ }

	public interface IAspectWithHolder<HolderT> : IAspect
		where HolderT : IAspectHolder
	{
		HolderT Holder
		{ get; }
	}

	public interface IAspectHolder
	{
		IReadOnlyCollection<IAspect> Aspects
		{ get; }
	}

	public static class AspectExtensions
	{
		public static bool Is<AspectT>(this IAspectHolder holder)
			where AspectT : IAspect
		{
			return holder.Aspects.OfType<AspectT>().Any();
		}

		public static IEnumerable<HolderT> Select<HolderT, AspectT>(this IEnumerable<HolderT> holders)
			where HolderT : IAspectHolder
			where AspectT : IAspect
		{
			return holders.Where(holder => holder.Is<AspectT>());
		}

		public static AspectT GetAspect<AspectT>(this IAspectHolder holder)
			where AspectT : IAspect
		{
			return holder.Aspects.OfType<AspectT>().Single();
		}

		public static AspectT? TryGetAspect<AspectT>(this IAspectHolder holder)
			where AspectT : IAspect
		{
			return holder.Aspects.OfType<AspectT>().FirstOrDefault();
		}
	}
}
