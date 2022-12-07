using System;
using System.Drawing;
using System.Globalization;
using System.Linq;

using Roguelike.Core.Configuration;
using Roguelike.Core.Interfaces;
using Roguelike.Core.Localization;

namespace Roguelike.Core.ActiveObjects
{
	public class Dog : Alive
	{
		#region Properties

		public override Color SkinColor
		{ get; }

		#endregion

		public Dog(Balance balance, bool sexIsMale, Time birthDate, Color skinColor)
			: base(balance, sexIsMale, birthDate, new Properties(5, 5, 30, 5, 5, 5), Enumerable.Empty<Item>())
		{
			SkinColor = skinColor;
		}

		protected override ActionResult DoImplementation()
		{
#warning Implement animal AI.
			if (Owner == null)
			{
				var random = new Random(DateTime.Now.Millisecond);
				return this.TryMove(DirectionHelper.AllDirections[random.Next(0, DirectionHelper.AllDirections.Count - 1)]);
			}
			else
			{
				var nextStep = Ai.CalculateRoute(this.GetRegion(), this.GetPosition(), Owner.GetPosition()).Skip(1).FirstOrDefault();
				if (nextStep != null)
				{
					return this.TryMove(this.GetPosition().GetDirection(nextStep));
				}
				else
				{
					var game = this.GetGame();
					var balance = game.Balance;
					return new ActionResult(
						Time.FromTicks(balance.Time, balance.ActionLongevity.Wait),
						string.Format(
							CultureInfo.InvariantCulture,
							game.Language.LogActionFormats.Wait,
							GetDescription(game.Language, game.Hero)));
				}
			}
		}

		public override Body CreateBody()
		{
			return ActiveObjects.Body.CreateAnimal();
		}

		public override string GetDescription(Language language, IAlive forWhom)
		{
			return language.Objects.Dog;
		}
	}
}
