using Roguelike.Core.Localization;

namespace Roguelike.Core.ActiveObjects.Diseases
{
	public class Lues : Disease
	{
		public override string GetName(LanguageDiseases language)
		{
			return language.Lues;
		}

		public override string ToString()
		{
			return nameof(Lues);
		}
	}
}
