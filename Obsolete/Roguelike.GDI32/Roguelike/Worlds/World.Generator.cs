using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;

using Roguelike.Items;
using Roguelike.Objects.ActiveCharacters;
using Roguelike.Objects.ActiveEnvironment;
using Roguelike.Objects.Interfaces;
using Roguelike.Objects.StaticEnvironment;
using Roguelike.Worlds.Elementals;

using Sef.Common;

namespace Roguelike.Worlds
{
  partial class World
  {
    /// <summary>
    /// ctor.
    /// </summary>
    /// <param name="settings">стартовые настройки</param>
    public World(StartSettings settings)
    {
      // Singleton
      Instance = this;

      // инициализация полей
			bounds = new Rectangle(0, 0, Balance.WorldSize.Width, Balance.WorldSize.Height);
      cells = new Cell[Size.Width, Size.Height];
      God = new Random(DateTime.Now.Millisecond);
      
      // инициализация даты
      TimeStamp = new DateTime(
        God.Next(Balance.StartYearMin, Balance.StartYearMax),
        God.Next(1, 12),
        God.Next(1, 28),
        God.Next(9, 15),
        God.Next(0, 60),
        God.Next(0, 60));
      
      #region Генерация главной карты

      // инициализация изображения, на котором будет вестись прорисовка
      var bitmap = new Bitmap(Size.Width, Size.Height);
      var canvas = Graphics.FromImage(bitmap);
      
      // заливка всей карты водой
      canvas.FillRectangle(CellBackground.Water.Brush, 0, 0, bitmap.Width, bitmap.Height);

      // проведение пляжей
      int cx = bitmap.Width / 2, cy = bitmap.Height / 2;
// ReSharper disable once PossibleLossOfFraction
      double radius = (Math.Min(bitmap.Width, bitmap.Height) / 2) * 0.9 - 2;
      drawRound(canvas, radius, CellBackground.Sand, cx, cy, God);

      // проведение полосы земли
      radius *= 0.9;
      drawRound(canvas, radius, CellBackground.Ground, cx, cy, God);

      // проведение полосы травы
      radius *= 0.9;
      drawRound(canvas, radius, CellBackground.Grass, cx, cy, God);

      // проведение второй полосы земли
      radius *= 0.35;
      drawRound(canvas, radius, CellBackground.Ground, cx, cy, God);

      // проведение полосы камня
      radius *= 0.85;
      drawRound(canvas, radius, CellBackground.Rock, cx, cy, God);

      // проведение полосы снега
      radius *= 0.55;
      drawRound(canvas, radius, CellBackground.Snow, cx, cy, God);

      // заливка центра лавой
      radius *= 0.65;
      drawRound(canvas, radius, CellBackground.Lava, cx, cy, God);

      // создание ячеек самой карты по полученному изображению
      for (int x = 0; x < Size.Width; x++)
        for (int y = 0; y < Size.Height; y++)
        {
          var color = bitmap.GetPixel(x, y);
          cells[x, y] = new Cell(new Point(x, y), CellBackground.All.Find(b => (b.Brush is SolidBrush) && equalColors((b.Brush as SolidBrush).Color, color)));
        }

      #endregion

      #region Проведение рек

      var riverPos = new Point(Size.Width / 2, Size.Height / 2);
      River lastRiver = null;
			while (GetCell(riverPos).Background != CellBackground.Water)
			{
				if (GetCell(riverPos).Background != CellBackground.Lava && GetCell(riverPos).Background != CellBackground.Snow)
				{ // если можно добавить реку - добавляем
					var river = new River();
					AddNewObject(river, riverPos);
					cells[riverPos.X, riverPos.Y].SetBackground(CellBackground.Water);
					bitmap.SetPixel(riverPos.X, riverPos.Y, Color.Navy);

					if (lastRiver != null)
					{
						lastRiver.SetDirection(riverPos);
					}
					lastRiver = river;
				}
				else if (lastRiver != null)
				{ // если ушли назад в снег/лаву - откатываемся до предыдущей клетки
					riverPos = lastRiver.Position;
				}

				// высчитываем координаты следующей клетки реки
				int newX, newY;
				do
				{
					newX = God.Next(-1, 1);
					newY = God.Next(-1, 1);
				} while (newX == 0 && newY == 0);
				riverPos = new Point(riverPos.X + newX, riverPos.Y + newY);
      }

#if DEBUG
      // сохранение карты в файл на диске для просмотра при отладке
      bitmap.Save(RoguelikeProgram.GetResourceFile("map.bmp"));
#endif

      #endregion

      #region Создание объектов

      // строим "однокомнатные дома" из стен
      var rooms = new List<Rectangle>();
			for (int i = 0; i < Balance.RandomRoomCount; i++)
      {
        Rectangle roomBounds;
	      do
	      {
					roomBounds = new Rectangle(
            God.Next(75, Size.Width - 150),
            God.Next(75, Size.Height - 150),
						God.Next(Balance.MinRoomSize, Balance.MaxRoomSize),
						God.Next(Balance.MinRoomSize, Balance.MaxRoomSize));

		      bool dangerous = false;
		      for (int x = roomBounds.Left; x <= roomBounds.Right; x++)
		      {
			      for (int y = roomBounds.Top; y <= roomBounds.Bottom; y++)
			      {
				      if (cells[x, y].Background.IsDangerous)
				      {
								dangerous = true;
					      break;
				      }
			      }
			      if (dangerous) break;
		      }
		      if (rooms.FindAll(r => r.IntersectsWith(roomBounds)).Count == 0 && // вновь созданная комната не пересекается с уже существующими
						  !dangerous) // комната не лежит в реке или вулкане
						break;
	      }
        while (true);

        rooms.Add(roomBounds);
				createRoom(roomBounds, Vector.AroundDirections4.GetRandom());
      }

      // садим растения
      for (int x = 0; x < Size.Width; x++)
        for (int y = 0; y < Size.Height; y++)
          if (cells[x, y].Background.IsSoil)
						if (God.Next(100) < Balance.HerbSeed)
              AddNewObject(new Herb(HerbKind.All[God.Next(HerbKind.All.Count)]), new Point(x, y));

      // зажигаем костры
			for (int i = 0; i < Balance.FiresCount; i++)
      {
        int s = Math.Min(Size.Width, Size.Height) / 5;
        int x = God.Next(s, 4 * s);
        int y = God.Next(s, 4 * s);
        AddNewObject(new Fire(), new Point(x, y));
      }
      
      #endregion

      #region Создание героя

      Hero = new Hero(settings);
      var startPosition = new Point();
      while (
        !GetCell(startPosition).Background.IsStartPoint ||
        (GetCell(startPosition).Objects.Count > 1))
        startPosition = new Point(
          God.Next(150, Size.Width - 300),
          God.Next(150, Size.Height - 300));
      AddNewObject(Hero, startPosition);

      #endregion

      #region Добавление стихий

      elementals = new List<IActiveObject> { new WaterElemental(), new FireElemental() };

      #endregion

      #region Создание персонажей

			for (int i = 0; i < Balance.ActorsCount; i++)
      {
        var npc = new Npc();
        AddNewObject(npc, new Point(God.Next(Size.Width), God.Next(Size.Height)));

        // добавление им оружия
				if (God.Next(100) < Balance.ActorChanceWeapon)
        {
          var weapon = Weapon.All[God.Next(Weapon.All.Count)].ApplyRandomModifier();
          npc.EquippedWeapons.Add(weapon);
        }

        // добавление им брони
        // добавляется всем и сразу одевается!
        var armor = Wear.All[God.Next(Wear.All.Count)].ApplyRandomModifier();
				npc.WearItem(armor);

        // добавление книг
				if (God.Next(100) < Balance.ActorChanceBook)
        {
					Bag bag;
					npc.Inventories.Add(bag = new Bag());
					bag.Add(Book.All.GetRandom(God));
        }
      }

      #endregion

      #region Удаление лишнего

			for (int x = 0; x < Size.Width; x++)
	    {
				for (int y = 0; y < Size.Height; y++)
				{
					var cell = cells[x, y];

					// всё, расположенное в вулканах и на воде, убивается апстену
					if (cell.Background == CellBackground.Lava)
						cell.Objects.Clear();
					if (cell.Background == CellBackground.Water)
					{
						var river = cell.Objects.OfType<River>().FirstOrDefault();
						cell.Objects.Clear();
						if (river != null)
						{
							cell.Objects.Add(river);
						}
					}

					// трава не может расти в костре
					var fires = cell.Objects.OfType<Fire>().ToList();
					if (fires.Count > 0)
					{
						cell.Objects.Clear();
						cell.Objects.Add(fires[0]);
					}
				}
	    }

      #endregion

      // формирование пола в комнатах
      foreach (var room in rooms)
			{
				for (int x = room.Left; x < room.Right; x++)
				{
					for (int y = room.Top; y < room.Bottom; y++)
	        {
		        cells[x, y].SetBackground(CellBackground.Floor);
						// удаление лишних объектов в комнатах
						foreach (var o in cells[x, y].Objects.ToList())
		        {
			        if (o is Fire || o is Herb)
			        {
				        cells[x, y].Objects.Remove(o);
			        }
		        }
	        }
				}
			}

      // перемещение героя
      while (GetCell(Hero.Position).Objects.Count > 1)
			{
				Teleport(Hero, new Point(
          Hero.Position.X + God.Next(-3, 3),
          Hero.Position.Y + God.Next(-3, 3)));
			}
    }

