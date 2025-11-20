using System;

using Roguelike.Actions;

namespace Roguelike.Objects.Interfaces
{
  /// <summary>
  /// Активный объект.
  /// </summary>
  public interface IActiveObject
  {
    /// <summary>
    /// Выполнить действие.
    /// </summary>
    /// <returns>сообщение для лога</returns>
    ActionResult Do();

    /// <summary>
    /// Время следующего действия.
    /// </summary>
    DateTime NextActionTime
    { get; set; }
  }
}
