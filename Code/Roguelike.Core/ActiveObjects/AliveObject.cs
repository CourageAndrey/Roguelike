﻿using System;
using System.Globalization;

using Roguelike.Core.Interfaces;
using Roguelike.Core.Items;
using Roguelike.Core.StaticObjects;

namespace Roguelike.Core.ActiveObjects
{
	public abstract class AliveObject : ActiveObject, IRequireGravitation
	{
		#region Properties

		public bool SexIsMale
		{ get; private set; }

		public Time Age
		{ get; private set; }

		public Properties Properties
		{ get; private set; }

		public Body Body
		{ get; }

		public IInventory Inventory
		{ get; }

		public bool IsDead
		{ get; private set; }

		public string DeadReason
		{ get; private set; }

		public double Weight
		{ get { return Body.Weight + Inventory.GetItemsWeight(); } }

		public Item WeaponToFight
		{ get; private set; }

		public bool IsAgressive
		{ get; private set; }

		public double Toughness
#warning This setting has to be adjustable
		{ get { return 1; } }

		#endregion

		protected AliveObject(bool sexIsMale, Time age, Properties properties, IInventory inventory)
		{
			if (age == null) throw new ArgumentNullException(nameof(age));
			if (properties == null) throw new ArgumentNullException(nameof(properties));
			if (inventory == null) throw new ArgumentNullException(nameof(inventory));

			SexIsMale = sexIsMale;
			Age = age;
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
				time = balance.ActionLongevityChangeWeapon;
				logMessage = string.Format(
					CultureInfo.InvariantCulture,
					IsAgressive ? language.LogActionFormatStartFight : language.LogActionFormatStopFight,
					this,
					WeaponToFight);
			}
			else
			{
				time = balance.ActionLongevityDisabled;
				logMessage = string.Format(CultureInfo.InvariantCulture, language.LogActionFormatChangeWeaponDisabled, this);
			}
			return new ActionResult(Time.FromTicks(balance, time), logMessage);
		}

		public abstract Body CreateBody();
	}
}
