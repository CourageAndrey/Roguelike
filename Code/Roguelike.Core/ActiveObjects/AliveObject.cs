﻿using System;
using System.Collections.Generic;
using System.Globalization;

using Roguelike.Core.Interfaces;
using Roguelike.Core.Items;
using Roguelike.Core.StaticObjects;

namespace Roguelike.Core.ActiveObjects
{
	public abstract class AliveObject : ActiveObject, IRequireGravitation, IInteractive, IAlive
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

		public IInventory Inventory
		{ get; }

		public bool IsDead
		{ get; private set; }

		public string DeadReason
		{ get; private set; }

		public double Weight
		{ get { return Body.Weight + Inventory.GetItemsWeight(); } }

		public IWeapon WeaponToFight
		{ get; private set; }

		public bool IsAgressive
		{ get; private set; }

		public double Toughness
#warning This setting has to be adjustable
		{ get { return 1; } }

		#endregion

		protected AliveObject(bool sexIsMale, Time birthDate, IProperties properties, IInventory inventory)
		{
			if (properties == null) throw new ArgumentNullException(nameof(properties));
			if (inventory == null) throw new ArgumentNullException(nameof(inventory));

			SexIsMale = sexIsMale;
			BirthDate = birthDate;
			Properties = properties;
			Body = CreateBody();
			Inventory = inventory;
			WeaponToFight = new Unarmed(this);
		}

		public virtual Corpse Die(string reason)
		{
			IsDead = true;
			DeadReason = reason;
			var corpse = new Corpse(this);
			if (CurrentCell != null)
			{
				CurrentCell.AddObject(corpse);
				CurrentCell.RemoveObject(this);
			}
			return corpse;
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
				time = balance.ActionLongevity.ChangeWeapon;
				logMessage = string.Format(
					CultureInfo.InvariantCulture,
					IsAgressive ? language.LogActionFormatStartFight : language.LogActionFormatStopFight,
					this,
					WeaponToFight);
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

			((AliveObject) target).Die($"killed by {this}");
#warning Translate message.

			return new ActionResult(
				Time.FromTicks(balance.Time, (int)(balance.ActionLongevity.Attack)),
				string.Format(CultureInfo.InvariantCulture, language.LogActionFormatAttack, this, target, WeaponToFight));
		}
	}
}
