using System;
using System.Collections.Generic;

using Roguelike.Core.Interfaces;
using Roguelike.Core.Items;

namespace Roguelike.Core.ActiveObjects
{
	public abstract class Humanoid : AliveObject
	{
		#region Properties

		public string Name
		{ get; private set; }

		public Item WearHead
		{ get; private set; }

		public Item WearUpperBody
		{ get; private set; }

		public Item WearLowerBody
		{ get; private set; }

		public Item WearCover
		{ get; private set; }

		public Item WearHands
		{ get; private set; }

		public Item WearFoots
		{ get; private set; }

		public ICollection<Item> WearNecklaces
		{ get; }

		public ICollection<Item> WearRings
		{ get; }

		public IDictionary<Skill, int> Skills
		{ get; }

		public IDictionary<Skill, double> SkillExperience
		{ get; }

		#endregion

		protected Humanoid(bool sexIsMale, Time age, Properties properties, IInventory inventory, string name)
			: base(sexIsMale, age, properties, inventory)
		{
			if (string.IsNullOrEmpty(name)) throw new ArgumentNullException(nameof(name));

			Name = name;

			WearHead = new Naked(this);
			WearUpperBody = new Naked(this);
			WearLowerBody = new Naked(this);
			WearCover = new Naked(this);
			WearHands = new Naked(this);
			WearFoots = new Naked(this);
			WearNecklaces = new List<Item>();
			WearRings = new List<Item>();

			Skills = new Dictionary<Skill, int>();
			SkillExperience = new Dictionary<Skill, double>();
		}

		public override Body CreateBody()
		{
			return Body.CreateHumanoid(this);
		}

		public override List<Interaction> GetAvailableInteractions(Object actor)
		{
			var result = base.GetAvailableInteractions(actor);

			var game = CurrentCell.Region.World.Game;
			var balance = game.Balance;
			var language = game.Language;

			result.Add(new Interaction(language.InteractionChat, true, target => { throw new NotImplementedException(); }));
			result.Add(new Interaction(language.InteractionTrade, true, target => { throw new NotImplementedException(); }));
			result.Add(new Interaction(language.InteractionPickpocket, true, target => { throw new NotImplementedException(); }));

			return result;
		}
	}
}
