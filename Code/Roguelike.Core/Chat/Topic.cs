using System;

using Roguelike.Core.Localization;

namespace Roguelike.Core.Chat
{
	public class Topic
	{
		private readonly Func<LanguageQuestions, string> _ask;

		private Topic(Func<LanguageQuestions, string> ask)
		{
			_ask = ask;
		}

		public static readonly Topic WhoAreYou = new Topic(language => language.WhoAreYou);
		public static readonly Topic WhereAreWeNow = new Topic(language => language.WhereAreWeNow);
		public static readonly Topic WhereAreYouFrom = new Topic(language => language.WhereAreYouFrom);

		public string Ask(LanguageQuestions language)
		{
			return _ask(language);
		}
	}
}
