using System;

using Roguelike.Core.Interfaces;

namespace Roguelike.Core.ActiveObjects
{
	public class Npc : Humanoid
	{
		#region Properties



		#endregion

		public Npc(bool sexIsMale, Time birthDate, Properties properties, IInventory inventory, string name)
			: base(sexIsMale, birthDate, properties, inventory, name)
		{ }

		public override ActionResult Do()
		{
#warning Implement NPC AI.
			var random = new Random(DateTime.Now.Millisecond);
			return TryMove(DirectionHelper.AllDirections[random.Next(0, DirectionHelper.AllDirections.Count - 1)]);
		}
	}
}
