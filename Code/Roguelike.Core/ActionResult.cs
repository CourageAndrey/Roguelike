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

		public Activity NewActivity
		{ get; }

		#endregion

		#region Constructors

		public ActionResult(Time longevity, string message, Activity newActivity = null)
			: this(longevity, new[] { message }, newActivity)
		{ }

		public ActionResult(Time longevity, IEnumerable<string> messages, Activity newActivity = null)
		{
			Longevity = longevity;
			LogMessages = new List<string>(messages);
			NewActivity = newActivity;
		}

		#endregion

		internal static ActionResult GetEmpty(Balance balance)
		{
			return new ActionResult(Time.FromTicks(balance.Time, balance.ActionLongevity.Null), string.Empty);
		}
	}
}
