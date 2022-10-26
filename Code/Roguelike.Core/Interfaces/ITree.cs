using System.Collections.Generic;

namespace Roguelike.Core.Interfaces
{
	public interface ITree : IObject, IInteractive
	{
		ICollection<IItem> Chop();
	}
}
