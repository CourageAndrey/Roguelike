using System.Drawing;

using Roguelike.Worlds;

namespace Roguelike.Objects.StaticEnvironment
{
  /// <summary>
  /// Объект: кровь.
  /// </summary>
  public class BloodPool : WorldObject
  {
    /// <summary>
    /// Прорисовка.
    /// </summary>
    /// <param name="canvas">поверхность рисования</param>
    /// <param name="bounds">границы</param>
    public override void Draw(Graphics canvas, Rectangle bounds)
    {
			canvas.DrawString(Symbols.Pool, Balance.DefaultFont, Brushes.DarkRed, bounds);
    }

    /// <summary>
    /// Получение строкового описания.
    /// </summary>
    /// <param name="forActor">для персонажа</param>
    /// <returns>краткое строковое описание для подсказок карты</returns>
		public override string GetDescription(Actor forActor)
    {
      return "лужа крови";
    }

    /// <summary>
    /// "Прозрачность".
    /// </summary>
    /// <remarks>определяет, можно ли пройти сквозь объект</remarks>
    public override bool CanGoThrough
    {
      get
      {
        return true;
      }
    }

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
  }
}
