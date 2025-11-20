using System;
using System.Collections.Generic;
using System.Drawing;

using Roguelike.Actions;
using Roguelike.Items;
using Roguelike.Objects.Interfaces;
using Roguelike.Worlds;

namespace Roguelike.Objects.StaticEnvironment
{
	/// <summary>
	/// Объект: труп.
	/// </summary>
	public class Corpse : WorldObject, IInteractive
	{
		#region Свойства

		/// <summary>
		/// Персонаж, которому "принадлежит" труп.
		/// </summary>
		public Actor Actor
		{ get; private set; }

		/// <summary>
		/// Предметы трупа.
		/// </summary>
		public Inventory Inventory
		{ get; private set; }

		#endregion

		/// <summary>
		/// ctor.
		/// </summary>
		/// <param name="actor">персонаж, которому "принадлежит" труп</param>
		public Corpse(Actor actor)
		{
			Actor = actor;
		}

		#region Implementation of WorldObject

		/// <summary>
		/// Получение строкового описания.
		/// </summary>
		/// <param name="forActor">для персонажа</param>
		/// <returns>краткое строковое описание для подсказок карты</returns>
		public override string GetDescription(Actor forActor)
		{
			return string.Format("труп ({0})", Actor.GetDescription(forActor));
		}

		/// <summary>
		/// Прорисовка.
		/// </summary>
		/// <param name="canvas">поверхность рисования</param>
		/// <param name="bounds">границы</param>
		public override void Draw(Graphics canvas, Rectangle bounds)
		{
			canvas.DrawString(
			 Symbols.DeadFace,
			 Balance.DefaultFont,
			 Actor.GetWearingBrush(),
			 bounds);
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

		#endregion

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
				case interactionInventory:
					// выбираем что брать
#warning тут должен быть вызов формы инвентори обыскиваемого персонажа
					var selected = sender.SelectFromOther();

					// берём
					if (selected.Value != null)
					{
						sender.PickupItem(selected.Value, selected.Key);
						return new ActionResult(
							Balance.ActionLongevityPickup,
							string.Format("{0} подбирает {1} с {2}", sender.Description, selected.Key.GetDescription(World.Instance.Hero), selected.Value.Description));
					}
					else
						return new ActionResult(
							Balance.ActionLongevityWait * sender.Speed,
							sender.Description + " порылся в своём рюкзаке");

					return new ActionResult(Balance.ActionLongevityPickup * sender.Speed, sender.Description + " обыскал " + GetDescription(sender));
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
			  new Interaction(interactionInventory, "обыскать", actor => true),
		  };
		}

		private const int interactionInventory = 1;
#warning добавить действие: спрятать труп

		#endregion
	}
}
