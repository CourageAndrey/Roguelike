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

		public Manequin Manequin
		{ get { return this.GetAspect<Manequin>(); } }

		public Skilled Skilled
		{ get { return this.GetAspect<Skilled>(); } }

		public Rider Rider
		{ get { return this.GetAspect<Rider>(); } }

		public Profession Profession
		{ get; }

		public Interlocutor Interlocutor
		{ get { return this.GetAspect<Interlocutor>(); } }

		public override Color SkinColor
		{ get { return Race.SkinColor; } }

		#endregion

		protected Humanoid(Balance balance, Race race, bool sexIsMale, Time birthDate, string name, Profession profession)
			: base(balance, sexIsMale, birthDate, race.GetProperties(profession), race.GetItems(profession))
		{
			if (string.IsNullOrEmpty(name)) throw new ArgumentNullException(nameof(name));

			Name = name;
			Race = race;
			Profession = profession;

			var manequin = new Manequin(this);
			manequin.EquipmentChanged += m => CurrentCell?.RefreshView(false);

			AddAspects(
				manequin,
				new Skilled(),
				new Rider(this),
				new Interlocutor(this));

#warning Need to recalculate total weight here because manequin is not included now - even if it's empty.
		}

		protected override decimal GetTotalWeigth()
		{
			var manequin = this.TryGetAspect<Manequin>();
			return	base.GetTotalWeigth() +
					(manequin?.GetAllItems() ?? new IItem[0] as IEnumerable<IItem>).Sum(wear => wear.Weight);
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
