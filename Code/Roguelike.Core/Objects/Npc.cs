using System.Drawing;

using Roguelike.Core.Configuration;
using Roguelike.Core.Localization;
using Roguelike.Core.Mechanics;
using Roguelike.Core.Places;
using Roguelike.Core.RolePlaying;

namespace Roguelike.Core.Objects
{
	public class Npc : Humanoid
	{
		#region Properties



		#endregion

		public Npc(Balance balance, Race race, bool sexIsMale, Time birthDate, string surname, Profession profession, Color hairColor, Haircut haircut, Settlement birthPlace, Language language)
			: base(balance, race, sexIsMale, birthDate, race.GenerateName(sexIsMale, surname, language.Character.Races), profession, hairColor, haircut, birthPlace)
		{ }

		protected override ActionResult DoImplementation()
		{
			return this.Wander();
		}
	}
}
