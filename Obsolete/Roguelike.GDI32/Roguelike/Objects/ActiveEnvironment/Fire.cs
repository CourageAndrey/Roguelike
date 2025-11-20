using System;
using System.Collections.Generic;
using System.Drawing;

using Roguelike.Actions;
using Roguelike.Objects.Interfaces;
using Roguelike.Worlds;

namespace Roguelike.Objects.ActiveEnvironment
{
  /// <summary>
  /// Объект: костёр.
  /// </summary>
  public class Fire : WorldObject, IActiveObject
  {
    #region Overrides of WorldObject

    /// <summary>
    /// Получение строкового описания.
    /// </summary>
    /// <param name="forActor">для персонажа</param>
    /// <returns>краткое строковое описание для подсказок карты</returns>
		public override string GetDescription(Actor forActor)
    {
      return "костёр";
    }

    /// <summary>
    /// Прорисовка.
    /// </summary>
    /// <param name="canvas">поверхность рисования</param>
    /// <param name="bounds">границы</param>
    public override void Draw(Graphics canvas, Rectangle bounds)
    {
      canvas.DrawString(Symbols.Fire, Balance.DefaultFont, currentBrush, bounds);
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
    public Fire()
    {
      refresh();
    }

    /// <summary>
    /// Обновление внешнего вида.
    /// </summary>
    private void refresh()
    {
      var r = World.Instance.God;
      currentBrush = new SolidBrush(Color.FromArgb(r.Next(150, 255), r.Next(0, 100), 0));
    }

    /// <summary>
    /// Текущая кисть прорисовки.
    /// </summary>
    private Brush currentBrush;

    /// <summary>
    /// Порядок отрисовки объектов.
    /// </summary>
    public override HeightDisplayOrder HeightOrder
    {
      get
      {
        return HeightDisplayOrder.Medium;
      }
    }
  }
}
