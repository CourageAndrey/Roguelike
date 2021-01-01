using System.Collections.Generic;

using Roguelike.Core.ActiveObjects;
using Roguelike.Core.Chat;

namespace Roguelike.Core.Interfaces
{
	public interface IInterlocutor
	{
		string GetName(Humanoid interlocutor);

		Attitude GetAttitude(Humanoid interlocutor);

		ICollection<Topic> GetTopics(Humanoid interlocutor);

		Text Discuss(Humanoid interlocutor, Topic topic);
	}
}
