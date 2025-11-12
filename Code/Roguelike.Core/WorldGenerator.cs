using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Drawing;
using System.Linq;

using Roguelike.Core.Aspects;
using Roguelike.Core.Configuration;
using Roguelike.Core.Interfaces;
using Roguelike.Core.Localization;
using Roguelike.Core.Objects;
using Roguelike.Core.Places;

namespace Roguelike.Core
{
	public static class WorldGenerator
	{
		public static IReadOnlyCollection<Region> GenerateRegions(this World world)
		{
			return new ReadOnlyCollection<Region>(new[]
			{
				new Region(world, new Vector(384, 128, 1), CellBackground.Snow) { OriginationOf = { Race.Nordman }},
				new Region(world, new Vector(128, 384, 1), CellBackground.Sand) { OriginationOf = { Race.Nomad }},
				new Region(world, new Vector(256, 128, 1), CellBackground.Rock) { OriginationOf = { Race.Highlander }},
				new Region(world, new Vector(256, 256, 1), CellBackground.Grass) { OriginationOf = { Race.PlainsMan }},
				new Region(world, new Vector(384, 128, 1), CellBackground.Swamp) { OriginationOf = { Race.Jungleman }},
			});
		}

		public static void CreateVillage(this Region region, Balance balance, Random seed, Language language, int x1, int x2, int y1, int y2, int z)
		{
			if (Math.Abs(x1 - x2) < 10 || Math.Abs(y1 - y2) < 10) throw new Exception("Village is too small - 10x10 is minimal size.");

			const int minHouseDimension = 5;
			const int maxHouseDimension = 7;

			int treesCount = (x2 - x1) * (y2 - y1) / 10;
			for (int i = 0; i < treesCount; i++)
			{
				new Tree().placeIntoFreeCell(region, seed, x1, x2, y1, y2, z);
			}

			int poolsCount = (x2 - x1) * (y2 - y1) / 30;
			for (int i = 0; i < poolsCount; i++)
			{
				new Pool().placeIntoFreeCell(region, seed, x1, x2, y1, y2, z);
			}

			var houses = new List<House>();
			int houseY1 = y1 + 1;
			while (houseY1 + maxHouseDimension + 1 < y2)
			{
				int houseX1 = x1 + 1;
				while (houseX1 + maxHouseDimension + 1 < x2)
				{
					houses.Add(region.CreateHouse(seed, minHouseDimension, maxHouseDimension, houseX1, houseY1, z));

					houseX1 += maxHouseDimension + seed.Next(1, 2);
				}

				houseY1 += maxHouseDimension + seed.Next(1, 2);
			}

#warning Make settlement names localizable
			region.Places.Add(new Settlement(houses, l => "Citytown village"));

			for (int i = 0; i < houses.Count; i++)
			{
				var race = region.OriginationOf.First();
				var profession = Profession.Everyman;
				string surname = profession.IsSurname
					? profession.GetName(language.Character.Professions)
					: race.Surnames[i % race.Surnames.Count];
				var husband = region.CreateFamily(balance, seed, race, profession, surname, x1, x2, y1, y2, z, houses[i]);

				region.CreateAnimals(balance, seed, husband, x1, x2, y1, y2, z);
			}
		}

		public static House CreateHouse(this Region region, Random seed, int minHouseDimension, int maxHouseDimension, int houseX1, int houseY1, int z)
		{
			int finalX1 = seed.Next(houseX1 - 1, houseX1 + 1);
			int finalX2 = finalX1 + seed.Next(minHouseDimension, maxHouseDimension) - 1;
			int finalY1 = seed.Next(houseY1 - 1, houseY1 + 1);
			int finalY2 = finalY1 + seed.Next(minHouseDimension, maxHouseDimension) - 1;

			var doorDirection = Directions.All4.GetRandom(seed);

			region.CreateWalls(seed, finalX1, finalX2, finalY1, finalY2, z, doorDirection);

			new Fire().placeIntoFreeCell(region, seed, finalX1 + 1, finalX2 - 1, finalY1 + 1, finalY2 - 1, z);
			new Bed().placeIntoFreeCell(region, seed, finalX1 + 1, finalX2 - 1, finalY1 + 1, finalY2 - 1, z);
			new Bed().placeIntoFreeCell(region, seed, finalX1 + 1, finalX2 - 1, finalY1 + 1, finalY2 - 1, z);

			var allHouseCells = new List<Cell>();
			for (int x = finalX1; x <= finalX2; x++)
			{
				for (int y = finalY1; y <= finalY2; y++)
				{
					allHouseCells.Add(region.GetCell(x, y, z));
				}
			}

			var house = new House(allHouseCells);
			region.Places.Add(house);
			return house;
		}

