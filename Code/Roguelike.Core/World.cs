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
			Hero.MoveTo(region.GetCell(
				seed.Next(100, balance.DefaultRegionXdimension - 100),
				seed.Next(100, balance.DefaultRegionYdimension - 100),
				0));
			Hero.Inventory.TryAdd(new Hatchet());
			Hero.MakeMapKnown(balance.HeroInitialViewDistance);

			//------------------------------ test objects below
			region.CreateRoom(1, 5, 1, 5, 0, Direction.Up);
			new Pool().MoveTo(region.GetCell(6, 7, 0));
			new Tree().MoveTo(region.GetCell(7, 7, 0));
			new Fire().MoveTo(region.GetCell(2, 2, 0));
			new Bed().MoveTo(region.GetCell(4, 4, 0));
			var npc = new Npc(true, Time.FromYears(balance, 50), new Properties(), new Inventory(), "John Smith");
			npc.MoveTo(region.GetCell(2, 10, 0));
			new Npc(true, Time.FromYears(balance, 14), new Properties(), new Inventory(), "Jack Smiley").Die("just die").MoveTo(region.GetCell(0, 1, 0));
			new Animal(false, Time.FromYears(balance, 5), new Properties(), new Inventory()) { Owner = npc }.MoveTo(region.GetCell(3, 10, 0));
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
