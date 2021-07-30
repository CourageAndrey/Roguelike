using System.Drawing;

using Roguelike.Worlds;

namespace Roguelike.Objects.Base
{
  /// <summary>
  /// Объект мира.
  /// </summary>
  public abstract class WorldObject : IWorldObject
  {
    #region Свойства

    /// <summary>
    /// Положение.
    /// </summary>
    public Vector Position
    { get; protected set; }

    /// <summary>
    /// "Прозрачность".
    /// </summary>
    /// <remarks>определяет, можно ли пройти сквозь объект</remarks>
    public virtual bool Transparent
    { get { return false; } }

    /// <summary>
    /// Порядок отрисовки объектов.
    /// </summary>
    /// <remarks>
    /// 0 - для фоновых объектов
    /// 1 - для низких объектов (например, трава)
    /// 2 - для нормальных объектов
    /// 3 - для высоких объектов (например, птица)
    /// </remarks>
    public virtual int TabPos
    { get { return 2; } }

    /// <summary>
    /// Описание.
    /// </summary>
    public abstract string Description
    { get; }

    #endregion

    /// <summary>
    /// Прорисовка.
    /// </summary>
    /// <param name="canvas">поверхность рисования</param>
    /// <param name="bounds">границы</param>
    public abstract void Draw(Graphics canvas, Rectangle bounds);

    /// <summary>
    /// Вычисление мировых координат.
    /// </summary>
    /// <param name="camera">положение камеры</param>
    /// <param name="viewport">размер окна просмотра</param>
    /// <param name="coords">координаты объекта</param>
    /// <returns>точка, в которой видится объект</returns>
    public static Vector GetWorldCoordinates(Vector camera, Rectangle viewport, Point coords)
    {
      return new Vector(
        (+coords.X - (Balance.CellSize.Width >> 1) - (viewport.Width >> 1)) / Balance.CellSize.Width + camera.X,
        (-coords.Y + (Balance.CellSize.Height >> 1) + (viewport.Height >> 1)) / Balance.CellSize.Height + camera.Y);
    }

    /// <summary>
    /// ctor.
    /// </summary>
    protected WorldObject()
    {
      Position = new Vector();
    }

    #region Движение

    /// <summary>
    /// Сдвиг в сторону.
    /// </summary>
    /// <param name="direction">направление</param>
    /// <param name="longevity">расстояние сдвига</param>
    public void Strafe(Direction direction, int longevity = 1)
    {
      var newPos = Position.Strafe(direction, longevity);
      if (isFreeCell(newPos))
        Teleport(newPos);
    }

    /// <summary>
    /// Перемещение в точку.
    /// </summary>
    /// <param name="x">x</param>
    /// <param name="y">y</param>
    /// <remarks>не проверяет, свободня ли эта точка</remarks>
    internal void Teleport(int x, int y)
    {
      Teleport(new Vector(x, y));
    }

    /// <summary>
    /// Проверка, свободная ли ячейка.
    /// </summary>
    /// <param name="v">точка</param>
    /// <returns>true, если в точке нет непрозрачных объектов</returns>
    private static bool isFreeCell(Vector v)
    {
      var objects = World.Instance.GetObjectsInPos(v);
      return (objects.Find(o => !o.Transparent) == null);
    }

    /// <summary>
    /// Смена положения в пространстве.
    /// </summary>
    /// <param name="v">новое положени.</param>
    public void Teleport(Vector v)
    {
      var old = Position;
      Position = v;
      World.Instance.Recache(this, old);
    }

    #endregion
  }
}
