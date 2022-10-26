using System;
using System.Collections.Generic;
using System.Globalization;

using Roguelike.Core.Chat;
using Roguelike.Core.Interfaces;
using Roguelike.Core.Localization;

namespace Roguelike.Core.ActiveObjects
{
	public abstract class Humanoid : Alive, IHumanoid
	{
		#region Properties

		public string Name
		{ get; private set; }

		public IManequin Manequin
		{ get; }

		public IDictionary<Skill, int> Skills
		{ get; }

		public IDictionary<Skill, double> SkillExperience
		{ get; }

		public ITransport Transport
		{
			get { return _transport; }
			set
			{
				_transport = value;
				CurrentCell.RefreshView(false);
			}
		}

		#endregion

		#region Implementation of IInterlocutor

		private readonly ICollection<Humanoid> _knownPersons = new HashSet<Humanoid>();
		private readonly IDictionary<Humanoid, Attitude> _attitudes = new Dictionary<Humanoid, Attitude>();
		private ITransport _transport;

		public SocialGroup SocialGroup
		{ get { return SocialGroup.No; } }

		public string GetName(Humanoid interlocutor)
		{
			return Name;
		}

		public Attitude GetAttitude(Humanoid interlocutor)
		{
			Attitude attitude;
			return _attitudes.TryGetValue(interlocutor, out attitude)
				? attitude
				: SocialGroup.GetAttitude(interlocutor.SocialGroup);
		}

		public ICollection<Topic> GetTopics(Humanoid interlocutor)
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

		public void GetAcquainted(Humanoid other)
		{
			_knownPersons.Add(other);
			other._knownPersons.Add(this);
		}

		public Text Discuss(Humanoid interlocutor, Topic topic, Language language)
		{
			if (topic == Topic.WhatIsYourName)
			{
				if (_knownPersons.Contains(interlocutor))
				{
					return new Text(string.Format(
						CultureInfo.InvariantCulture,
						language.Talk.AnswerFormats.NameAgain,
						Name));
				}
				else
				{
					GetAcquainted(interlocutor);
					return new Text(string.Format(
						CultureInfo.InvariantCulture,
						language.Talk.AnswerFormats.NameFirst,
						Name,
						interlocutor.Name));
				}
			}
			else if (topic == Topic.HowOldAreYou)
			{
				return new Text(string.Format(
					CultureInfo.InvariantCulture,
					language.Talk.AnswerFormats.Age,
					Age));
			}
			else
			{
				throw new NotImplementedException("I have nothing to say.");
			}
		}

		#endregion

		protected Humanoid(bool sexIsMale, Time birthDate, IProperties properties, IEnumerable<Item> inventory, string name)
			: base(sexIsMale, birthDate, properties, inventory)
		{
			if (string.IsNullOrEmpty(name)) throw new ArgumentNullException(nameof(name));

			Name = name;

			Manequin = new Manequin(this);

			Skills = new Dictionary<Skill, int>();
			SkillExperience = new Dictionary<Skill, double>();
		}

		public override Body CreateBody()
		{
			return ActiveObjects.Body.CreateHumanoid();
		}
	}
}
