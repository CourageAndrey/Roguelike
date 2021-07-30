using System.Collections.Generic;
using System.Drawing;

namespace Roguelike.Worlds
{
  /// <summary>
  /// Класс, расширяющий стандартную точку до вектора на плоскости.
  /// </summary>
  public static class Vector
  {
    /// <summary>
    /// Клонирование.
    /// </summary>
    /// <param name="point">точка на плоскости</param>
    /// <returns>копия точки</returns>
    public static Point Clone(this Point point)
    {
      return new Point(point.X, point.Y);
    }

    /// <summary>
    /// Определение расстояния до другого вектора.
    /// </summary>
    /// <param name="point">первая точка</param>
    /// <param name="other">вторая точка</param>
    /// <returns>расстояние</returns>
    /// <remarks>Учитывая ресурсоёмкость расчётов корня, используется квадрат этого расстояния.</remarks>
    public static int GetDistanceSquare(this Point point, Point other)
    {
      int x = point.X - other.X;
      int y = point.Y - other.Y;
      return x * x + y * y;
    }

    /// <summary>
    /// Сдвиг.
    /// </summary>
    /// <param name="point">точка на плоскости</param>
    /// <param name="direction">направление</param>
    /// <param name="longevity">расстояние сдвига</param>
    /// <returns>точка с новыми координатами</returns>
    public static Point Strafe(this Point point, Direction direction, int longevity = 1)
    {
      int newX = point.X, newY = point.Y;
      switch (direction)
      {
        case Direction.Up:
          newY += longevity;
          break;
        case Direction.Down:
          newY -= longevity;
          break;
        case Direction.Left:
          newX -= longevity;
          break;
        case Direction.Right:
          newX += longevity;
          break;
        case Direction.DownLeft:
          newX -= longevity;
          newY -= longevity;
          break;
        case Direction.DownRight:
          newX += longevity;
          newY -= longevity;
          break;
        case Direction.UpLeft:
          newX -= longevity;
          newY += longevity;
          break;
        case Direction.UpRight:
          newX += longevity;
          newY += longevity;
          break;
        case Direction.None:
        case Direction.Center:
          break;
      }

      return new Point(newX, newY);
    }

    /// <summary>
    /// Сдвиг.
    /// </summary>
    /// <param name="point">точка на плоскости</param>
    /// <param name="x">смещение по оси X</param>
    /// <param name="y">смещение по оси Y</param>
    /// <returns>точка с новыми координатами</returns>
    public static Point Strafe(this Point point, int x, int y)
    {
      return new Point(point.X + x, point.Y + y);
    }

    /// <summary>
    /// Получение направления на объект.
    /// </summary>
    /// <param name="point">первая точка</param>
    /// <param name="other">вторая точка</param>
    /// <returns>направление</returns>
    public static Direction GetDirection(this Point point, Point other)
    {
      if (point.Y > other.Y)
        return (point.X < other.X) ? Direction.DownRight : (point.X == other.X ? Direction.Down : Direction.DownLeft);
      if (point.Y == other.Y)
        return (point.X < other.X) ? Direction.Right : (point.X == other.X ? Direction.Center : Direction.Left);
      if (point.Y < other.Y)
        return (point.X < other.X) ? Direction.UpRight : (point.X == other.X ? Direction.Up : Direction.UpLeft);
      return Direction.None;
    }

    /// <summary>
    /// Доступные направления движения по сторонам.
    /// </summary>
    public static readonly List<Direction> AroundDirections8 = new List<Direction>
    {
      Direction.DownLeft,
      Direction.Down,
      Direction.DownRight,
      Direction.Left,
      Direction.Right,
      Direction.UpLeft,
      Direction.Up,
      Direction.UpRight
    };

    /// <summary>
    /// Доступные направления движения по сторонам.
    /// </summary>
    public static readonly List<Direction> AroundDirections4 = new List<Direction>
    {
      Direction.Down,
      Direction.Left,
      Direction.Right,
      Direction.Up,
    };
  }
}
