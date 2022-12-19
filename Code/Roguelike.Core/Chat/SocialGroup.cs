using System;

using Roguelike.Core.Localization;

namespace Roguelike.Core.Chat
{
	public class SocialGroup
	{
		#region Properties

		private readonly Func<Language, string> _getName;

		#endregion

		private SocialGroup(Func<Language, string> nameGetter)
		{
			_getName = nameGetter;
		}

		public string GetName(Language language)
		{
			return _getName(language);
		}

		public Attitude GetAttitude(SocialGroup other)
		{
#warning Implement default group attitudes.
			return Attitude.Neutral;
		}

		public static readonly SocialGroup No = new SocialGroup(language => string.Empty);
	}
}
