using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;

namespace Roguelike.Core.ActiveObjects
{
	public class Animal : Alive
	{
		#region Properties



		#endregion

		public Animal(bool sexIsMale, Time birthDate, Properties properties, IEnumerable<Item> inventory)
			: base(sexIsMale, birthDate, properties, inventory)
		{ }

		protected override ActionResult DoImplementation()
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
						Time.FromTicks(balance.Time, balance.ActionLongevity.Wait),
						string.Format(
							CultureInfo.InvariantCulture,
							game.Language.LogActionFormats.Wait,
							this));
				}
			}
		}

		public override Body CreateBody()
		{
			return ActiveObjects.Body.CreateAnimal();
		}
	}
}
