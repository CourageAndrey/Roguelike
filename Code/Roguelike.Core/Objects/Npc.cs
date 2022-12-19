using System.Collections.Generic;

using Roguelike.Core.Aspects;
using Roguelike.Core.Configuration;

namespace Roguelike.Core.Objects
{
	public class Npc : Humanoid
	{
		#region Properties



		#endregion

		public Npc(Balance balance, Race race, bool sexIsMale, Time birthDate, Properties properties, IEnumerable<Item> inventory, string name)
			: base(balance, race, sexIsMale, birthDate, properties, inventory, name)
		{ }

		protected override ActionResult DoImplementation()
		{
			return this.Wander();
		}
	}
}
