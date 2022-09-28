using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading;

using Roguelike.Core.Interfaces;
using Roguelike.Core.Items;
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

		public ICollection<Item> Inventory
		{ get; }

		public bool IsDead
		{ get; private set; }

		public string DeadReason
		{ get; private set; }

		public double Weight
		{ get; private set; }

		public IWeapon WeaponToFight
		{ get; private set; }

		public bool IsAgressive
		{ get; private set; }

		public double Toughness
		{ get { return Properties.Endurance; } }

		#endregion

		#region Events

		public event ValueChangedEventHandler<IRequireGravitation, double> WeightChanged;

		public event ValueChangedEventHandler<IAlive, bool> AgressiveChanged;

		public event ValueChangedEventHandler<IAlive, IWeapon> WeaponChanged;

		public event EventHandler<IAlive, string> OnDeath;

		protected void RaiseWeightChanged(double oldWeight, double newWeight)
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
				deathMessage = string.Format(CultureInfo.InvariantCulture, language.LogActionFormatDeath, this, reason);
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

		protected Alive(bool sexIsMale, Time birthDate, IProperties properties, IEnumerable<Item> inventory)
		{
			if (properties == null) throw new ArgumentNullException(nameof(properties));
			if (inventory == null) throw new ArgumentNullException(nameof(inventory));

			SexIsMale = sexIsMale;
			BirthDate = birthDate;
			Properties = properties;
			WeaponToFight = new Unarmed(this);

			double getTotalWeigth()
			{
				return Toughness * Body.Weight + Inventory.Sum(item => item.Weight);
			};

			void updateWeight()
			{
				var weight = getTotalWeigth();
				RaiseWeightChanged(Weight, weight);
				Weight = weight;
			}

			Body = CreateBody();
			Body.WeightChanged += (sender, value, newValue) => updateWeight();

			void updateOnItemChange(IRequireGravitation item, double oldWeight, double newWeight)
			{
				updateWeight();
			}

			var _inventory = new EventCollection<Item>(inventory);
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

			Weight = getTotalWeigth();
		}

		public ActionResult ChangeAggressive(bool agressive)
		{
			int time;
			string logMessage;
			var game = CurrentCell.Region.World.Game;
			var language = game.Language;
			var balance = game.Balance;

			if (IsAgressive != agressive)
			{
				IsAgressive = agressive;

				RaiseAgressiveChanged(!agressive, agressive);

				if (agressive)
				{
					WeaponToFight.RaisePreparedForBattle(this);
				}
				else
				{
					WeaponToFight.RaiseStoppedBattle(this);
				}

				time = balance.ActionLongevity.ChangeAgressive;
				logMessage = string.Format(
					CultureInfo.InvariantCulture,
					IsAgressive ? language.LogActionFormatStartFight : language.LogActionFormatStopFight,
					this,
					WeaponToFight);
			}
			else
			{
				time = balance.ActionLongevity.Disabled;
				logMessage = string.Format(CultureInfo.InvariantCulture, language.LogActionFormatChangeFightModeDisabled, this);
			}
			return new ActionResult(Time.FromTicks(balance.Time, time), logMessage);
		}

		public ActionResult ChangeWeapon(IWeapon weapon)
		{
			int time;
			string logMessage;
			var game = CurrentCell.Region.World.Game;
			var language = game.Language;
			var balance = game.Balance;

			var oldWeapon = WeaponToFight;
			if (oldWeapon != weapon)
			{
				WeaponToFight = weapon;

				RaiseWeaponChanged(oldWeapon, weapon);

				time = balance.ActionLongevity.ChangeWeapon;
				logMessage = string.Format(
					CultureInfo.InvariantCulture,
					language.LogActionFormatChangeWeapon,
					this,
					oldWeapon,
					weapon);
			}
			else
			{
				time = balance.ActionLongevity.Disabled;
				logMessage = string.Format(CultureInfo.InvariantCulture, language.LogActionFormatChangeWeaponDisabled, this);
			}
			return new ActionResult(Time.FromTicks(balance.Time, time), logMessage);
		}

		public abstract Body CreateBody();

		public virtual List<Interaction> GetAvailableInteractions(Object actor)
		{
			var game = CurrentCell.Region.World.Game;
			var balance = game.Balance;
			var language = game.Language;

			return new List<Interaction>
			{
				new Interaction(language.InteractionBackstab, true, target =>
				{
					Die("backstabbed");
#warning Finish implementation, translate it and make not so easy.
					return ActionResult.GetEmpty(balance);
				})
			};
		}

		public virtual ActionResult Attack(IAlive target)
		{
			var world = CurrentCell.Region.World;
			var game = world.Game;
			var balance = game.Balance;
			var language = game.Language;
			var random = new Random(DateTime.Now.Millisecond);

			int hitPossibility = balance.BaseHitPossibility;
			hitPossibility += ((int) Properties.Reaction - (int) target.Properties.Reaction) * 10;
			if (random.Next(0, 100) < hitPossibility)
			{
				target.Die(string.Format(CultureInfo.InvariantCulture, language.ReathReasonKilled, this));
			}

			return new ActionResult(
				Time.FromTicks(balance.Time, (int)(balance.ActionLongevity.Attack)),
				string.Format(CultureInfo.InvariantCulture, language.LogActionFormatAttack, this, target, WeaponToFight));
		}
	}
}
