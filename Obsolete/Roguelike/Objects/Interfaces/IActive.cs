using System;
using Roguelike.Actions;

namespace Roguelike.Objects.Interfaces
{
  /// <summary>
  /// Активный объект.
  /// </summary>
  public interface IActive
  {
    /// <summary>
    /// Выполнить действие.
    /// </summary>
    /// <returns>результат действия</returns>
    ActionResult Do();

    /// <summary>
    /// Время следующего действия.
    /// </summary>
    DateTime NextActionTime
    { get; set; }
  }
}
