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
			var language = _holder.GetWorld().Game.Language;
			return string.Format(
				KnownPersons.Contains(other) ? language.Talk.KnownPersonFormat : language.Talk.UnknownPersonFormat,
				_holder.SexIsMale ? language.Character.SexIsMale : language.Character.SexIsFemale,
				_holder.Race.GetName(language.Character.Races),
				_holder.Name);
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
				Topic.WhoAreYou,
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
			if (topic == Topic.WhoAreYou)
			{
				bool isKnown = KnownPersons.Contains(other);

				if (!isKnown)
				{
					GetAcquainted(other);
				}

				return new Text(string.Format(
					CultureInfo.InvariantCulture,
					language.Talk.AnswerFormats.GetAcquainted,
					isKnown ? language.Talk.AnswerFormats.AlreadyKnown : string.Empty,
					_holder.Name,
					_holder.Profession.GetName(language.Character.Professions),
					_holder.GetAge(_holder.GetWorld().Time),
					other.Name));
			}
			else
			{
				throw new NotImplementedException("I have nothing to say.");
			}
		}
	}
}
