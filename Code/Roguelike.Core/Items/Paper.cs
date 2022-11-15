using System;

using Roguelike.Core.Interfaces;
using Roguelike.Core.Localization;

namespace Roguelike.Core.Items
{
	public class Paper : IItemAspect
	{
		private readonly Func<LanguageBooks, string> _getTitle;
		private readonly Func<LanguageBooks, string> _getText;

		public Paper(Func<LanguageBooks, string> getTitle, Func<LanguageBooks, string> getText)
		{
			_getTitle = getTitle;
			_getText = getText;
		}

		public string GetTitle(LanguageBooks language)
		{
			return _getTitle(language);
		}

		public string GetText(LanguageBooks language)
		{
			return _getText(language);
		}
	}
}
