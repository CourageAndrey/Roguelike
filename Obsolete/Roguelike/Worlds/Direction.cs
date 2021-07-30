using System;

namespace Roguelike.Worlds
{
  /// <summary>
  /// Направление движения.
  /// </summary>
  public enum Direction
  {
    /// <summary>
    /// Нет.
    /// </summary>
    None = 0,

    /// <summary>
    /// SW.
    /// </summary>
    DownLeft = 1,

    /// <summary>
    /// S.
    /// </summary>
    Down = 2,

    /// <summary>
    /// S.
    /// </summary>
    DownRight = 3,

    /// <summary>
    /// W.
    /// </summary>
    Left = 4,

    /// <summary>
    /// На месте.
    /// </summary>
    Center = 5,

    /// <summary>
    /// E.
    /// </summary>
    Right = 6,

    /// <summary>
    /// NW.
    /// </summary>
    UpLeft = 7,

    /// <summary>
    /// N.
    /// </summary>
    Up = 8,

    /// <summary>
    /// NE.
    /// </summary>
    UpRight = 9
  }

  /// <summary>
  /// Определение направления строкой.
  /// </summary>
  public static class DirectionHelper
  {
    /// <summary>
    /// Описание направления.
    /// </summary>
    /// <param name="direction">направление</param>
    /// <returns>направление строкой</returns>
    public static string GetDescription(Direction direction)
    {
      switch (direction)
      {
        case Direction.None:
          return "нигде";
        case Direction.DownLeft:
          return "на юго-запад";
        case Direction.Down:
          return "на юг";
        case Direction.DownRight:
          return "на юго-восток";
        case Direction.Left:
          return "на запад";
        case Direction.Center:
          return "на месте";
        case Direction.Right:
          return "на восток";
        case Direction.UpLeft:
          return "на северо-запад";
        case Direction.Up:
          return "на север";
        case Direction.UpRight:
          return "на северо-восток";
        default:
          throw new ArgumentOutOfRangeException("direction");
      }
    }

    /// <summary>
    /// Получение стоимотси движения.
    /// </summary>
    /// <param name="direction">направление</param>
    /// <returns>количество затраченных секунд при сдвиге на 1</returns>
    public static double GetMovementCost(Direction direction)
    {
      switch (direction)
      {
        case Direction.None:
        case Direction.Center:
          return 0.5;
        case Direction.Down:
        case Direction.Left:
        case Direction.Right:
        case Direction.Up:
          return 1;
        case Direction.DownLeft:
        case Direction.DownRight:
        case Direction.UpLeft:
        case Direction.UpRight:
          return 1.41;
        default:
          throw new NotSupportedException();
      }
    }
  }
}
