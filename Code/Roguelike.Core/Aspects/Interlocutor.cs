using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;

using Roguelike.Core.Chat;
using Roguelike.Core.Interfaces;
using Roguelike.Core.Localization;
using Roguelike.Core.Places;

namespace Roguelike.Core.Aspects
{
	public class Interlocutor : AspectWithHolder<IHumanoid>
	{
		#region Properties

		private readonly IDictionary<IHumanoid, Attitude> _attitudes = new Dictionary<IHumanoid, Attitude>();

		public ICollection<IHumanoid> KnownPersons
		{ get { return _attitudes.Keys; } }

		public SocialGroup SocialGroup
		{ get { return SocialGroup.No; } }

		#endregion

		public Interlocutor(IHumanoid holder)
			: base(holder)
		{ }

		public string GetName(IHumanoid other)
		{
			var language = Holder.GetWorld().Game.Language;
			return string.Format(
				KnownPersons.Contains(other) ? language.Talk.KnownPersonFormat : language.Talk.UnknownPersonFormat,
				Holder.SexIsMale ? language.Character.SexIsMale : language.Character.SexIsFemale,
				Holder.Race.GetName(language.Character.Races),
				Holder.Name);
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
			other.Interlocutor._attitudes[Holder] = Attitude.Neutral;
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
					Holder.Name,
					Holder.Profession.GetName(language.Character.Professions),
					Holder.GetAge(Holder.GetWorld().Time),
					other.Name));
			}
			else if (topic == Topic.WhereAreYouFrom)
			{
#warning Calculate location
				return new Text(string.Format(
					CultureInfo.InvariantCulture,
					language.Talk.AnswerFormats.Origination,
					other.GetRegion().Places.OfType<Settlement>().Single().GetName(language)));
			}
			else if (topic == Topic.WhereAreWeNow)
			{
				return new Text(string.Format(
					CultureInfo.InvariantCulture,
					language.Talk.AnswerFormats.CurrentLocation,
					Holder.BirthPlace.GetName(language)));
			}
			else
			{
				throw new NotImplementedException("I have nothing to say.");
			}
		}
	}
}
