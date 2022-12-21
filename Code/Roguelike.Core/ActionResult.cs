using System.Collections.Generic;
using System.Globalization;

using Roguelike.Core.Configuration;
using Roguelike.Core.Interfaces;

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

		internal static ActionResult Wait(IObject forWhom)
		{
			var game = forWhom.GetGame();
			var balance = game.World.Balance;
			return new ActionResult(
				Time.FromTicks(balance.Time, balance.ActionLongevity.Wait),
				string.Format(
					CultureInfo.InvariantCulture,
					game.Language.LogActionFormats.Wait,
					forWhom.GetDescription(game.Language, game.Hero)));
		}
	}
}
