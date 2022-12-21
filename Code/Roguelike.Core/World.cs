﻿using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;

using Roguelike.Core.Aspects;
using Roguelike.Core.Interfaces;
using Roguelike.Core.Items;
using Roguelike.Core.Localization;
using Roguelike.Core.Objects;

namespace Roguelike.Core
{
	public class World
	{
		#region Properties

		public Game Game
		{ get; internal set; }

		public Time Time
		{ get { return _time; } }

		public IReadOnlyCollection<Region> Regions
		{ get; }

		public IHero Hero
		{ get; }

		private Time _time;

		#endregion

		public World(Configuration.Balance balance, Language language)
		{
			var seed = Randomize();

			_time = new Time(
				balance.Time,
				balance.Time.BeginYear,
				(byte) seed.Next(0, balance.Time.MonthInYear),
				(byte) seed.Next(0, balance.Time.WeeksInMonth),
				(byte) seed.Next(0, balance.Time.DaysInWeek),
				(uint) seed.Next(0, (int) balance.Time.TicksInDay));

			Regions = this.GenerateRegions(balance.WorldSize);
			var region = Regions.First();

			Hero = new Hero(balance, Race.SinglePossible, true, _time.AddYears(-25), new Properties(10, 10, 30, 10, 10, 10), Enumerable.Empty<Item>(), "Andor Drakon");
			var heroCell = region.GetCell(
				seed.Next(10, balance.WorldSize.RegionXdimension - 50),
				seed.Next(10, balance.WorldSize.RegionYdimension - 50),
				0);
			Hero.MoveTo(heroCell);
			Hero.Inventory.Items.Add(ItemFactory.CreateHatchet());
			Hero.Inventory.Items.Add(ItemFactory.CreateBow());
			for (int i = 0; i < 3; i++)
			{
				Hero.Inventory.Items.Add(ItemFactory.CreateLoafOfBread());
				Hero.Inventory.Items.Add(ItemFactory.CreateBottleOFWater());
			}
			Hero.Manequin.LowerBodyWear = ItemFactory.CreateTrousers(Color.Brown);
			Hero.Manequin.UpperBodyWear = ItemFactory.CreateShirt(Color.LightGray);
			Hero.Manequin.Jewelry.Add(ItemFactory.CreateRing());
			foreach (var arrow in ItemFactory.CreateMissiles(MissileType.Arrow, 20))
			{
				Hero.Inventory.Items.Add(arrow);
			}
			Hero.Inventory.Items.Add(ItemFactory.CreateBook(Color.Coral, language => language.HelloWorld, language => language.HelloWorld));
			Hero.Camera.MakeMapKnown(balance.Distance.HeroInitialView);

			region.CreateVillage(
				balance,
				seed,
				language,
				heroCell.Position.X + 1,
				heroCell.Position.X + 30,
				heroCell.Position.Y + 1,
				heroCell.Position.Y + 30,
				heroCell.Position.Z);
		}

		public static Random Randomize()
		{
			return new Random(DateTime.Now.Millisecond);
		}

		#region Step performing

		public void DoOneStep()
		{
			var language = Game.Language;
			var currentRegion = Hero.GetRegion();
			var actors = currentRegion.GetActiveObjects();
			var nextActor = actors.Dequeue();
			while (nextActor != Hero)
			{
				var beforeActionTime = nextActor.NextActionTime;
				var performedAction = nextActor.Do();
				ApplyAction(nextActor, performedAction);
				var alive = nextActor as IAlive;
				if (alive != null && beforeActionTime != null)
				{
					alive.State.PassTime(nextActor.NextActionTime.Value - beforeActionTime.Value, language);
				}

				nextActor = actors.Dequeue();
			}
			currentRegion.ResetActiveCache();
		}

		public void ApplyAction(IActive actor, ActionResult actionResult)
		{
			foreach (string line in actionResult.LogMessages)
			{
				Game.WriteLog(line);
			}

			var alive = actor as IAlive;
			if (alive != null && actionResult.NewActivity != null)
			{
				alive.State.SetActivity(actionResult.NewActivity);
			}

#warning Wrong typecast.
			(actor as Active).NextActionTime += actionResult.Longevity;
			if ((actor.NextActionTime != null) && (actor.NextActionTime > _time))
			{
				_time = actor.NextActionTime.Value;
			}
		}

		#endregion
	}
}
