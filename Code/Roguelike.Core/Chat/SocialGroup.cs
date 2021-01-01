using System;

namespace Roguelike.Core.Chat
{
	public class SocialGroup
	{
		public string Name
		{ get { return _nameGetter(); } }

		private readonly Func<string> _nameGetter;

		private SocialGroup(Func<string> nameGetter)
		{
			_nameGetter = nameGetter;
		}

		public Attitude GetAttitude(SocialGroup other)
		{
#warning Implement default group attitudes.
			return Attitude.Neutral;
		}

		public static readonly SocialGroup No = new SocialGroup(() => string.Empty);
	}
}
