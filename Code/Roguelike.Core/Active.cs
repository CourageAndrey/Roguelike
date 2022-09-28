﻿using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading;

using Roguelike.Core.Interfaces;

namespace Roguelike.Core
{
	public abstract class Active : Object, IActive
	{
		#region Properties

		public Time? NextActionTime
		{ get; internal set; }

		#endregion

		public abstract ActionResult Do();

		public ActionResult TryMove(Direction direction)
		{
			var world = CurrentCell.Region.World;
			var game = world.Game;
			var balance = game.Balance;

			var oldPosition = CurrentCell.Position;
			var newCell = CurrentCell.Region.GetCell(CurrentCell.Position.GetNeighboor(direction));
			if (newCell != null)
			{
				double distance = CurrentCell.Position != null
					? newCell.Position.GetDistance(CurrentCell.Position)
					: 0;
				var language = game.Language;

				var alive = this as IAlive;
				IAlive target = null;
				if (alive?.IsAgressive == true && (target = newCell.Objects.OfType<IAlive>().FirstOrDefault()) != null)
				{
					return alive.Attack(target);
				}
				else
				{
					return TryMoveTo(newCell)
						? new ActionResult(
							Time.FromTicks(balance.Time, (int)(balance.ActionLongevity.Step * distance)),
							string.Format(CultureInfo.InvariantCulture, language.LogActionFormatMove, this, oldPosition, newCell.Position))
						: new ActionResult(
							Time.FromTicks(balance.Time, balance.ActionLongevity.Disabled),
							string.Format(CultureInfo.InvariantCulture, language.LogActionFormatMoveDisabled, this, oldPosition, newCell.Position));
				}
			}
			else
			{
				return ActionResult.GetEmpty(balance);
			}
#warning Take reflexes speed into account.
		}

		#region Log messages

		public event EventHandler<IActive, ICollection<string>> OnLogMessage;

		protected void WriteToLog(ICollection<string> messages)
		{
			var handler = Volatile.Read(ref OnLogMessage);
			if (handler != null)
			{
				handler(this, messages);
			}
		}

		protected void WriteToLog(string message)
		{
			WriteToLog(new[] { message });
		}

		#endregion
	}
}