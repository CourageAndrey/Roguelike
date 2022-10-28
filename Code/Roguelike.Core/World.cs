using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;

using Roguelike.Core.ActiveObjects;
using Roguelike.Core.Interfaces;
using Roguelike.Core.Items;

namespace Roguelike.Core
{
	public class World
	{
		#region Properties

		public Game Game
		{ get; }

		public Time Time
		{ get { return _time; } }

		public IReadOnlyCollection<Region> Regions
		{ get; }

		public Hero Hero
		{ get; }

		private Time _time;

		#endregion

		public World(Game game)
		{
			Game = game;
			var balance = game.Balance;

			var seed = Randomize();

			_time = new Time(
				balance.Time,
				balance.Time.BeginYear,
				(byte) seed.Next(0, balance.Time.MonthInYear),
				(byte) seed.Next(0, balance.Time.WeeksInMonth),
				(byte) seed.Next(0, balance.Time.DaysInWeek),
				(uint) seed.Next(0, (int) balance.Time.TicksInDay));

			Regions = this.GenerateRegions(balance.WorldSize.RegionsCount);
			var region = Regions.First();

			Hero = new Hero(Race.SinglePossible, true, _time.AddYears(-25), new Properties(10, 10, 30, 10, 10, 10), Enumerable.Empty<Item>(), "Andor Drakon");
			var heroCell = region.GetCell(
				seed.Next(10, balance.WorldSize.RegionXdimension - 50),
				seed.Next(10, balance.WorldSize.RegionYdimension - 50),
				0);
			Hero.MoveTo(heroCell);
			Hero.Inventory.Add(new Hatchet());
			Hero.Inventory.Add(new Bow());
			Hero.Manequin.LowerBodyWear = new Trousers(Color.Brown);
			Hero.Manequin.UpperBodyWear = new Shirt(Color.LightGray);
			for (int i = 0; i < 20; i++)
			{
				Hero.Inventory.Add(new Arrow());
			}
			Hero.Inventory.Add(new Book(Color.Coral, language => language.HelloWorld, language => language.HelloWorld));
			Hero.MakeMapKnown(balance.Distance.HeroInitialView);

			region.CreateVillage(
				balance,
				seed,
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
			var currentRegion = Hero.CurrentCell.Region;
			var actors = currentRegion.GetActiveObjects();
			var nextActor = actors.Dequeue();
			while (nextActor != Hero)
			{
				ApplyAction(nextActor, nextActor.Do());
				nextActor = actors.Dequeue();
			}
			currentRegion.ResetActiveCache();
		}

		public void ApplyAction(Active actor, ActionResult actionResult)
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

			actor.NextActionTime += actionResult.Longevity;
			if ((actor.NextActionTime != null) && (actor.NextActionTime > _time))
			{
				_time = actor.NextActionTime.Value;
			}
		}

		#endregion
	}
}
