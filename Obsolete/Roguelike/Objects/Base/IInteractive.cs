using Roguelike.Actions;

namespace Roguelike.Objects.Base
{
  /// <summary>
  /// Объект, с которым можно взаимодействовать.
  /// </summary>
  public interface IInteractive : IWorldObject
  {
    /// <summary>
    /// Взаимодействие.
    /// </summary>
    /// <param name="sender">субъект, вызвавший взаимодействие</param>
    /// <returns>результат воздействия</returns>
    ActionResult Handle(Actor sender);

    /// <summary>
    /// Получение название взаимодействия.
    /// </summary>
    /// <returns>краткое строковое описание</returns>
    string GetActionDescription();
  }
}
