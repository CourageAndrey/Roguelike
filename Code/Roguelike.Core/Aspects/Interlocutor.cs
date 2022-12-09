using System;
using System.Collections.Generic;
using System.Globalization;

using Roguelike.Core.Chat;
using Roguelike.Core.Interfaces;
using Roguelike.Core.Localization;

namespace Roguelike.Core.Aspects
{
	public class Interlocutor : IAspect
	{
		#region Properties

		private readonly IHumanoid _holder;
		private readonly IDictionary<IHumanoid, Attitude> _attitudes = new Dictionary<IHumanoid, Attitude>();

		public ICollection<IHumanoid> KnownPersons
		{ get { return _attitudes.Keys; } }

		public SocialGroup SocialGroup
		{ get { return SocialGroup.No; } }

		#endregion

		public Interlocutor(IHumanoid holder)
		{
			_holder = holder;
		}

		public string GetName(IHumanoid other)
		{
			return _holder.Name;
		}

		public Attitude GetAttitude(IHumanoid other)
		{
			Attitude attitude;
			return _attitudes.TryGetValue(other, out attitude)
				? attitude
				: SocialGroup.GetAttitude(other.Interlocutor.SocialGroup);
		}

		public ICollection<Topic> GetTopics(IHumanoid other)
		{
			return new[]
			{
				Topic.WhatIsYourName,
				Topic.HowOldAreYou,
				Topic.WhatDoYouDo,
				Topic.WhereAreWeNow,
				Topic.WhereAreYouFrom,
			};
		}

		public void GetAcquainted(IHumanoid other)
		{
			_attitudes[other] = Attitude.Neutral;
			other.Interlocutor._attitudes[_holder] = Attitude.Neutral;
		}

		public Text Discuss(IHumanoid other, Topic topic, Language language)
		{
			if (topic == Topic.WhatIsYourName)
			{
				if (KnownPersons.Contains(other))
				{
					return new Text(string.Format(
						CultureInfo.InvariantCulture,
						language.Talk.AnswerFormats.NameAgain,
						_holder.Name));
				}
				else
				{
					GetAcquainted(other);
					return new Text(string.Format(
						CultureInfo.InvariantCulture,
						language.Talk.AnswerFormats.NameFirst,
						_holder.Name,
						other.Name));
				}
			}
			else if (topic == Topic.HowOldAreYou)
			{
				return new Text(string.Format(
					CultureInfo.InvariantCulture,
					language.Talk.AnswerFormats.Age,
					_holder.GetAge(_holder.GetWorld().Time)));
			}
			else
			{
				throw new NotImplementedException("I have nothing to say.");
			}
		}
	}
}
