using System.Globalization;
using System.Threading;

using Roguelike.Core.Interfaces;

namespace Roguelike.Core.Aspects
{
	public class Thief : IAspect
	{
		#region Properties

		private readonly IAlive _holder;

		public bool IsSneaking
		{ get; private set; }

		public event ValueChangedEventHandler<IAlive, bool>? SneakChanged;

		#endregion

		public Thief(IAlive holder)
		{
			_holder = holder;
		}

		private void RaiseSneakingChanged(bool oldSneaking, bool newSneaking)
		{
			var handler = Volatile.Read(ref SneakChanged);
			if (handler != null)
			{
				handler(_holder, oldSneaking, newSneaking);
			}
		}

		public ActionResult ChangeSneak(bool sneaking)
		{
			int time;
			string logMessage;
			var game = _holder.GetGame();
			var language = game.Language.LogActionFormats;
			var balance = game.World.Balance;
			Activity? newActivity = null;

			if (IsSneaking != sneaking)
			{
				IsSneaking = sneaking;

				RaiseSneakingChanged(!sneaking, sneaking);

				newActivity = sneaking ? Activity.Sneaks : Activity.Stands;

				time = balance.ActionLongevity.ChangeSneaking;
				logMessage = string.Format(
					CultureInfo.InvariantCulture,
					IsSneaking ? language.StartSneaking : language.StopSneaking,
					_holder.GetDescription(game.Language, game.Hero));
			}
			else
			{
				time = balance.ActionLongevity.Disabled;
				logMessage = string.Format(CultureInfo.InvariantCulture, language.ChangeSneakingDisabled, _holder.GetDescription(game.Language, game.Hero));
			}

			return new ActionResult(Time.FromTicks(balance.Time, time), logMessage, newActivity);
		}

		/*public ActionResult Pickpocket(IHumanoid target)
		{
			var game = _holder.GetGame();
			var balance = game.World.Balance;
			var language = game.Language;
			var random = new Random(DateTime.Now.Millisecond);

			// Check if target has any items
			if (target.Inventory.Items.Count == 0)
			{
				return new ActionResult(
					Time.FromTicks(balance.Time, balance.ActionLongevity.Disabled),
					string.Format(CultureInfo.InvariantCulture, language.LogActionFormats.PickpocketFailed, _holder.GetDescription(language, game.Hero), target.GetDescription(language, game.Hero)));
			}

			// Calculate success chance based on Reaction difference and sneaking state
			int successChance = 50; // Base chance
			successChance += ((int)_holder.Properties.Reaction - (int)target.Properties.Reaction) * 10;
			if (IsSneaking)
			{
				successChance += 20; // Bonus for sneaking
			}

			if (random.Next(0, 100) < successChance)
			{
				// Success - steal a random item
				var itemToSteal = target.Inventory.Items.ElementAt(random.Next(target.Inventory.Items.Count));
				target.Inventory.Items.Remove(itemToSteal);
				_holder.Inventory.Items.Add(itemToSteal);

				return new ActionResult(
					Time.FromTicks(balance.Time, balance.ActionLongevity.Pickpocket),
					string.Format(CultureInfo.InvariantCulture, language.LogActionFormats.Pickpocket, _holder.GetDescription(language, game.Hero), target.GetDescription(language, game.Hero), itemToSteal.GetDescription(language, _holder)));
			}
			else
			{
				// Failed - target becomes hostile
				if (target.Is<Fighter>())
				{
					target.Fighter.ChangeAggressive(true);
				}

				return new ActionResult(
					Time.FromTicks(balance.Time, balance.ActionLongevity.Pickpocket),
					string.Format(CultureInfo.InvariantCulture, language.LogActionFormats.PickpocketFailed, _holder.GetDescription(language, game.Hero), target.GetDescription(language, game.Hero)));
			}
		}*/
	}
}
