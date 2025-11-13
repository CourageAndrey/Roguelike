using System;

using Roguelike.Core.Interfaces;

namespace Roguelike.Core.Aspects
{
	public abstract class AspectWithHolder<HolderT> : IAspectWithHolder<HolderT>
		where HolderT : IAspectHolder
	{
		public HolderT Holder
		{ get; }

		protected AspectWithHolder(HolderT holder)
		{
			Holder = holder ?? throw new ArgumentNullException(nameof(holder));
		}
	}
}
