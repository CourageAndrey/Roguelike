﻿using System;
using System.Collections.Generic;

namespace Roguelike.Core.ActiveObjects
{
	public class Npc : Humanoid
	{
		#region Properties



		#endregion

		public Npc(Race race, bool sexIsMale, Time birthDate, Properties properties, IEnumerable<Item> inventory, string name)
			: base(race, sexIsMale, birthDate, properties, inventory, name)
		{ }

		protected override ActionResult DoImplementation()
		{
#warning Implement NPC AI.
			var random = new Random(DateTime.Now.Millisecond);
			return TryMove(DirectionHelper.AllDirections[random.Next(0, DirectionHelper.AllDirections.Count - 1)]);
		}
	}
}
