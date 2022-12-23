using System.Drawing;

using Roguelike.Core.Configuration;

namespace Roguelike.Core.Objects
{
	public class Npc : Humanoid
	{
		#region Properties



		#endregion

		public Npc(Balance balance, Race race, bool sexIsMale, Time birthDate, string surmame, Profession profession, Color hairColor)
			: base(balance, race, sexIsMale, birthDate, race.GenerateName(sexIsMale, surmame), profession, hairColor)
		{ }

		protected override ActionResult DoImplementation()
		{
			return this.Wander();
		}
	}
}
