using System;
using System.Collections.Generic;
using System.Drawing;
using System.Globalization;
using System.Linq;

using Roguelike.Core.Chat;
using Roguelike.Core.Configuration;
using Roguelike.Core.Interfaces;
using Roguelike.Core.Localization;
using Roguelike.Core.Objects;

namespace Roguelike.Core.ActiveObjects
{
	public abstract class Humanoid : Alive, IHumanoid
	{
		#region Properties

		public string Name
		{ get; }

		public Race Race
		{ get; }

		public IManequin Manequin
		{ get; }

		public IDictionary<Skill, int> Skills
		{ get; }

		public IDictionary<Skill, double> SkillExperience
		{ get; }

		public Transport Transport
		{
			get { return _transport; }
			set
			{
				_transport = value;
				CurrentCell.RefreshView(false);
			}
		}

		public override Color SkinColor
		{ get { return Race.SkinColor; } }

		#endregion

		#region Implementation of IInterlocutor

		private readonly ICollection<Humanoid> _knownPersons = new HashSet<Humanoid>();
		private readonly IDictionary<Humanoid, Attitude> _attitudes = new Dictionary<Humanoid, Attitude>();
		private Transport _transport;

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
					this.GetAge(this.GetWorld().Time)));
			}
			else
			{
				throw new NotImplementedException("I have nothing to say.");
			}
		}

		#endregion

		protected Humanoid(Balance balance, Race race, bool sexIsMale, Time birthDate, IProperties properties, IEnumerable<Item> inventory, string name)
			: base(balance, sexIsMale, birthDate, properties, inventory)
		{
			if (string.IsNullOrEmpty(name)) throw new ArgumentNullException(nameof(name));

			Name = name;
			Race = race;

			Manequin = new Manequin(this);
			Manequin.EquipmentChanged += m => CurrentCell?.RefreshView(false);

			Skills = new Dictionary<Skill, int>();
			SkillExperience = new Dictionary<Skill, double>();
		}

		protected override decimal GetTotalWeigth()
		{
			return	base.GetTotalWeigth() +
					(Manequin?.GetAllItems() ?? new IItem[0] as IEnumerable<IItem>).Sum(wear => wear.Weight);
		}

		public override Body CreateBody()
		{
			return ActiveObjects.Body.CreateHumanoid();
		}

		public override string GetDescription(Language language, IAlive forWhom)
		{
			return forWhom == this || (forWhom as Humanoid)?._knownPersons.Contains(this) == true
				? Name
				: (SexIsMale
					? language.Objects.HumanMale
					: language.Objects.HumanFemale);
		}
	}
}
