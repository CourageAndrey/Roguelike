using System;
using System.Collections.Generic;
using System.Drawing;

using Roguelike.Actions;
using Roguelike.Objects.Interfaces;
using Roguelike.Worlds;

namespace Roguelike.Objects.ActiveEnvironment
{
  /// <summary>
  /// Объект: река.
  /// </summary>
  public class River : WorldObject, IActiveObject
  {
    #region Overrides of WorldObject

    /// <summary>
    /// Получение строкового описания.
    /// </summary>
    /// <param name="forActor">для персонажа</param>
    /// <returns>краткое строковое описание для подсказок карты</returns>
		public override string GetDescription(Actor forActor)
    {
      return "река";
    }

    /// <summary>
    /// Прорисовка.
    /// </summary>
    /// <param name="canvas">поверхность рисования</param>
    /// <param name="bounds">границы</param>
    public override void Draw(Graphics canvas, Rectangle bounds)
    {
      canvas.DrawString(Symbols.River, Balance.DefaultFont, currentBrush, bounds);
    }

    /// <summary>
    /// "Прозрачность".
    /// </summary>
    /// <remarks>определяет, можно ли пройти сквозь объект</remarks>
    public override bool CanGoThrough
    { get { return true; } }

    #endregion

    #region Implementation of IActiveObject

    /// <summary>
    /// Выполнить действие.
    /// </summary>
    /// <returns>сообщение для лога</returns>
    public ActionResult Do()
    {
      // обновляем внешний вид
      refresh();

      return new ActionResult(Balance.ActionLongevityElemental, new List<string>());
    }

    /// <summary>
    /// Время следующего действия.
    /// </summary>
    public DateTime NextActionTime
    { get; set; }

    #endregion

    /// <summary>
    /// ctor.
    /// </summary>
    public River()
    {
      refresh();
    }

    /// <summary>
    /// Обновление внешнего вида.
    /// </summary>
    private void refresh()
    {
      var r = World.Instance.God;
	    int other = r.Next(0, 50);
			currentBrush = new SolidBrush(Color.FromArgb(other, other, r.Next(175, 255)));
    }

    /// <summary>
    /// Текущая кисть прорисовки.
    /// </summary>
    private Brush currentBrush;

    /// <summary>
    /// Направление.
    /// </summary>
    internal Point NextCell
    { get; private set; }

    /// <summary>
    /// Порядок отрисовки объектов.
    /// </summary>
    public override HeightDisplayOrder HeightOrder
    {
      get
      {
        return HeightDisplayOrder.Background;
      }
    }

    /// <summary>
    /// Установка направления.
    /// </summary>
    /// <param name="position">следующая ячейка</param>
    internal void SetDirection(Point position)
    {
      NextCell = position;
    }
  }
}
