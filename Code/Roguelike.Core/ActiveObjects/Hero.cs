using System;
using System.Collections.Generic;

using Roguelike.Core.Configuration;
using Roguelike.Core.Interfaces;
using Roguelike.Core.Objects;
using Roguelike.Core.Objects.Aspects;

namespace Roguelike.Core.ActiveObjects
{
	public class Hero : Humanoid
	{
		public Hero(Balance balance, Race race, bool sexIsMale, Time birthDate, IProperties properties, IEnumerable<Item> inventory, string name)
			: base(balance, race, sexIsMale, birthDate, properties, inventory, name)
		{
			AddAspects(new CreatureCamera(this));
		}

		protected override ActionResult DoImplementation()
		{
			throw new NotSupportedException("Hero works outside this logic.");
		}

		public override Corpse Die(string reason)
		{
			var game = this.GetGame();
			game.Defeat();
			return base.Die(reason);
		}
	}
}
