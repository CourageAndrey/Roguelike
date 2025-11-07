using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading;

using Roguelike.Core.Interfaces;
using Roguelike.Core.Items;

namespace Roguelike.Core.Aspects
{
	public class Fighter : IAspect, IVariableMassy
	{
		#region Properties

		private readonly IAlive _holder;

		public IItem WeaponToFight
		{ get; private set; }

		public bool IsAggressive
		{ get; private set; }

		public decimal Weight
		{ get { return WeaponToFight.Weight; } }

		public event ValueChangedEventHandler<IAlive, bool>? AggressiveChanged;

		public event ValueChangedEventHandler<IAlive, IItem>? WeaponChanged;

		public event ValueChangedEventHandler<IMassy, decimal>? WeightChanged;

		#endregion

		public Fighter(IAlive holder)
		{
			_holder = holder;
			WeaponToFight = new Unarmed(_holder);
		}

		public ActionResult ChangeAggressive(bool aggressive)
		{
			int time;
			string logMessage;
			var game = _holder.GetGame();
			var language = game.Language.LogActionFormats;
			var balance = game.World.Balance;
			Activity? newActivity = null;

			if (IsAggressive != aggressive)
			{
				IsAggressive = aggressive;

				RaiseAggressiveChanged(!aggressive, aggressive);

				if (aggressive)
				{
					WeaponToFight.GetAspect<Weapon>().RaisePreparedForBattle(_holder);
					newActivity = Activity.Guards;
				}
				else
				{
					WeaponToFight.GetAspect<Weapon>().RaiseStoppedBattle(_holder);
					newActivity = Activity.Stands;
				}

				time = balance.ActionLongevity.ChangeAggressive;
				logMessage = string.Format(
					CultureInfo.InvariantCulture,
					IsAggressive ? language.StartFight : language.StopFight,
					_holder.GetDescription(game.Language, game.Hero),
					WeaponToFight);
			}
			else
			{
				time = balance.ActionLongevity.Disabled;
				logMessage = string.Format(CultureInfo.InvariantCulture, language.ChangeFightModeDisabled, _holder.GetDescription(game.Language, game.Hero));
			}
			return new ActionResult(Time.FromTicks(balance.Time, time), logMessage, newActivity);
		}

		public ActionResult ChangeWeapon(IItem weapon)
		{
			int time;
			string logMessage;
			var game = _holder.GetGame();
			var language = game.Language.LogActionFormats;
			var balance = game.World.Balance;

			var oldWeapon = WeaponToFight;
			if (oldWeapon != weapon)
			{
				WeaponToFight = weapon;

				if (!(oldWeapon is Unarmed))
				{
					_holder.Inventory.Items.Add(oldWeapon);
				}
				if (!(weapon is Unarmed))
				{
					_holder.Inventory.Items.Remove(weapon);
				}

				RaiseWeaponChanged(oldWeapon, weapon);

				time = balance.ActionLongevity.ChangeWeapon;
				logMessage = string.Format(
					CultureInfo.InvariantCulture,
					language.ChangeWeapon,
					_holder.GetDescription(game.Language, game.Hero),
					oldWeapon,
					weapon);
			}
			else
			{
				time = balance.ActionLongevity.Disabled;
				logMessage = string.Format(CultureInfo.InvariantCulture, language.ChangeWeaponDisabled, _holder.GetDescription(game.Language, game.Hero));
			}

			return new ActionResult(Time.FromTicks(balance.Time, time), logMessage);
		}

		public ActionResult Backstab(IAlive target)
		{
			var game = _holder.GetGame();
			var language = game.Language;
			var balance = game.World.Balance;

			target.Die(language.DeathReasons.Backstabbed);
			return new ActionResult(
				Time.FromTicks(balance.Time, balance.ActionLongevity.Backstab),
				string.Format(
					CultureInfo.InvariantCulture,
					language.LogActionFormats.Backstab,
					target.GetDescription(language, game.Hero),
					_holder.GetDescription(language, game.Hero),
					WeaponToFight.GetDescription(language, game.Hero)));
		}

