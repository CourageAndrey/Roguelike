using System.Collections.Generic;

using Roguelike.Core.Configuration;

namespace Roguelike.Core
{
	public class ActionResult
	{
		#region Properties

		public Time Longevity
		{ get; }

		public IList<string> LogMessages
		{ get; }

		#endregion

		#region Конструкторы

		public ActionResult(Time longevity, string message)
			: this(longevity, new[] { message })
		{ }

		public ActionResult(Time longevity, IEnumerable<string> messages)
		{
			Longevity = longevity;
			LogMessages = new List<string>(messages);
		}

		#endregion

		internal static ActionResult GetEmpty(Balance balance)
		{
			return new ActionResult(Time.FromTicks(balance, balance.ActionLongevityNull), string.Empty);
		}
	}
}
