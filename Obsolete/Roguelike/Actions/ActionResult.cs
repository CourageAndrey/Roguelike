using System.Collections.Generic;

using Roguelike.Worlds;

namespace Roguelike.Actions
{
  /// <summary>
  /// Итог выполнения действия.
  /// </summary>
  public class ActionResult
  {
    #region Свойства

    /// <summary>
    /// Затраченное время, секунд.
    /// </summary>
    public double Longevity
    { get; private set; }

    /// <summary>
    /// Сообщения для журнала.
    /// </summary>
    public IList<string> LogMessages
    { get; private set; }

    #endregion

    #region Конструкторы

    /// <summary>
    /// ctor.
    /// </summary>
    /// <param name="longevity">затраченное время, с</param>
    /// <param name="message">сообщение журнала</param>
    public ActionResult(double longevity, string message)
      : this(longevity, new[] { message })
    { }

    /// <summary>
    /// ctor.
    /// </summary>
    /// <param name="longevity">затраченное время, с</param>
    /// <param name="messages">сообщения журнала</param>
    public ActionResult(double longevity, IEnumerable<string> messages)
    {
      Longevity = longevity;
      LogMessages = new List<string>(messages);
    }

    #endregion

    /// <summary>
    /// Пустое действие.
    /// </summary>
    public static readonly ActionResult Empty = new ActionResult(Balance.ActionLongevityNull, string.Empty);
  }
}
