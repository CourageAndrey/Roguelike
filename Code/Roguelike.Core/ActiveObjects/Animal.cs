using System;
using System.Globalization;
using System.Linq;

using Roguelike.Core.Interfaces;

namespace Roguelike.Core.ActiveObjects
{
	public class Animal : AliveObject
	{
		#region Properties



		#endregion

		public Animal(bool sexIsMale, Time age, Properties properties, IInventory inventory)
			: base(sexIsMale, age, properties, inventory)
		{ }

		public override ActionResult Do()
		{
#warning Implement animal AI.
			if (Owner == null)
			{
				var random = new Random(DateTime.Now.Millisecond);
				return TryMove(DirectionHelper.AllDirections[random.Next(0, DirectionHelper.AllDirections.Count - 1)]);
			}
			else
			{
				var nextStep = Ai.CalculateRoute(CurrentCell.Region, CurrentCell.Position, Owner.CurrentCell.Position).Skip(1).FirstOrDefault();
				if (nextStep != null)
				{
					return TryMove(CurrentCell.Position.GetDirection(nextStep));
				}
				else
				{
					var game = CurrentCell.Region.World.Game;
					var balance = game.Balance;
					return new ActionResult(
						Time.FromTicks(balance, balance.ActionLongevityWait),
						string.Format(
							CultureInfo.InvariantCulture,
							game.Language.LogActionFormatWait,
							this));
				}
			}
		}

		public override Body CreateBody()
		{
			return ActiveObjects.Body.CreateAnimal(this);
		}
	}
}
