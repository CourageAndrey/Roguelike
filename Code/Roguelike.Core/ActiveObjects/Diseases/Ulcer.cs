using Roguelike.Core.Localization;

namespace Roguelike.Core.ActiveObjects.Diseases
{
	public class Ulcer : Disease
	{
		public override string GetName(LanguageDiseases language)
		{
			return language.Ulcer;
		}

		public override string ToString()
		{
			return nameof(Ulcer);
		}
	}
}
