using System;
using System.Drawing;
using System.Globalization;
using System.Linq;

namespace Roguelike.Core.ActiveObjects
{
	public class Dog : Alive
	{
		#region Properties

		public override Color SkinColor
		{ get; }

		#endregion

		public Dog(bool sexIsMale, Time birthDate, Color skinColor)
			: base(sexIsMale, birthDate, new Properties(5, 5, 30, 5, 5, 5), Enumerable.Empty<Item>())
		{
			SkinColor = skinColor;
		}

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
