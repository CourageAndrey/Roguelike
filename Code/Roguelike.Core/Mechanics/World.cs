using System.Drawing;

using Roguelike.Core.Aspects;
using Roguelike.Core.Configuration;
using Roguelike.Core.Interfaces;
using Roguelike.Core.Items;
using Roguelike.Core.Localization;
using Roguelike.Core.Objects;

namespace Roguelike.Core.Mechanics
{
	public class World
	{
		#region Properties

		public Balance Balance
		{ get; }

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

		public World(Balance balance, Language language, HeroStartSettings heroStartSettings)
		{
			Balance = balance;

			var seed = Randomize();

			_time = new Time(
				balance.Time,
				balance.Time.BeginYear,
				(byte) seed.Next(0, balance.Time.MonthInYear),
				(byte) seed.Next(0, balance.Time.WeeksInMonth),
				(byte) seed.Next(0, balance.Time.DaysInWeek),
				(uint) seed.Next(0, (int) balance.Time.TicksInDay));

			Regions = this.GenerateRegions();
			var region = Regions.First(r => r.OriginationOf.Contains(heroStartSettings.Race));

			Hero = new Hero(region, balance, _time, heroStartSettings);
			var heroCell = region.GetCell(
				seed.Next(10, region.Size.X - 50),
				seed.Next(10, region.Size.Y - 50),
				0)!;
			Hero.MoveTo(heroCell);
			Hero.Inventory.Items.Add(ItemFactory.CreateHatchet());
			Hero.Inventory.Items.Add(ItemFactory.CreateBow());
			for (int i = 0; i < 3; i++)
			{
				Hero.Inventory.Items.Add(ItemFactory.CreateLoafOfBread());
				Hero.Inventory.Items.Add(ItemFactory.CreateBottleOFWater());
			}
			Hero.Mannequin.LowerBodyWear = ItemFactory.CreateTrousers(Color.Brown);
			Hero.Mannequin.UpperBodyWear = ItemFactory.CreateShirt(Color.LightGray);
			Hero.Mannequin.Jewelry.Add(ItemFactory.CreateRing());
			Hero.Inventory.Items.Add(new Pack(ItemFactory.CreateMissile(MissileType.Arrow), 20));
			Hero.Inventory.Items.Add(ItemFactory.CreateBook(Color.Coral, l => l.HelloWorld, l => l.HelloWorld));
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
			while (nextActor.Holder != Hero)
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

		public void ApplyAction(Active actor, ActionResult actionResult)
		{
			foreach (string line in actionResult.LogMessages)
			{
				Game.WriteLog(line);
			}

			var alive = actor.Holder as IAlive;
			if (alive != null && actionResult.NewActivity != null)
			{
				alive.State.SetActivity(actionResult.NewActivity);
			}

			var nextActionTime = actor.SetNextActionTime(actionResult.Longevity);
			if (nextActionTime > _time)
			{
				var previousDayPart = _time.DayPart;
				_time = nextActionTime;
				
				// Update weather if it's time for a change
				var currentRegion = Hero.GetRegion();
				if (_time >= currentRegion.Weather.NextChangeTime)
				{
					currentRegion.Weather.Change();
				}
				
				if (_time.DayPart != previousDayPart)
				{
					Hero.Camera.RefreshVisibleCells();
				}
			}
		}

		#endregion
	}
}
