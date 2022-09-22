using System.Collections.Generic;

namespace Roguelike.Core.Interfaces
{
	public delegate void LogMessageRaisedDelegate(IActive sender, ICollection<string> messages);

	public interface IActive
	{
		Time? NextActionTime
		{ get; }

		ActionResult Do();

		event LogMessageRaisedDelegate OnLogMessage;
	}
}