		public ActionResult? Attack(IAlive target)
		{
			if (WeaponToFight.GetAspect<Weapon>().IsRange) return null;

			var world = _holder.GetWorld();
			var game = world.Game;
			var balance = world.Balance;
			var language = game.Language;
			var random = new Random(DateTime.Now.Millisecond);

			int hitPossibility = balance.Player.BaseHitPossibility;
			hitPossibility += (_holder.Properties.Reaction - target.Properties.Reaction) * 10;
			if (random.Next(0, 100) < hitPossibility)
			{
				target.Die(string.Format(CultureInfo.InvariantCulture, language.DeathReasons.Killed, _holder.GetDescription(game.Language, game.Hero)));
			}

			return new ActionResult(
				Time.FromTicks(balance.Time, (int)(balance.ActionLongevity.Attack)),
				string.Format(CultureInfo.InvariantCulture, language.LogActionFormats.Attack, _holder.GetDescription(game.Language, game.Hero), target, WeaponToFight),
				Activity.Fights);
		}

		public ActionResult? Shoot(Cell target)
		{
			if (!WeaponToFight.GetAspect<Weapon>().IsRange) return null;

			var position = _holder.CurrentCell!.Position;
			var region = _holder.CurrentCell.Region;
			var world = region.World;
			var game = world.Game;
			var balance = world.Balance;
			var language = game.Language;
			var random = new Random(DateTime.Now.Millisecond);

			var missileType = WeaponToFight.GetAspect<RangeWeapon>().Type;
			var missile = _holder.Inventory.Items
				.Select<IItem, Missile>()
				.FirstOrDefault(m => m.GetAspect<Missile>().Type == missileType);
#warning Check missile for null value.

			if (!position.Equals(target.Position))
			{
				var direction = position.GetDirection(target.Position);

				// take all obstacles into account
				var track = new List<Cell>();
				int dx = target.Position.X - position.X,
					dy = target.Position.Y - position.Y;
				double	distance = Math.Sqrt(dx * dx + dy * dy),
						sx = dx / distance,
						sy = dy / distance,
						x = position.X,
						y = position.Y;
				while (distance > 1)
				{
					x += sx;
					y += sy;
					var cell = region.GetCell(
						(int) Math.Round(x, MidpointRounding.AwayFromZero),
						(int) Math.Round(y, MidpointRounding.AwayFromZero),
#warning Z is ignored here.
						position.Z);
					if (!track.Contains(cell))
					{
						track.Add(cell);
					}
					distance--;
				}
				track.Remove(target);

				if (track.Count > 0)
				{
					// animate missile fly
					game.UserInterface.AnimateShoot(direction, track, missile);
				}
			}

			// calculate damage
			var aim = target.Objects.OfType<IAlive>().FirstOrDefault();
			if (aim != null)
			{
				int hitPossibility = balance.Player.BaseHitPossibility;
				hitPossibility += (_holder.Properties.Perception - aim.Properties.Reaction) * 10;
				if (random.Next(0, 100) < hitPossibility)
				{
					aim.Die(string.Format(CultureInfo.InvariantCulture, language.DeathReasons.Killed, _holder.GetDescription(game.Language, game.Hero)));
				}
			}

			// remove missile
			_holder.Inventory.RemoveOneItem(missile);

			return new ActionResult(
				Time.FromTicks(balance.Time, balance.ActionLongevity.Shoot),
				string.Format(CultureInfo.InvariantCulture, language.LogActionFormats.Shoot, _holder.GetDescription(game.Language, game.Hero), target, WeaponToFight),
				Activity.Fights);
		}

		private void RaiseAggressiveChanged(bool oldAggressive, bool newAggressive)
		{
			var handler = Volatile.Read(ref AggressiveChanged);
			if (handler != null)
			{
				handler(_holder, oldAggressive, newAggressive);
			}
		}

		private void RaiseWeaponChanged(IItem oldWeapon, IItem newWeapon)
		{
			var weaponHandler = Volatile.Read(ref WeaponChanged);
			if (weaponHandler != null)
			{
				weaponHandler(_holder, oldWeapon, newWeapon);
			}

			var weightChanged = Volatile.Read(ref WeightChanged);
			if (weightChanged != null)
			{
				weightChanged(_holder, oldWeapon.Weight, newWeapon.Weight);
			}
		}
	}
}
