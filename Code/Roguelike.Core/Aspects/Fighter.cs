﻿using System;
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

		public bool IsAgressive
		{ get; private set; }

		public decimal Weight
		{ get { return WeaponToFight.Weight; } }

		public event ValueChangedEventHandler<IAlive, bool> AgressiveChanged;

		public event ValueChangedEventHandler<IAlive, IItem> WeaponChanged;

		public event ValueChangedEventHandler<IMassy, decimal> WeightChanged;

		#endregion

		public Fighter(IAlive holder)
		{
			_holder = holder;
			WeaponToFight = new Unarmed(_holder);
		}

		public ActionResult ChangeAggressive(bool agressive)
		{
			int time;
			string logMessage;
			var game = _holder.GetGame();
			var language = game.Language.LogActionFormats;
			var balance = game.Balance;
			Activity newActivity = null;

			if (IsAgressive != agressive)
			{
				IsAgressive = agressive;

				RaiseAgressiveChanged(!agressive, agressive);

				if (agressive)
				{
					WeaponToFight.GetAspect<Weapon>().RaisePreparedForBattle(_holder);
					newActivity = Activity.Guards;
				}
				else
				{
					WeaponToFight.GetAspect<Weapon>().RaiseStoppedBattle(_holder);
					newActivity = Activity.Stands;
				}

				time = balance.ActionLongevity.ChangeAgressive;
				logMessage = string.Format(
					CultureInfo.InvariantCulture,
					IsAgressive ? language.StartFight : language.StopFight,
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
			var balance = game.Balance;

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

#warning Revert method argument and this.
		public ActionResult Backstab(IAlive actor)
		{
			_holder.Die("backstabbed");
			return null;
#warning Finish implementation, translate it and make not so easy.
		}

		public ActionResult Attack(IAlive target)
		{
			var world = _holder.GetWorld();
			var game = world.Game;
			var balance = game.Balance;
			var language = game.Language;
			var random = new Random(DateTime.Now.Millisecond);

			int hitPossibility = balance.Player.BaseHitPossibility;
			hitPossibility += ((int)_holder.Properties.Reaction - (int) target.Properties.Reaction) * 10;
			if (random.Next(0, 100) < hitPossibility)
			{
				target.Die(string.Format(CultureInfo.InvariantCulture, language.DeathReasons.Killed, _holder.GetDescription(game.Language, game.Hero)));
			}

			return new ActionResult(
				Time.FromTicks(balance.Time, (int)(balance.ActionLongevity.Attack)),
				string.Format(CultureInfo.InvariantCulture, language.LogActionFormats.Attack, _holder.GetDescription(game.Language, game.Hero), target, WeaponToFight),
				Activity.Fights);
		}

#warning Rethink cache variables within method below.
		public ActionResult Shoot(Cell target)
		{
			var world = _holder.GetWorld();
			var game = world.Game;
			var balance = game.Balance;
			var language = game.Language;
			var random = new Random(DateTime.Now.Millisecond);

			var missile = _holder.Inventory.Items.Select<IItem, Missile>().First();

			if (!_holder.GetPosition().Equals(target.Position))
			{
				var direction = _holder.GetPosition().GetDirection(target.Position);
				var region = _holder.GetRegion();
				int z = _holder.GetPosition().Z;

				// take all obstacles into account
				var track = new List<Cell>();
				var step = _holder.CurrentCell;
				int dx = target.Position.X - _holder.GetPosition().X,
					dy = target.Position.Y - _holder.GetPosition().Y;
				double	distance = Math.Sqrt(dx * dx + dy * dy),
						sx = dx / distance,
						sy = dy / distance,
						x = _holder.GetPosition().X,
						y = _holder.GetPosition().Y;
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
				hitPossibility += ((int)_holder.Properties.Perception - (int) aim.Properties.Reaction) * 10;
				if (random.Next(0, 100) < hitPossibility)
				{
					aim.Die(string.Format(CultureInfo.InvariantCulture, language.DeathReasons.Killed, _holder.GetDescription(game.Language, game.Hero)));
				}
			}

			// remove missile
			_holder.Inventory.Items.Remove(missile);

			return new ActionResult(
				Time.FromTicks(balance.Time, (int)(balance.ActionLongevity.Shoot)),
				string.Format(CultureInfo.InvariantCulture, language.LogActionFormats.Shoot, _holder.GetDescription(game.Language, game.Hero), target, WeaponToFight),
				Activity.Fights);
		}

		private void RaiseAgressiveChanged(bool oldAgressive, bool newAgressive)
		{
			var handler = Volatile.Read(ref AgressiveChanged);
			if (handler != null)
			{
				handler(_holder, oldAgressive, newAgressive);
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