		public static void CreateWalls(this Region region, Random seed, int x1, int x2, int y1, int y2, int z, Direction doorSide)
		{
			if (Math.Abs(x1 - x2) < 3 || Math.Abs(y1 - y2) < 3) throw new Exception("Room is too small - 3x3 is minimal size.");

			int doorX, doorY;
			switch (doorSide)
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
					doorX = (x1 + x2) / 2;
					doorY = Math.Min(y1, y2);
					break;
				default:
					throw new Exception("Only up, down, left and right doors are available.");
			}

			for (int x = x1; x <= x2; x++)
			{
				for (int y = y1; y <= y2; y++)
				{
					var removedObjects = new List<Object>(region.GetCell(x, y, z)!.Objects);
					foreach (var o in removedObjects)
					{
						o.MoveTo(null);
					}
				}
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

			var weather = new Weather(region);
			var interior = new InteriorCellEnvironment(weather);
			for (int x = x1; x <= x2; x++)
			{
				for (int y = y1; y <= y2; y++)
				{
					var cell = region.GetCell(x, y, z)!;
					cell.ChangeBackground(CellBackground.Floor);
					cell.MakeInterior(interior);
				}
			}

			var doorCell = region.GetCell(doorX, doorY, z)!;
			doorCell.RemoveObject(doorCell.Objects.OfType<Wall>().First());
			var door = new Door();
			door.MoveTo(doorCell);
			if (seed.Next(0, 2) == 1)
			{
				door.Close();
			}
		}

		public static Npc CreateFamily(this Region region, Balance balance, Random seed, Race race, Profession profession, string surname, int x1, int x2, int y1, int y2, int z, House house)
		{
			var hairColors = race.HairColors.ToList();
			var settlement = region.Places.OfType<Settlement>().Single();

			var husband = new Npc(
				balance,
				race,
				true,
				Time.FromYears(balance.Time, balance.Time.BeginYear).AddYears(-50),
				surname,
				profession,
				hairColors[seed.Next(hairColors.Count)],
				Haircut.ShortHairs,
				settlement);
			husband.Race.DressCostume(husband);
			husband.placeIntoFreeCell(region, seed, x1, x2, y1, y2, z);

			var wife = new Npc(
				balance,
				race,
				false,
				Time.FromYears(balance.Time, balance.Time.BeginYear).AddYears(-50),
				surname,
				profession,
				hairColors[seed.Next(hairColors.Count)],
				Haircut.LongHairs,
				settlement);
			wife.Race.DressCostume(wife);
			wife.placeIntoFreeCell(region, seed, x1, x2, y1, y2, z);

			house.Settle(husband, wife);

			return husband;
		}

		public static void CreateAnimals(this Region region, Balance balance, Random seed, IObject owner, int x1, int x2, int y1, int y2, int z)
		{
			var pet = new Dog(balance, false, Time.FromYears(balance.Time, balance.Time.BeginYear).AddYears(-5), Color.Gray);
			pet.GetAspect<Ownership>().OwnBy(owner);
			pet.placeIntoFreeCell(region, seed, x1, x2, y1, y2, z);

			var transport = new Horse(balance, false, Time.FromYears(balance.Time, balance.Time.BeginYear).AddYears(-10), Color.LightSalmon);
			transport.GetAspect<Ownership>().OwnBy(owner);
			transport.placeIntoFreeCell(region, seed, x1, x2, y1, y2, z);
		}

		private static void placeIntoFreeCell(this Object @object, Region region, Random seed, int x1, int x2, int y1, int y2, int z)
		{
			var freeCells = new List<Cell>();
			for (int x = x1; x <= x2; x++)
			{
				for (int y = y1; y <= y2; y++)
				{
					var cell = region.GetCell(x, y, z)!;
					if (cell.IsTransparent)
					{
						freeCells.Add(cell);
					}
				}
			}

			@object.MoveTo(freeCells[seed.Next(0, freeCells.Count - 1)]);
		}
	}
}
