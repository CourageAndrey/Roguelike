using System.Drawing;
using System.Linq;

using Roguelike.Core.Configuration;
using Roguelike.Core.Interfaces;
using Roguelike.Core.Localization;
using Roguelike.Core.Objects;

namespace Roguelike.Core.ActiveObjects
{
	public class Horse : Alive
	{
		#region Properties

		public override Color SkinColor
		{ get; }

		#endregion

		public Horse(Balance balance, bool sexIsMale, Time birthDate, Color skinColor)
			: base(balance, sexIsMale, birthDate, new Properties(5, 5, 30, 5, 5, 5), Enumerable.Empty<Item>())
		{
			SkinColor = skinColor;
			AddAspects(new Transport(this));
		}

		protected override ActionResult DoImplementation()
		{
#warning Horse is too passive.
			var game = this.GetGame();
			var balance = game.Balance;
			return new ActionResult(Time.FromYears(balance.Time, 1), string.Empty);
		}

		public override Body CreateBody()
		{
			return ActiveObjects.Body.CreateAnimal();
		}

		public override string GetDescription(Language language, IAlive forWhom)
		{
			return language.Objects.Horse;
		}
	}
}
