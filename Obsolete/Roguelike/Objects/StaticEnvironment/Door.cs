using System;
using System.Collections.Generic;
using System.Drawing;

using Roguelike.Actions;
using Roguelike.Objects.Interfaces;
using Roguelike.Worlds;

namespace Roguelike.Objects.StaticEnvironment
{
  /// <summary>
  /// Объект: дверь.
  /// </summary>
  public class Door : WorldObject, IInteractive
  {
    /// <summary>
    /// Состояние: закрыта/открыта.
    /// </summary>
    public bool Closed
    { get; set; }

    /// <summary>
    /// Прорисовка.
    /// </summary>
    /// <param name="canvas">поверхность рисования</param>
    /// <param name="bounds">границы</param>
    public override void Draw(Graphics canvas, Rectangle bounds)
    {
      canvas.DrawString(Closed ? Symbols.DoorClosed : Symbols.DoorOpened, Balance.DefaultFont, Brushes.Brown, bounds);
    }

    /// <summary>
    /// "Прозрачность".
    /// </summary>
    /// <remarks>определяет, можно ли пройти сквозь объект</remarks>
    public override bool CanGoThrough
    {
      get
      {
        return !Closed;
      }
    }

	  /// <summary>
	  /// Порядок отрисовки объектов.
	  /// </summary>
	  public override HeightDisplayOrder HeightOrder
	  {
			get { return HeightDisplayOrder.Medium; }
	  }

	  /// <summary>
    /// Получение строкового описания.
    /// </summary>
    /// <param name="forActor">для персонажа</param>
    /// <returns>краткое строковое описание для подсказок карты</returns>
		public override string GetDescription(Actor forActor)
    {
      return (Closed ? "закрытая" : "открытая") + " дверь";
    }

		#region Implementation of IInteractive

		/// <summary>
    /// Взаимодействие.
    /// </summary>
    /// <param name="sender">субъект, вызвавший взаимодействие</param>
		/// <param name="interaction">выбранное взаимодействие</param>
		/// <returns>результат воздействия</returns>
		public ActionResult Handle(Actor sender, int interaction)
    {
			switch (interaction)
	    {
				case interactionOpen:
			    if (Closed)
			    {
				    Closed = false;
			    }
			    else
			    {
						throw new NotSupportedException("Нельзя открыть незакрытую дверь!");
			    }
			    break;
				case interactionClose:
					if (!Closed)
					{
						Closed = true;
					}
					else
					{
						throw new NotSupportedException("Нельзя заткрыть уже закрытую дверь!");
					}
					break;
				default:
					throw new NotSupportedException();
	    }

      return new ActionResult(
        Balance.ActionLongevityDoor * sender.Speed,
        string.Format("{0} {1} дверь в точке {2}", sender.Description, Closed ? "закрыл" : "открыл", Position));
    }

	  /// <summary>
	  /// Все возможные взаимодействия.
	  /// </summary>
	  /// <returns>список</returns>
	  public IList<Interaction> GetInteractions()
	  {
		  return new List<Interaction>
		  {
			  new Interaction(interactionOpen, "открыть дверь", actor => Closed),
				new Interaction(interactionClose, "закрыть дверь", actor => !Closed),
		  };
	  }

	  private const int interactionOpen = 1;
		private const int interactionClose = 2;

    #endregion
  }
}
