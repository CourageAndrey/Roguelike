using System;
using System.Collections.Generic;

using Roguelike.Core.Configuration;
using Roguelike.Core.Interfaces;

namespace Roguelike.Core.ActiveObjects
{
	public class Npc : Humanoid
	{
		#region Properties



		#endregion

		public Npc(Balance balance, Race race, bool sexIsMale, Time birthDate, Properties properties, IEnumerable<Item> inventory, string name, IObjectAspect[] aspects = null)
			: base(balance, race, sexIsMale, birthDate, properties, inventory, name, aspects)
		{ }

		protected override ActionResult DoImplementation()
		{
#warning Implement NPC AI.
			var random = new Random(DateTime.Now.Millisecond);
			return TryMove(DirectionHelper.AllDirections[random.Next(0, DirectionHelper.AllDirections.Count - 1)]);
		}
	}
}
