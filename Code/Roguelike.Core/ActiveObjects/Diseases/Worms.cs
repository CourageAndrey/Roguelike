using Roguelike.Core.Localization;

namespace Roguelike.Core.ActiveObjects.Diseases
{
	public class Worms : Disease
	{
		public override string GetName(LanguageDiseases language)
		{
			return language.Worms;
		}

		public override string ToString()
		{
			return nameof(Worms);
		}
	}
}
