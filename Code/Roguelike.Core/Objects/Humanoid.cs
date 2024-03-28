using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;

using Roguelike.Core.Aspects;
using Roguelike.Core.Configuration;
using Roguelike.Core.Interfaces;
using Roguelike.Core.Localization;

namespace Roguelike.Core.Objects
{
	public abstract class Humanoid : Alive, IHumanoid
	{
		#region Properties

		public string Name
		{ get; }

		public Race Race
		{ get; }

		public Mannequin Mannequin
		{ get { return this.GetAspect<Mannequin>(); } }

		public Skilled Skilled
		{ get { return this.GetAspect<Skilled>(); } }

		public Rider Rider
		{ get { return this.GetAspect<Rider>(); } }

		public Profession Profession
		{ get; }

		public Interlocutor Interlocutor
		{ get { return this.GetAspect<Interlocutor>(); } }

		public Appearance Appearance
		{ get { return this.GetAspect<Appearance>(); } }

		public override Color SkinColor
		{ get { return Race.SkinColor; } }

		#endregion

		protected Humanoid(Balance balance, Race race, bool sexIsMale, Time birthDate, string name, Profession profession, Color hairColor, Haircut haircut)
			: base(balance, sexIsMale, birthDate, race.GetProperties(profession), race.GetItems(profession))
		{
			if (string.IsNullOrEmpty(name)) throw new ArgumentNullException(nameof(name));

			Name = name;
			Race = race;
			Profession = profession;

			var mannequin = new Mannequin(this);
			mannequin.EquipmentChanged += m => CurrentCell?.RefreshView(false);

			AddAspects(
				mannequin,
				new Skilled(),
				new Rider(this),
				new Interlocutor(this),
				new Appearance(hairColor, haircut));

#warning Need to recalculate total weight here because mannequin is not included now - even if it's empty.
		}

		protected override decimal GetTotalWeigth()
		{
			var mannequin = this.TryGetAspect<Mannequin>();
			return base.GetTotalWeigth() +
					(mannequin?.GetAllItems() ?? Array.Empty<IItem>() as IEnumerable<IItem>).Sum(wear => wear.Weight);
		}

		public override Body CreateBody()
		{
			return Body.CreateHumanoid();
		}

		public override string GetDescription(Language language, IAlive forWhom)
		{
			return forWhom == this || (forWhom as Humanoid)?.Interlocutor?.KnownPersons?.Contains(this) == true
				? Name
				: (SexIsMale
					? language.Objects.HumanMale
					: language.Objects.HumanFemale);
		}
	}
}
