using System.Drawing;

using Roguelike.Worlds;

namespace Roguelike.Objects.StaticEnvironment
{
  /// <summary>
  /// Объект: стена.
  /// </summary>
  public class Wall : WorldObject
  {
    /// <summary>
    /// Прорисовка.
    /// </summary>
    /// <param name="canvas">поверхность рисования</param>
    /// <param name="bounds">границы</param>
    public override void Draw(Graphics canvas, Rectangle bounds)
    {
      canvas.DrawString(Symbols.Wall, Balance.DefaultFont, Brushes.Silver, bounds);
    }

    /// <summary>
    /// Получение строкового описания.
    /// </summary>
    /// <param name="forActor">для персонажа</param>
    /// <returns>краткое строковое описание для подсказок карты</returns>
		public override string GetDescription(Actor forActor)
    {
      return "стена";
    }
  }
}
