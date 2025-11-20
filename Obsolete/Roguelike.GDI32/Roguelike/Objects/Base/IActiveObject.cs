using System;
using System.Collections.Generic;

namespace Roguelike.Objects.Base
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
    IList<string> Do();

    /// <summary>
    /// Время следующего действия.
    /// </summary>
    DateTime NextActionTime
    { get; set; }
  }
}
