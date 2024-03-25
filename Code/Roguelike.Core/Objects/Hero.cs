﻿using System;
using System.Drawing;

using Roguelike.Core.Aspects;
using Roguelike.Core.Configuration;
using Roguelike.Core.Interfaces;

namespace Roguelike.Core.Objects
{
	public class Hero : Humanoid, IHero
	{
		#region Properties

		public Camera Camera
		{ get { return this.GetAspect<Camera>(); } }

		#endregion

		public Hero(Balance balance, Time now, HeroStartSettings startSettings)
			: base(
				balance,
				startSettings.Race,
				startSettings.SexIsMale,
				now.AddYears(- (int) startSettings.Age).AddDays(1),
				startSettings.Name,
				startSettings.Profession,
				startSettings.HairColor,
				startSettings.Haircut)
		{
			AddAspects(new Camera(this, GetVisibleDistance));
		}

		private double GetVisibleDistance()
		{
			return Properties.Perception;
		}

		protected override ActionResult DoImplementation()
		{
			throw new NotSupportedException("Hero works outside this logic.");
		}

		public override Corpse Die(string reason)
		{
			var game = this.GetGame();
			game.Defeat();
			return base.Die(reason);
		}
	}

	public class HeroStartSettings
	{
		#region Properties

		public Race Race
		{ get; set; }

		public bool SexIsMale
		{ get; set; }

		public uint Age
		{ get; set; }

		public string Name
		{ get; set; }

		public Profession Profession
		{ get; set; }

		public Color HairColor
		{ get; set; }

		public Haircut Haircut
		{ get; set; }

		#endregion
	}
}
