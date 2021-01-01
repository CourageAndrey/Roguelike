using System;

using Roguelike.Core.Localization;

namespace Roguelike.Core.Chat
{
	public class Topic
	{
		private readonly Func<Language, string> _ask;

		private Topic(Func<Language, string> ask)
		{
			_ask = ask;
		}

		public static readonly Topic WhatIsYourName = new Topic(language => language.QuestionWhatIsYourName);
		public static readonly Topic HowOldAreYou = new Topic(language => language.QuestionHowOldAreYou);
		public static readonly Topic WhatDoYouDo = new Topic(language => language.QuestionWhatDoYouDo);
		public static readonly Topic WhereAreWeNow = new Topic(language => language.QuestionWhereAreWeNow);
		public static readonly Topic WhereAreYouFrom = new Topic(language => language.QuestionWhereAreYouFrom);

		public string Ask(Language language)
		{
			return _ask(language);
		}
	}
}
