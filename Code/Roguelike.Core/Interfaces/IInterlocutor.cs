using System.Collections.Generic;

using Roguelike.Core.ActiveObjects;
using Roguelike.Core.Chat;
using Roguelike.Core.Localization;

namespace Roguelike.Core.Interfaces
{
	public interface IInterlocutor
	{
		SocialGroup SocialGroup
		{ get; }

		string GetName(Humanoid interlocutor);

		Attitude GetAttitude(Humanoid interlocutor);

		ICollection<Topic> GetTopics(Humanoid interlocutor);

		Text Discuss(Humanoid interlocutor, Topic topic, Language language);
	}
}
