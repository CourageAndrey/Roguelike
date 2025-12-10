using System.Drawing;

using Roguelike.Core.Aspects;
using Roguelike.Core.Configuration;
using Roguelike.Core.Interfaces;
using Roguelike.Core.Localization;
using Roguelike.Core.Mechanics;

namespace Roguelike.Core.Objects
{
	public class Dog : Alive
	{
		#region Properties

		public override Color SkinColor
		{ get; }

		#endregion

		public Dog(Balance balance, bool sexIsMale, Time birthDate, Color skinColor)
			: base(balance, sexIsMale, birthDate, new Properties(5, 5, 30, 5, 5, 5, 1), Enumerable.Empty<Item>())
		{
			SkinColor = skinColor;
		}

		protected override ActionResult DoImplementation()
		{
			var owner = this.GetAspect<Ownership>().Owner;
			if (owner == null)
			{
				return this.Wander();
			}
			else
			{
				return this.Follow(owner);
			}
		}

		public override Body CreateBody()
		{
			return Body.CreateAnimal(this);
		}

		public override string GetDescription(Language language, IAlive forWhom)
		{
			return language.Objects.Dog;
		}
	}
}
