using System;
using System.Collections.Generic;
using System.Drawing;

using Roguelike.Actions;
using Roguelike.Objects.Interfaces;
using Roguelike.Worlds;

namespace Roguelike.Objects.StaticEnvironment
{
  /// <summary>
  /// Объект: растение.
  /// </summary>
	public class Herb : WorldObject, IInteractive
	{
		#region Свойства

		/// <summary>
    /// Вид.
    /// </summary>
		public readonly HerbKind Kind;

		#endregion

    /// <summary>
    /// Прорисовка.
    /// </summary>
    /// <param name="canvas">поверхность рисования</param>
    /// <param name="bounds">границы</param>
    public override void Draw(Graphics canvas, Rectangle bounds)
    {
      canvas.DrawString(Kind.Type.Symbol, Balance.DefaultFont, Kind.Brush, bounds);
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
    /// Получение строкового описания.
    /// </summary>
    /// <param name="forActor">для персонажа</param>
    /// <returns>краткое строковое описание для подсказок карты</returns>
		public override string GetDescription(Actor forActor)
    {
      return Kind.GetDescription(World.Instance.Hero);
    }

    /// <summary>
    /// ctor.
    /// </summary>
    /// <param name="kind">вид</param>
    public Herb(HerbKind kind)
    {
      Kind = kind;
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
				case interactionPickup:
					sender.PickupHerb(this);
        return new ActionResult(
          Balance.ActionLongevityHerb * sender.Speed,
          string.Format("{0} собрал {1}", sender.Description, Kind.GetDescription(World.Instance.Hero)));
				default:
					throw new NotSupportedException();
			}
		}

		/// <summary>
		/// Все возможные взаимодействия.
		/// </summary>
		/// <returns>список</returns>
		public IList<Interaction> GetInteractions()
		{
			return new List<Interaction>
		  {
			  new Interaction(interactionPickup, "собрать растение", actor => true),
		  };
		}

		private const int interactionPickup = 1;

		#endregion
  }
}
