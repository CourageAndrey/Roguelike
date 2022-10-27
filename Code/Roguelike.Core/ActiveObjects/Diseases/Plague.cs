using Roguelike.Core.Localization;

namespace Roguelike.Core.ActiveObjects.Diseases
{
	public class Plague : Disease
	{
		public override string GetName(LanguageDiseases language)
		{
			return language.Plague;
		}

		public override string ToString()
		{
			return nameof(Plague);
		}
	}
}
