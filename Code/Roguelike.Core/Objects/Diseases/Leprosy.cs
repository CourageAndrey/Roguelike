using Roguelike.Core.Localization;

namespace Roguelike.Core.Objects.Diseases
{
	public class Leprosy : Disease
	{
		public override string GetName(LanguageDiseases language)
		{
			return language.Leprosy;
		}

		public override string ToString()
		{
			return nameof(Leprosy);
		}
	}
}
