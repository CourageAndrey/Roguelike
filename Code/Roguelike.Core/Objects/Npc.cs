﻿using System.Collections.Generic;

using Roguelike.Core.Configuration;

namespace Roguelike.Core.Objects
{
	public class Npc : Humanoid
	{
		#region Properties



		#endregion

		public Npc(Balance balance, Race race, bool sexIsMale, Time birthDate, IEnumerable<Item> inventory, string surmame, Profession profession)
			: base(balance, race, sexIsMale, birthDate, inventory, race.GenerateName(sexIsMale, surmame), profession)
		{ }

		protected override ActionResult DoImplementation()
		{
			return this.Wander();
		}
	}
}
