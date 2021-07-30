using System.Collections.Generic;

using Roguelike.Actions;

namespace Roguelike.Objects.Interfaces
{
  /// <summary>
  /// Объект, с которым можно взаимодействовать.
  /// </summary>
  public interface IInteractive
  {
		/// <summary>
		/// Описание.
		/// </summary>
		string Description
		{ get; }

    /// <summary>
    /// Взаимодействие.
    /// </summary>
    /// <param name="sender">субъект, вызвавший взаимодействие</param>
		/// <param name="interaction">выбранное взаимодействие</param>
    /// <returns>результат воздействия</returns>
    ActionResult Handle(Actor sender, int interaction);

	  /// <summary>
	  /// Все возможные взаимодействия.
	  /// </summary>
	  /// <returns>список</returns>
	  IList<Interaction> GetInteractions();
  }
}
