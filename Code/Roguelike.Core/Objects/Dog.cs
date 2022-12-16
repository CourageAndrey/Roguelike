using System;
using System.Drawing;
using System.Linq;

using Roguelike.Core.ActiveObjects;
using Roguelike.Core.Aspects;
using Roguelike.Core.Configuration;
using Roguelike.Core.Interfaces;
using Roguelike.Core.Localization;

namespace Roguelike.Core.Objects
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
			var owner = this.GetAspect<Ownership>().Owner;
			if (owner == null)
			{
				var random = new Random(DateTime.Now.Millisecond);
				return this.TryMove(DirectionHelper.AllDirections[random.Next(0, DirectionHelper.AllDirections.Count - 1)]);
			}
			else
			{
				var nextStep = Ai.CalculateRoute(this.GetRegion(), this.GetPosition(), owner.GetPosition()).Skip(1).FirstOrDefault();
				if (nextStep != null)
				{
					return this.TryMove(this.GetPosition().GetDirection(nextStep));
				}
				else
				{
					return ActionResult.Wait(this);
				}
			}
		}

		public override Body CreateBody()
		{
			return Body.CreateAnimal();
		}

		public override string GetDescription(Language language, IAlive forWhom)
		{
			return language.Objects.Dog;
		}
	}
}
