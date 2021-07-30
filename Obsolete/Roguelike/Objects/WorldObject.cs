using System.Drawing;

using Roguelike.Worlds;

namespace Roguelike.Objects
{
  /// <summary>
  /// Объект мира.
  /// </summary>
  public abstract class WorldObject
  {
    #region Свойства

    /// <summary>
    /// Положение.
    /// </summary>
    public Point Position
    { get; private set; }

    /// <summary>
    /// "Прозрачность".
    /// </summary>
    /// <remarks>определяет, можно ли пройти сквозь объект</remarks>
    public virtual bool CanGoThrough
    { get { return false; } }

    /// <summary>
    /// Порядок отрисовки объектов.
    /// </summary>
    public virtual HeightDisplayOrder HeightOrder
    { get { return HeightDisplayOrder.Normal; } }

    /// <summary>
    /// Получение строкового описания.
    /// </summary>
    /// <param name="forActor">для персонажа</param>
    /// <returns>краткое строковое описание для подсказок карты</returns>
    public abstract string GetDescription(Actor forActor);

		/// <summary>
    /// Описание.
    /// </summary>
    public string Description
    { get { return GetDescription(World.Instance.Hero); } }

    #endregion

    /// <summary>
    /// Прорисовка.
    /// </summary>
    /// <param name="canvas">поверхность рисования</param>
    /// <param name="bounds">границы</param>
    public abstract void Draw(Graphics canvas, Rectangle bounds);

    #region Движение

    /// <summary>
    /// Сдвиг в сторону.
    /// </summary>
    /// <param name="direction">направление</param>
    /// <param name="longevity">расстояние сдвига</param>
    public void Strafe(Direction direction, int longevity = 1)
    {
      var w = World.Instance;
      var newPos = Position.Strafe(direction, longevity);
			if (w.GetCell(newPos).CanGoThrough)
        w.Teleport(this, newPos);
    }

    /// <summary>
    /// Установка координат.
    /// </summary>
    /// <param name="v">новое положение</param>
    /// <remarks>Может вызываться только из обработчиков изменения списков объектов ячейки.</remarks>
    internal void SetPosition(Point v)
    {
      Position = v;
    }

    #endregion
  }
}
