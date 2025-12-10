using Roguelike.Core.Localization;

namespace Roguelike.Core.RolePlaying
{
	public class Trait
	{
		#region Properties

		private readonly Func<LanguageTraits, string> _getName;

		#endregion

		private Trait(Func<LanguageTraits, string> nameGetter)
		{
			_getName = nameGetter;
		}

		public string GetName(LanguageTraits language)
		{
			return _getName(language);
		}

		#region List

		/*-  increase characteristics: very strong, very intelligent, etc.
		   - increase skills: inborn hunter, inborn healer, etc.
		   - increase skills progress: fast learner of ...
		   - long-lived or short-lived, which modify maximum character life span
		   - increase (up to full immunity) or decrease (up to critical vulnerability) elemental resistance
		   - increase or decrease derived numbers like movement speed, reaction speed, carrying capacity, shooting range, hunger and thirst, etc.*/

		#endregion
	}
}
