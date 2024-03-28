using System.Drawing;

using Roguelike.Core.Configuration;

namespace Roguelike.Core.Objects
{
	public class Npc : Humanoid
	{
		#region Properties



		#endregion

		public Npc(Balance balance, Race race, bool sexIsMale, Time birthDate, string surname, Profession profession, Color hairColor, Haircut haircut)
			: base(balance, race, sexIsMale, birthDate, race.GenerateName(sexIsMale, surname), profession, hairColor, haircut)
		{ }

		protected override ActionResult DoImplementation()
		{
			return this.Wander();
		}
	}
}
