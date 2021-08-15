using System;
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

		public static void CreateVillage(this Region region, Balance balance, int x1, int x2, int y1, int y2, int z)
		{
			if (Math.Abs(x1 - x2) < 10 || Math.Abs(y1 - y2) < 10) throw new Exception("Village is too small - 10x10 is minimal size.");

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
