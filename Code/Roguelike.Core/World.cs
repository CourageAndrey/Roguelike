using System;
using System.Collections.Generic;
using System.Linq;

using Roguelike.Core.ActiveObjects;
using Roguelike.Core.Items;
using Roguelike.Core.StaticObjects;

namespace Roguelike.Core
{
	public class World
	{
		#region Properties

		public Game Game
		{ get; }

		public Time Time
		{ get { return time; } }

		public IReadOnlyCollection<Region> Regions
		{ get; }

		public Hero Hero
		{ get; }

		private Time time;

		#endregion

		public World(Game game)
		{
			Game = game;
			var balance = game.Balance;

			var seed = Randomize();

			time = new Time(
				balance,
				balance.BeginYear,
				(byte) seed.Next(0, balance.MonthInYear),
				(byte) seed.Next(0, balance.WeeksInMonth),
				(byte) seed.Next(0, balance.DaysInWeek),
				(uint) seed.Next(0, (int) balance.TicksInDay));

			Regions = this.GenerateRegions(balance.DefaultRegionsCount);
			var region = Regions.First();

			Hero = new Hero(true, Time.FromYears(balance, 25), new Properties(), new Inventory(), "Andor Drakon");
			var heroCell = region.GetCell(
				seed.Next(10, balance.DefaultRegionXdimension - 50),
				seed.Next(10, balance.DefaultRegionYdimension - 50),
				0);
			Hero.MoveTo(heroCell);
			Hero.Inventory.TryAdd(new Hatchet());
			Hero.MakeMapKnown(balance.HeroInitialViewDistance);

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

		public void ApplyAction(ActiveObject actor, ActionResult actionResult)
		{
			foreach (string line in actionResult.LogMessages)
			{
				Game.WriteLog(line);
			}
			actor.NextActionTime = actor.NextActionTime + actionResult.Longevity;
			if ((actor.NextActionTime != null) && (actor.NextActionTime > time))
			{
				time = actor.NextActionTime;
			}
		}

		#endregion
	}
}
