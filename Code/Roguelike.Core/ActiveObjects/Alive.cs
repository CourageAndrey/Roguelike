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
using Roguelike.Core.StaticObjects;

namespace Roguelike.Core.ActiveObjects
{
	public abstract class Alive : Active, IAlive
	{
		#region Properties

		public bool SexIsMale
		{ get; private set; }

		public Time BirthDate
		{ get; }

		public uint Age
		{
			get
			{
				if (CurrentCell != null)
				{
					var world = CurrentCell.Region.World;
					return (uint) (world.Time - BirthDate).Year;
				}
				else
				{
					return 0;
				}
			}
		}

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

		public IWeapon WeaponToFight
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
					var balance = CurrentCell.Region.World.Game.Balance;
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

		public event ValueChangedEventHandler<IRequireGravitation, decimal> WeightChanged;

		public event ValueChangedEventHandler<IAlive, bool> AgressiveChanged;

		public event ValueChangedEventHandler<IAlive, IWeapon> WeaponChanged;

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

		protected void RaiseWeaponChanged(IWeapon oldWeapon, IWeapon newWeapon)
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
			if (CurrentCell != null)
			{
				var language = CurrentCell.Region.World.Game.Language;
				deathMessage = string.Format(CultureInfo.InvariantCulture, language.LogActionFormats.Death, this, reason);
			}
			else
			{
				deathMessage = $"{this} die: {reason}";
			}
			WriteToLog(deathMessage);

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

			void updateOnItemChange(IRequireGravitation item, decimal oldWeight, decimal newWeight)
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
					Inventory.Sum(item => item.Weight);
		}

		public ActionResult ChangeAggressive(bool agressive)
		{
			int time;
			string logMessage;
			var game = CurrentCell.Region.World.Game;
			var language = game.Language.LogActionFormats;
			var balance = game.Balance;
			Activity newActivity = null;

			if (IsAgressive != agressive)
			{
				IsAgressive = agressive;

				RaiseAgressiveChanged(!agressive, agressive);

				if (agressive)
				{
					WeaponToFight.RaisePreparedForBattle(this);
					newActivity = Activity.Guards;
				}
				else
				{
					WeaponToFight.RaiseStoppedBattle(this);
					newActivity = Activity.Stands;
				}

				time = balance.ActionLongevity.ChangeAgressive;
				logMessage = string.Format(
					CultureInfo.InvariantCulture,
					IsAgressive ? language.StartFight : language.StopFight,
					this,
					WeaponToFight);
			}
			else
			{
				time = balance.ActionLongevity.Disabled;
				logMessage = string.Format(CultureInfo.InvariantCulture, language.ChangeFightModeDisabled, this);
			}
			return new ActionResult(Time.FromTicks(balance.Time, time), logMessage, newActivity);
		}

		public ActionResult ChangeWeapon(IWeapon weapon)
		{
			int time;
			string logMessage;
			var game = CurrentCell.Region.World.Game;
			var language = game.Language.LogActionFormats;
			var balance = game.Balance;

			var oldWeapon = WeaponToFight;
			if (oldWeapon != weapon)
			{
				WeaponToFight = weapon;

				RaiseWeaponChanged(oldWeapon, weapon);

				time = balance.ActionLongevity.ChangeWeapon;
				logMessage = string.Format(
					CultureInfo.InvariantCulture,
					language.ChangeWeapon,
					this,
					oldWeapon,
					weapon);
			}
			else
			{
				time = balance.ActionLongevity.Disabled;
				logMessage = string.Format(CultureInfo.InvariantCulture, language.ChangeWeaponDisabled, this);
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
			var world = CurrentCell.Region.World;
			var game = world.Game;
			var balance = game.Balance;
			var language = game.Language;
			var random = new Random(DateTime.Now.Millisecond);

			int hitPossibility = balance.Player.BaseHitPossibility;
			hitPossibility += ((int) Properties.Reaction - (int) target.Properties.Reaction) * 10;
			if (random.Next(0, 100) < hitPossibility)
			{
				target.Die(string.Format(CultureInfo.InvariantCulture, language.DeathReasons.Killed, this));
			}

			return new ActionResult(
				Time.FromTicks(balance.Time, (int)(balance.ActionLongevity.Attack)),
				string.Format(CultureInfo.InvariantCulture, language.LogActionFormats.Attack, this, target, WeaponToFight),
				Activity.Fights);
		}

		public virtual ActionResult Shoot(Cell target)
		{
			var world = CurrentCell.Region.World;
			var game = world.Game;
			var balance = game.Balance;
			var language = game.Language;
			var random = new Random(DateTime.Now.Millisecond);

			var missile = Inventory.OfType<Arrow>().First();

			if (!CurrentCell.Position.Equals(target.Position))
			{
				var direction = CurrentCell.Position.GetDirection(target.Position);
				var region = CurrentCell.Region;
				int z = CurrentCell.Position.Z;

				// take all obstacles into account
				var track = new List<Cell>();
				var step = CurrentCell;
				int dx = target.Position.X - CurrentCell.Position.X,
					dy = target.Position.Y - CurrentCell.Position.Y;
				double	distance = Math.Sqrt(dx * dx + dy * dy),
						sx = dx / distance,
						sy = dy / distance,
						x = CurrentCell.Position.X,
						y = CurrentCell.Position.Y;
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
					aim.Die(string.Format(CultureInfo.InvariantCulture, language.DeathReasons.Killed, this));
				}
			}

			// remove missile
			Inventory.Remove(missile);

			return new ActionResult(
				Time.FromTicks(balance.Time, (int)(balance.ActionLongevity.Shoot)),
				string.Format(CultureInfo.InvariantCulture, language.LogActionFormats.Shoot, this, target, WeaponToFight),
				Activity.Fights);
		}

		public virtual ActionResult DropItem(IItem item)
		{
			var world = CurrentCell.Region.World;
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

#warning Now we need to decide if selected weapon and manequin items must store within Inventory or not.

			return new ActionResult(
				Time.FromTicks(balance.Time, balance.ActionLongevity.DropItem),
				string.Format(CultureInfo.InvariantCulture, language.DropItem, this, item));
		}

		public sealed override ActionResult Do()
		{
			var result = DoImplementation();
			return new ActionResult(result.Longevity.Scale(Speed), result.LogMessages);
		}

		protected abstract ActionResult DoImplementation();

		private ActionResult EatDrink(
			IFood food,
			Func<ActionLongevityBalance, long> getLongevity,
			Func<LanguageLogActionFormats, string> getLogFormat)
		{
			var world = CurrentCell.Region.World;
			var game = world.Game;
			Balance balance = game.Balance;
			var language = game.Language;

			State.EatDrink(food, language);

			return new ActionResult(
				Time.FromTicks(balance.Time, getLongevity(balance.ActionLongevity)),
				string.Format(CultureInfo.InvariantCulture, getLogFormat(language.LogActionFormats), this, food.GetDescription(language.Items, this)));
		}

		public ActionResult Eat(IFood food)
		{
			return EatDrink(food, b => b.Eat, f => f.Eat);
		}

		public ActionResult Drink(IDrink drink)
		{
			return EatDrink(drink, b => b.Drink, f => f.Drink);
		}
	}
}
