using Roguelike.Core.Localization;

namespace Roguelike.Core.Objects.Diseases
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
