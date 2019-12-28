using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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

			var regions = new Region[balance.DefaultRegionsCount];
			for (int r = 0; r < balance.DefaultRegionsCount; r++)
			{
				regions[r] = new Region(this);
			}
			Regions = new ReadOnlyCollection<Region>(regions);

#warning Hero has to placed not just in first available region.
			(Hero = new Hero(true, Time.FromYears(balance, 25), new Properties(), new Inventory(), "Andor Drakon")).MoveTo(Regions.First().GetCell(0, 0, 0));
			Hero.Inventory.TryAdd(new Hatchet());
			for (int x = 0; x < 15; x++)
			{
				for (int y = 0; y < 15; y++)
				{
					Hero.MapMemory.Add(Hero.CurrentCell.Region.GetCell(x, y, 0));
				}
			}

			//------------------------------ test objects below
			var region = Regions.First();
			createRoom(region, 1, 5, 1, 5, 0, Direction.Up);
			new Pool().MoveTo(region.GetCell(6, 7, 0));
			new Tree().MoveTo(region.GetCell(7, 7, 0));
			new Fire().MoveTo(region.GetCell(2, 2, 0));
			new Bed().MoveTo(region.GetCell(4, 4, 0));
			var npc = new Npc(true, Time.FromYears(balance, 50), new Properties(), new Inventory(), "John Smith");
			npc.MoveTo(region.GetCell(2, 10, 0));
			new Npc(true, Time.FromYears(balance, 14), new Properties(), new Inventory(), "Jack Smiley").Die("just die").MoveTo(region.GetCell(0, 1, 0));
			new Animal(false, Time.FromYears(balance, 5), new Properties(), new Inventory()) { Owner = npc }.MoveTo(region.GetCell(3, 10, 0));
		}

		#region Generation routines

		private void createRoom(Region region, int x1, int x2, int y1, int y2, int z, Direction door)
		{
			if (Math.Abs(x1 - x2) < 3 || Math.Abs(y1 - y2) < 3) throw new Exception("Room is too small - 3x3 is minimal size.");

			int doorX, doorY;
			switch (door)
			{
				case Direction.Left:
					doorX = Math.Min(x1, x2);
					doorY = (y1 + y2) / 2;
					break;
				case Direction.Right:
					doorX = Math.Max(x1, x2);
					doorY = (y1 + y2) / 2;
					break;
				case Direction.Up:
					doorX = (x1 + x2) / 2;
					doorY = Math.Max(y1, y2);
					break;
				case Direction.Down:
					doorX = (y1 + y2) / 2;
					doorY = Math.Min(y1, y2);
					break;
				default:
					throw new Exception("Only up, down, left and right doors are available.");
			}

			for (int x = x1; x <= x2; x++)
			{
				new Wall().MoveTo(region.GetCell(x, y1, z));
				new Wall().MoveTo(region.GetCell(x, y2, z));
			}
			for (int y = y1; y <= y2; y++)
			{
				new Wall().MoveTo(region.GetCell(x1, y, z));
				new Wall().MoveTo(region.GetCell(x2, y, z));
			}

			for (int x = x1; x <= x2; x++)
			{
				for (int y = y1; y <= y2; y++)
				{
					region.GetCell(x, y, z).ChangeBackground(CellBackground.Floor);
				}
			}
			

			var doorCell = region.GetCell(doorX, doorY, z);
			doorCell.RemoveObject(doorCell.Objects.OfType<Wall>().First());
			new Door().MoveTo(doorCell);
		}

		#endregion

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
