﻿using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

using Roguelike.Core.ActiveObjects;
using Roguelike.Core.Configuration;
using Roguelike.Core.StaticObjects;

namespace Roguelike.Core
{
	public static class WorldGenerator
	{
		public static IReadOnlyCollection<Region> GenerateRegions(this World world, int count)
		{
			var regions = new Region[count];
			for (int r = 0; r < count; r++)
			{
				regions[r] = new Region(world);
			}
			return new ReadOnlyCollection<Region>(regions);
		}

		public static void CreateRoom(this Region region, int x1, int x2, int y1, int y2, int z, Direction door)
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
					var removedObjects = new List<Object>(region.GetCell(x, y, z).Objects);
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

		public static void CreateVillage(this Region region, Balance balance, Random seed, int x1, int x2, int y1, int y2, int z)
		{
			if (Math.Abs(x1 - x2) < 10 || Math.Abs(y1 - y2) < 10) throw new Exception("Village is too small - 10x10 is minimal size.");

			const int minHouseDimension = 5;
			const int maxHouseDimension = 7;
			var possibleDoorDirections = new List<Direction>
			{
				Direction.Down,
				Direction.Up,
				Direction.Left,
				Direction.Right,
			};

			int totalHouses = 0;

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

			int houseY1 = y1 + 1;
			while (houseY1 + maxHouseDimension + 1 < y2)
			{
				int houseX1 = x1 + 1;
				while (houseX1 + maxHouseDimension + 1 < x2)
				{
					int finalX1 = seed.Next(houseX1 - 1, houseX1 + 1);
					int finalX2 = finalX1 + seed.Next(minHouseDimension, maxHouseDimension) - 1;
					int finalY1 = seed.Next(houseY1 - 1, houseY1 + 1);
					int finalY2 = finalY1 + seed.Next(minHouseDimension, maxHouseDimension) - 1;

					var doorDirection = possibleDoorDirections[seed.Next(0, possibleDoorDirections.Count - 1)];

					region.CreateRoom(finalX1, finalX2, finalY1, finalY2, z, doorDirection);
					totalHouses++;

					new Fire().placeIntoFreeCell(region, seed, finalX1 + 1, finalX2 - 1, finalY1 + 1, finalY2 - 1, z);
					new Bed().placeIntoFreeCell(region, seed, finalX1 + 1, finalX2 - 1, finalY1 + 1, finalY2 - 1, z);
					new Bed().placeIntoFreeCell(region, seed, finalX1 + 1, finalX2 - 1, finalY1 + 1, finalY2 - 1, z);

					houseX1 += maxHouseDimension + seed.Next(1, 2);
				}

				houseY1 += maxHouseDimension + seed.Next(1, 2);
			}

			for (int i = 0; i < totalHouses; i++)
			{
				var husband = new Npc(true, Time.FromYears(balance.Time, balance.Time.BeginYear).AddYears(-50), new Properties(), new Inventory(), "John Smith " + i);
				husband.placeIntoFreeCell(region, seed, x1, x2, y1, y2, z);

				var wife = new Npc(false, Time.FromYears(balance.Time, balance.Time.BeginYear).AddYears(-50), new Properties(), new Inventory(), "Mary Poppins " + i);
				wife.placeIntoFreeCell(region, seed, x1, x2, y1, y2, z);

				var pet = new Animal(false, Time.FromYears(balance.Time, balance.Time.BeginYear).AddYears(-5), new Properties(), new Inventory()) { Owner = husband };
				pet.placeIntoFreeCell(region, seed, x1, x2, y1, y2, z);
			}
		}

		private static void placeIntoFreeCell(this Object @object, Region region, Random seed, int x1, int x2, int y1, int y2, int z)
		{
			Cell cell;
			do
			{
				int x = seed.Next(x1, x2);
				int y = seed.Next(y1, y2);
				cell = region.GetCell(x, y, z);
			} while (cell.Objects.Count > 0 && cell.Objects.All(o => !o.IsSolid));
			@object.MoveTo(cell);
		}

		public static void MakeMapKnown(this Hero hero, int viewDistance)
		{
			var region = hero.CurrentCell.Region;
			for (int x = -viewDistance; x < viewDistance; x++)
			{
				for (int y = -viewDistance; y < viewDistance; y++)
				{
					var cell = region.GetCell(hero.CurrentCell.Position.X + x, hero.CurrentCell.Position.Y + y, 0);
					if (cell != null)
					{
						hero.MapMemory.Add(cell);
					}
				}
			}
		}
	}
}
