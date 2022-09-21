﻿using System;
using System.Collections.Generic;
using System.Globalization;

using Roguelike.Core.Chat;
using Roguelike.Core.Interfaces;
using Roguelike.Core.Items;
using Roguelike.Core.Localization;

namespace Roguelike.Core.ActiveObjects
{
	public abstract class Humanoid : AliveObject, IInterlocutor, IManequin
	{
		#region Properties

		public string Name
		{ get; private set; }

		public IHeadWear HeadWear
		{ get; private set; }

		public IUpperBodyWear UpperBodyWear
		{ get; private set; }

		public ILowerBodyWear LowerBodyWear
		{ get; private set; }

		public ICoverWear CoverWear
		{ get; private set; }

		public IHandWear HandsWear
		{ get; private set; }

		public IFootWear FootsWear
		{ get; private set; }

		public ICollection<INecklace> Necklaces
		{ get; }

		public ICollection<IRing> Rings
		{ get; }

		public IDictionary<Skill, int> Skills
		{ get; }

		public IDictionary<Skill, double> SkillExperience
		{ get; }

		#endregion

		#region Implementation of IInterlocutor

		private readonly ICollection<Humanoid> _knownPersons = new HashSet<Humanoid>();
		private readonly IDictionary<Humanoid, Attitude> _attitudes = new Dictionary<Humanoid, Attitude>();

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
						language.AnswerFormatNameAgain,
						Name));
				}
				else
				{
					GetAcquainted(interlocutor);
					return new Text(string.Format(
						CultureInfo.InvariantCulture,
						language.AnswerFormatNameFirst,
						Name,
						interlocutor.Name));
				}
			}
			else if (topic == Topic.HowOldAreYou)
			{
				return new Text(string.Format(
					CultureInfo.InvariantCulture,
					language.AnswerFormatAge,
					Age.Year));
			}
			else
			{
				throw new NotImplementedException("I have nothing to say.");
			}
		}

		#endregion

		protected Humanoid(bool sexIsMale, Time age, IProperties properties, IInventory inventory, string name)
			: base(sexIsMale, age, properties, inventory)
		{
			if (string.IsNullOrEmpty(name)) throw new ArgumentNullException(nameof(name));

			Name = name;

			HeadWear = new Naked(this);
			UpperBodyWear = new Naked(this);
			LowerBodyWear = new Naked(this);
			CoverWear = new Naked(this);
			HandsWear = new Naked(this);
			FootsWear = new Naked(this);
			Necklaces = new List<INecklace>();
			Rings = new List<IRing>();

			Skills = new Dictionary<Skill, int>();
			SkillExperience = new Dictionary<Skill, double>();
		}

		public override Body CreateBody()
		{
			return ActiveObjects.Body.CreateHumanoid();
		}

		public override List<Interaction> GetAvailableInteractions(Object actor)
		{
			var result = base.GetAvailableInteractions(actor);

			var game = CurrentCell.Region.World.Game;
			var balance = game.Balance;
			var language = game.Language;

			result.Add(new Interaction(language.InteractionChat, true, target => game.UserInterface.BeginChat(game, this)));
			result.Add(new Interaction(language.InteractionTrade, true, target => game.UserInterface.BeginTrade(game, this)));
			result.Add(new Interaction(language.InteractionPickpocket, true, target => game.UserInterface.BeginPickpocket(game, this)));

			return result;
		}
	}
}
