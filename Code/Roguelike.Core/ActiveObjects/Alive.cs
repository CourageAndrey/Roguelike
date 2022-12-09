using System;
using System.Collections.Generic;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Threading;

using Roguelike.Core.Configuration;
using Roguelike.Core.Interfaces;
using Roguelike.Core.Items;
using Roguelike.Core.Localization;
using Roguelike.Core.Objects;

namespace Roguelike.Core.ActiveObjects
{
	public abstract class Alive : Active, IAlive
	{
		#region Properties

		public bool SexIsMale
		{ get; private set; }

		public Time BirthDate
		{ get; }

		public IProperties Properties
		{ get; private set; }

		public IBody Body
		{ get; }

		public IState State
		{ get; }

		public ICollection<IItem> Inventory
		{ get; }

		public bool IsDead
		{ get; private set; }

		public string DeadReason
		{ get; private set; }

		public decimal Weight
		{ get; private set; }

		public IItem WeaponToFight
		{ get; private set; }

		public bool IsAgressive
		{ get; private set; }

		public double Toughness
		{ get { return Properties.Endurance; } }

		public double Speed
		{
			get
			{
				if (CurrentCell != null)
				{
					var balance = this.GetGame().Balance;
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

		public event ValueChangedEventHandler<IAlive, bool> AgressiveChanged;

		public event ValueChangedEventHandler<IAlive, IItem> WeaponChanged;

		public event EventHandler<IAlive, string> OnDeath;

		protected void RaiseWeightChanged(decimal oldWeight, decimal newWeight)
		{
			var handler = Volatile.Read(ref WeightChanged);
			if (handler != null)
			{
				handler(this, oldWeight, newWeight);
			}
		}

		protected void RaiseAgressiveChanged(bool oldAgressive, bool newAgressive)
		{
			var handler = Volatile.Read(ref AgressiveChanged);
			if (handler != null)
			{
				handler(this, oldAgressive, newAgressive);
			}
		}

		protected void RaiseWeaponChanged(IItem oldWeapon, IItem newWeapon)
		{
			var handler = Volatile.Read(ref WeaponChanged);
			if (handler != null)
			{
				handler(this, oldWeapon, newWeapon);
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

		protected Alive(Balance balance, bool sexIsMale, Time birthDate, IProperties properties, IEnumerable<Item> inventory)
		{
			if (properties == null) throw new ArgumentNullException(nameof(properties));
			if (inventory == null) throw new ArgumentNullException(nameof(inventory));

			SexIsMale = sexIsMale;
			BirthDate = birthDate;
			Properties = properties;
			State = new State(balance, this);
			WeaponToFight = new Unarmed(this);

			void updateWeight()
			{
				var weight = GetTotalWeigth();
				RaiseWeightChanged(Weight, weight);
				Weight = weight;
			}

			Body = CreateBody();
			Body.WeightChanged += (sender, value, newValue) => updateWeight();

			void updateOnItemChange(IMassy item, decimal oldWeight, decimal newWeight)
			{
				updateWeight();
			}

			var _inventory = new EventCollection<IItem>(inventory);
			_inventory.ItemAdded += (sender, args) =>
			{
				updateWeight();

				args.Item.WeightChanged += updateOnItemChange;

				args.Item.RaisePicked(this);
			};
			_inventory.ItemRemoved += (sender, args) =>
			{
				args.Item.RaiseDropped(this);

				args.Item.WeightChanged -= updateOnItemChange;

				updateWeight();
			};
			Inventory = _inventory;

			Weight = GetTotalWeigth();
		}

		protected virtual decimal GetTotalWeigth()
		{
			return	(decimal) Toughness * Body.Weight +
					Inventory.Sum(item => item.Weight) +
					WeaponToFight.Weight;
		}

		public ActionResult ChangeAggressive(bool agressive)
		{
			int time;
			string logMessage;
			var game = this.GetGame();
			var language = game.Language.LogActionFormats;
			var balance = game.Balance;
			Activity newActivity = null;

			if (IsAgressive != agressive)
			{
				IsAgressive = agressive;

				RaiseAgressiveChanged(!agressive, agressive);

				if (agressive)
				{
					WeaponToFight.GetAspect<Weapon>().RaisePreparedForBattle(this);
					newActivity = Activity.Guards;
				}
				else
				{
					WeaponToFight.GetAspect<Weapon>().RaiseStoppedBattle(this);
					newActivity = Activity.Stands;
				}

				time = balance.ActionLongevity.ChangeAgressive;
				logMessage = string.Format(
					CultureInfo.InvariantCulture,
					IsAgressive ? language.StartFight : language.StopFight,
					GetDescription(game.Language, game.Hero),
					WeaponToFight);
			}
			else
			{
				time = balance.ActionLongevity.Disabled;
				logMessage = string.Format(CultureInfo.InvariantCulture, language.ChangeFightModeDisabled, GetDescription(game.Language, game.Hero));
			}
			return new ActionResult(Time.FromTicks(balance.Time, time), logMessage, newActivity);
		}

		public ActionResult ChangeWeapon(IItem weapon)
		{
			int time;
			string logMessage;
			var game = this.GetGame();
			var language = game.Language.LogActionFormats;
			var balance = game.Balance;

			var oldWeapon = WeaponToFight;
			if (oldWeapon != weapon)
			{
				WeaponToFight = weapon;

				if (!(oldWeapon is Unarmed))
				{
					Inventory.Add(oldWeapon);
				}
				if (!(weapon is Unarmed))
				{
					Inventory.Remove(weapon);
				}

				RaiseWeaponChanged(oldWeapon, weapon);

				time = balance.ActionLongevity.ChangeWeapon;
				logMessage = string.Format(
					CultureInfo.InvariantCulture,
					language.ChangeWeapon,
					GetDescription(game.Language, game.Hero),
					oldWeapon,
					weapon);
			}
			else
			{
				time = balance.ActionLongevity.Disabled;
				logMessage = string.Format(CultureInfo.InvariantCulture, language.ChangeWeaponDisabled, GetDescription(game.Language, game.Hero));
			}
			return new ActionResult(Time.FromTicks(balance.Time, time), logMessage);
		}

		public abstract Body CreateBody();

		public ActionResult Backstab(IAlive actor)
		{
			Die("backstabbed");
			return null;
#warning Finish implementation, translate it and make not so easy.
		}

		public virtual ActionResult Attack(IAlive target)
		{
			var world = this.GetWorld();
			var game = world.Game;
			var balance = game.Balance;
			var language = game.Language;
			var random = new Random(DateTime.Now.Millisecond);

			int hitPossibility = balance.Player.BaseHitPossibility;
			hitPossibility += ((int) Properties.Reaction - (int) target.Properties.Reaction) * 10;
			if (random.Next(0, 100) < hitPossibility)
			{
				target.Die(string.Format(CultureInfo.InvariantCulture, language.DeathReasons.Killed, GetDescription(game.Language, game.Hero)));
			}

			return new ActionResult(
				Time.FromTicks(balance.Time, (int)(balance.ActionLongevity.Attack)),
				string.Format(CultureInfo.InvariantCulture, language.LogActionFormats.Attack, GetDescription(game.Language, game.Hero), target, WeaponToFight),
				Activity.Fights);
		}

		public virtual ActionResult Shoot(Cell target)
		{
			var world = this.GetWorld();
			var game = world.Game;
			var balance = game.Balance;
			var language = game.Language;
			var random = new Random(DateTime.Now.Millisecond);

			var missile = Inventory.Select<IItem, Missile>().First();

			if (!this.GetPosition().Equals(target.Position))
			{
				var direction = this.GetPosition().GetDirection(target.Position);
				var region = this.GetRegion();
				int z = this.GetPosition().Z;

				// take all obstacles into account
				var track = new List<Cell>();
				var step = CurrentCell;
				int dx = target.Position.X - this.GetPosition().X,
					dy = target.Position.Y - this.GetPosition().Y;
				double	distance = Math.Sqrt(dx * dx + dy * dy),
						sx = dx / distance,
						sy = dy / distance,
						x = this.GetPosition().X,
						y = this.GetPosition().Y;
				while (distance > 1)
				{
					x += sx;
					y += sy;
					var cell = region.GetCell(
						(int) Math.Round(x, MidpointRounding.AwayFromZero),
						(int) Math.Round(y, MidpointRounding.AwayFromZero),
						z);
					if (!track.Contains(cell))
					{
						track.Add(cell);
					}
					distance--;
				}
				track.Remove(target);

				if (track.Count > 0)
				{
					// animate arrow fly
					game.UserInterface.AnimateShoot(direction, track, missile);
				}
			}

			// calculate damage
			var aim = target.Objects.OfType<IAlive>().FirstOrDefault();
			if (aim != null)
			{
				int hitPossibility = balance.Player.BaseHitPossibility;
				hitPossibility += ((int) Properties.Perception - (int) aim.Properties.Reaction) * 10;
				if (random.Next(0, 100) < hitPossibility)
				{
					aim.Die(string.Format(CultureInfo.InvariantCulture, language.DeathReasons.Killed, GetDescription(game.Language, game.Hero)));
				}
			}

			// remove missile
			Inventory.Remove(missile);

			return new ActionResult(
				Time.FromTicks(balance.Time, (int)(balance.ActionLongevity.Shoot)),
				string.Format(CultureInfo.InvariantCulture, language.LogActionFormats.Shoot, GetDescription(game.Language, game.Hero), target, WeaponToFight),
				Activity.Fights);
		}

		public virtual ActionResult DropItem(IItem item)
		{
			var world = this.GetWorld();
			var game = world.Game;
			var balance = game.Balance;
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

			Inventory.Remove(item);

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
			Balance balance = game.Balance;
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
