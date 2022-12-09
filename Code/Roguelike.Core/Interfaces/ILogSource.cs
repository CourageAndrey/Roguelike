using System.Collections.Generic;

namespace Roguelike.Core.Interfaces
{
	public interface ILogSource
	{
		void WriteToLog(ICollection<string> messages);

		event EventHandler<IObject, ICollection<string>> OnLogMessage;
	}

	public static class LogSourceExtensions
	{
		public static void WriteToLog(this ILogSource obj, string message)
		{
			obj.WriteToLog(new[] { message });
		}
	}
}