    private static void drawRound(Graphics canvas, double radius, CellBackground background, int centerX, int centerY, Random seed)
    {
      double angle = 0;
      var points = new List<Point>();
      double r = radius;
      while (angle < Math.PI * 2)
      {
        r = (r + radius * (1 + seed.Next(-5, 5) / 100.0)) / 2;
        points.Add(new Point(
          centerX + (int)(r * Math.Cos(angle)),
          centerY + (int)(r * Math.Sin(angle))));
        angle += (2 * Balance.WorldAngle * Math.PI);
      }
      canvas.FillPolygon(background.Brush, points.ToArray());
    }

    private void createRoom(Rectangle roomBounds, Direction doorDirection)
    {
      // защита от дурака
      if (roomBounds.Width < Balance.MinRoomSize || roomBounds.Height < Balance.MinRoomSize)
        throw new NotSupportedException("Это слишком маленькая комната! Минимум - 3х3.");

      // создание стен
      var walls = new List<Wall>();
      for (int x = 0; x < roomBounds.Width; x++)
        addWall(walls, roomBounds.X + x, roomBounds.Top);
      for (int x = 0; x < roomBounds.Width; x++)
        addWall(walls, roomBounds.X + x, roomBounds.Bottom-1);
      for (int y = 1; y < roomBounds.Height - 1; y++)
        addWall(walls, roomBounds.Left, roomBounds.Y + y);
      for (int y = 1; y < roomBounds.Height - 1; y++)
        addWall(walls, roomBounds.Right-1, roomBounds.Y + y);
      
      // дверь
      int doorX, doorY;
      switch (doorDirection)
      {
        case Direction.Up:
          doorX = (roomBounds.Left + roomBounds.Right)/2;
          doorY = roomBounds.Bottom-1;
          break;
        case Direction.Down:
          doorX = (roomBounds.Left + roomBounds.Right)/2;
          doorY = roomBounds.Top;
          break;
        case Direction.Left:
          doorX = roomBounds.Left;
          doorY = (roomBounds.Top + roomBounds.Bottom)/2;
          break;
        case Direction.Right:
          doorX = roomBounds.Right-1;
          doorY = (roomBounds.Top + roomBounds.Bottom)/2;
          break;
        default:
          throw new NotSupportedException("Only 2, 4, 6, 8 supported!");
      }
      var door = new Door();
      Teleport(door, new Point(doorX, doorY));
			cells[doorX, doorY].Objects.Remove(cells[doorX, doorY].Objects.OfType<Wall>().Single());
      walls.RemoveAll(w => (w.Position.X == doorX) && (w.Position.Y == doorY));
      door.Closed = God.Next(2) > 0;
    }

    private void addWall(ICollection<Wall> list, int x, int y)
    {
      var wall = new Wall();
      Teleport(wall, new Point(x, y));
      list.Add(wall);
    }

    /// <summary>
    /// Сравнение цветов без имён и прозрачности.
    /// </summary>
    /// <param name="a">цвет 1</param>
    /// <param name="b">цвет 2</param>
    /// <returns>true, если идентичны</returns>
    private static bool equalColors(Color a, Color b)
    {
      return (a.R == b.R) && (a.G == b.G) && (a.B == b.B);
    }
  }
}
