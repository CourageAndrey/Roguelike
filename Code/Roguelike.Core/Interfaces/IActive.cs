using System.Collections.Generic;

namespace Roguelike.Core.Interfaces
{
	public interface IActive : IObject
	{
		Time? NextActionTime
		{ get; }

		ActionResult Do();

		event EventHandler<IActive, ICollection<string>> OnLogMessage;
	}
}
