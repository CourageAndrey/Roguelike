using System;
using System.Collections.Generic;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Threading;

using Roguelike.Core.Aspects;
using Roguelike.Core.Configuration;
using Roguelike.Core.Interfaces;
using Roguelike.Core.Localization;
using Roguelike.Core.Objects;

namespace Roguelike.Core.Objects
{
	public abstract class Alive : Active, IAlive
	{
		#region Properties

		public bool SexIsMale
		{ get; private set; }

		public Time BirthDate
		{ get; }

		public Properties Properties
		{ get { return this.GetAspect<Properties>(); } }

		public Body Body
		{ get { return this.GetAspect<Body>(); } }

		public State State
		{ get { return this.GetAspect<State>(); } }

		public Inventory Inventory
		{ get { return this.GetAspect<Inventory>(); } }

		public Fighter Fighter
		{ get { return this.GetAspect<Fighter>(); } }

		public bool IsDead
		{ get; private set; }

		public string DeadReason
		{ get; private set; }

		public decimal Weight
		{ get; private set; }

		public double Toughness
		{ get { return Properties.Endurance; } }

		public double Speed
		{
			get
			{
				if (CurrentCell != null)
				{
					var balance = this.GetWorld().Balance;
					return 1 + Math.Pow(balance.Player.ReflexesRate, Properties.Reaction - balance.Player.ReflexesAverage);
				}
				else
				{
					return 1;
				}
			}
		}

		public abstract Color SkinColor
		{ get; }

		#endregion

		#region Events

		public event ValueChangedEventHandler<IMassy, decimal> WeightChanged;

		public event EventHandler<IAlive, string> OnDeath;

		protected void RaiseWeightChanged(decimal oldWeight, decimal newWeight)
		{
			var handler = Volatile.Read(ref WeightChanged);
			if (handler != null)
			{
				handler(this, oldWeight, newWeight);
			}
		}

		public virtual Corpse Die(string reason)
		{
			IsDead = true;
			DeadReason = reason;

			var handler = Volatile.Read(ref OnDeath);
			if (handler != null)
			{
				handler(this, reason);
			}

			string deathMessage;
			var game = this.GetGame();
			if (CurrentCell != null)
			{
				deathMessage = string.Format(CultureInfo.InvariantCulture, game.Language.LogActionFormats.Death, GetDescription(game.Language, game.Hero), reason);
			}
			else
			{
				deathMessage = $"{this.GetDescription(game.Language, game.Hero)} die: {reason}";
			}
			this.WriteToLog(deathMessage);

			var corpse = new Corpse(this);
			if (CurrentCell != null)
			{
				CurrentCell.AddObject(corpse);
				CurrentCell.RemoveObject(this);
			}

			return corpse;
		}

		#endregion

		protected Alive(Balance balance, bool sexIsMale, Time birthDate, Properties properties, IEnumerable<IItem> inventory)
		{
			if (properties == null) throw new ArgumentNullException(nameof(properties));
			if (inventory == null) throw new ArgumentNullException(nameof(inventory));

			SexIsMale = sexIsMale;
			BirthDate = birthDate;

			void updateWeight(IMassy massy, decimal oldWeight, decimal newWeight)
			{
				var weight = GetTotalWeigth();
				RaiseWeightChanged(Weight, weight);
				Weight = weight;
			}

			var body = CreateBody();
			body.WeightChanged += updateWeight;

			var _inventory = new Inventory(this, inventory);
			_inventory.WeightChanged += updateWeight;

			AddAspects(
				properties,
				body,
				new State(balance, this),
				_inventory,
				new Fighter(this));

			Weight = GetTotalWeigth();
		}

		protected virtual decimal GetTotalWeigth()
		{
			return	(decimal) Toughness * Body.Weight +
					Inventory.Weight +
					Fighter.Weight;
		}

		public abstract Body CreateBody();

		public virtual ActionResult DropItem(IItem item)
		{
			var world = this.GetWorld();
			var game = world.Game;
			var balance = world.Balance;
			var language = game.Language.LogActionFormats;

			var itemsPile = CurrentCell.Objects.OfType<ItemsPile>().SingleOrDefault();
			if (itemsPile != null)
			{
				itemsPile.PutItem(item);
			}
			else
			{
				new ItemsPile(item).MoveTo(CurrentCell);
			}

			Inventory.Items.Remove(item);

			return new ActionResult(
				Time.FromTicks(balance.Time, balance.ActionLongevity.DropItem),
				string.Format(CultureInfo.InvariantCulture, language.DropItem, GetDescription(game.Language, game.Hero), item));
		}

		public sealed override ActionResult Do()
		{
			var result = DoImplementation();
			return new ActionResult(result.Longevity.Scale(Speed), result.LogMessages);
		}

		protected abstract ActionResult DoImplementation();

		private ActionResult EatDrink(
			IItem food,
			Func<ActionLongevityBalance, long> getLongevity,
			Func<LanguageLogActionFormats, string> getLogFormat)
		{
			var world = this.GetWorld();
			var game = world.Game;
			Balance balance = world.Balance;
			var language = game.Language;

			State.EatDrink(food.GetAspect<Nutricious>(), language);

			return new ActionResult(
				Time.FromTicks(balance.Time, getLongevity(balance.ActionLongevity)),
				string.Format(CultureInfo.InvariantCulture, getLogFormat(language.LogActionFormats), GetDescription(game.Language, game.Hero), food.GetDescription(language, this)));
		}

		public ActionResult Eat(IItem food)
		{
			return EatDrink(food, b => b.Eat, f => f.Eat);
		}

		public ActionResult Drink(IItem drink)
		{
			return EatDrink(drink, b => b.Drink, f => f.Drink);
		}
	}
}
