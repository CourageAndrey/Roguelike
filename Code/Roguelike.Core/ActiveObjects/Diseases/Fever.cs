using Roguelike.Core.Localization;

namespace Roguelike.Core.ActiveObjects.Diseases
{
	public class Fever : Disease
	{
		public override string GetName(LanguageDiseases language)
		{
			return language.Fever;
		}

		public override string ToString()
		{
			return nameof(Fever);
		}
	}
}
