using Roguelike.Core.Configuration;
using Roguelike.Core.Places;

using System.Drawing;

namespace Roguelike.Core.Objects
{
	public class Npc : Humanoid
	{
		#region Properties



		#endregion

		public Npc(Balance balance, Race race, bool sexIsMale, Time birthDate, string surname, Profession profession, Color hairColor, Haircut haircut, Settlement birthPlace)
			: base(balance, race, sexIsMale, birthDate, race.GenerateName(sexIsMale, surname), profession, hairColor, haircut, birthPlace)
		{ }

		protected override ActionResult DoImplementation()
		{
			return this.Wander();
		}
	}
}
