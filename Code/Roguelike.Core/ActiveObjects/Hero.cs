using System;
using System.Collections.Generic;

using Roguelike.Core.Configuration;
using Roguelike.Core.Interfaces;
using Roguelike.Core.Objects;
using Roguelike.Core.StaticObjects;

namespace Roguelike.Core.ActiveObjects
{
	public class Hero : Humanoid
	{
		public Hero(Balance balance, Race race, bool sexIsMale, Time birthDate, IProperties properties, IEnumerable<Item> inventory, string name)
			: base(balance, race, sexIsMale, birthDate, properties, inventory, name, new IObjectAspect[1])
		{
#warning Errorneous typecast.
			((IObjectAspect[]) Aspects)[0] = new CreatureCamera(this);
		}

		protected override ActionResult DoImplementation()
		{
			throw new NotSupportedException("Hero works outside this logic.");
		}

		public override Corpse Die(string reason)
		{
			var game = CurrentCell.Region.World.Game;
			game.Defeat();
			return base.Die(reason);
		}
	}
}
