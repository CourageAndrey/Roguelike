using Roguelike.Core.Localization;

namespace Roguelike.Core.Interfaces
{
	public interface IPaper : IItem
	{
		string GetTitle(LanguageBooks language);

		string GetText(LanguageBooks language);
	}
}
