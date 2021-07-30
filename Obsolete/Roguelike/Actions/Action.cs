using System;
using System.Windows.Forms;

using Roguelike.Items;
using Roguelike.Objects;
using Roguelike.Objects.ActiveCharacters;
using Roguelike.Objects.Interfaces;
using Roguelike.Worlds;

namespace Roguelike.Actions
{
  /// <summary>
  /// Действие, которое может быть выполнено.
  /// </summary>
  public abstract class Action
  {
    #region Типы обработчика

    /// <summary>
    /// Обработчик без параметра.
    /// </summary>
    /// <param name="sender">действующее лицо</param>
    /// <returns>результат действия</returns>
    protected internal delegate ActionResult ActionHandler(Actor sender);

    /// <summary>
    /// Обработчик без параметра.
    /// </summary>
    /// <typeparam name="ParamsT">тип входного параметра</typeparam>
    /// <param name="sender">действующее лицо</param>
    /// <param name="parameter">входной параметр</param>
    /// <returns>результат действия</returns>
    protected internal delegate ActionResult ActionHandler<in ParamsT>(Actor sender, ParamsT parameter);

    #endregion

    #region Список

    /// <summary>
    /// Движение.
    /// </summary>
    public static readonly ActionWithParameters<Direction> Strafe = new ActionWithParameters<Direction>(handlerStrafe);

    /// <summary>
    /// Взаимодействие с объектом.
    /// </summary>
    public static readonly ActionWithParameters<IInteractive> Handle = new ActionWithParameters<IInteractive>(handlerHandle);

    /// <summary>
    /// Выбросить из рюкзака.
    /// </summary>
    public static readonly ActionWithParameters<Item> DropItem = new ActionWithParameters<Item>(handlerDropItem);

    /// <summary>
    /// Диалог.
    /// </summary>
    public static readonly ActionWithParameters<Actor> StartChat = new ActionWithParameters<Actor>(handlerStartChat);

    /// <summary>
    /// Нанесение повреждения.
    /// </summary>
    public static readonly ActionWithParameters<Actor> Hit = new ActionWithParameters<Actor>(handlerHit);

	/// <summary>
	/// Стрельба.
	/// </summary>
	public static readonly ActionWithParameters<Actor> Shoot = new ActionWithParameters<Actor>(handlerShoot);

	/// <summary>
    /// Изменение боеготовности.
    /// </summary>
    public static readonly ActionWithoutParameters SwitchAgressive = new ActionWithoutParameters(handlerSwitchAgressive);

    /// <summary>
    /// Изменение выбранного оружия.
    /// </summary>
    public static readonly ActionWithParameters<Weapon> ChangeWeapon = new ActionWithParameters<Weapon>(handlerChangeWeapon);
      
    /// <summary>
    /// Изменение одежды.
    /// </summary>
    public static readonly ActionWithParameters<Wear> ChangeWear = new ActionWithParameters<Wear>(handlerChangeWear);

    /// <summary>
    /// Поедание травы.
    /// </summary>
    public static readonly ActionWithParameters<HerbPack> Eat = new ActionWithParameters<HerbPack>(handlerEat);

    /// <summary>
    /// Чтение книги.
    /// </summary>
    public static readonly ActionWithParameters<Book> ReadBook = new ActionWithParameters<Book>(handlerReadBook);

    /// <summary>
    /// Осмотр.
    /// </summary>
    public static readonly ActionWithoutParameters LookAround = new ActionWithoutParameters(handlerLookAround);

    #endregion

    #region Обработчики

    // обработчик действия: Движение
    private static ActionResult handlerStrafe(Actor sender, Direction direction)
    {
      bool inWater = World.Instance.GetCell(sender.Position).Background == CellBackground.Water;
      bool canMove = inWater ? sender.Body.CanSwim() : sender.Body.CanWalk();
      if (canMove)
      {
        sender.Strafe(direction);
        return new ActionResult(
          DirectionHelper.GetMovementCost(direction) * Balance.ActionLongevityStrafe * sender.Speed,
#if DEBUG
          string.Format("{0} {1} в направлении {2}", sender.Description, inWater ? "проплыл" : "шагнул", DirectionHelper.GetDescription(direction))
#else
          null
#endif
);
      }
      else
        return handlerInvalid(sender);
    }

    // обработчик действия: Взаимодействие с объектом
    private static ActionResult handlerHandle(Actor sender, IInteractive handler)
    {
      // поверка возможности действия
      if (!sender.Body.CanHandle())
        return handlerInvalid(sender);

      // выбор варианта взамодействия
	    int interaction;
			return sender.SelectInteraction(handler.GetInteractions(), out interaction)
				? handler.Handle(sender, interaction) // взаимодействие
				: ActionResult.Empty;
    }

    // обработчик действия: Диалог
    private static ActionResult handlerStartChat(Actor sender, Actor actor)
    {
      if (sender.Body.CanChat())
      {
        sender.Chat(actor);
        return new ActionResult(
          Balance.ActionLongevityChat,
          string.Format("{0} поговорил с {1}", sender.Description, actor.Description));
      }
      else
        return handlerInvalid(sender);
    }

    // обработчик действия: Нанесение повреждения
    private static ActionResult handlerHit(Actor sender, Actor target)
    {
      if (sender.Body.CanFight())
      {
        string log = sender.Hit(target);
        return new ActionResult(Balance.ActionLongevityHit * sender.Speed, log);
      }
      else
        return handlerInvalid(sender);
    }

	// обработчик действия: Стрельба
	private static ActionResult handlerShoot(Actor sender, Actor target)
	{
#warning implement
		throw new NotImplementedException();
	}

    // обработчик действия: Изменение боеготовности
    private static ActionResult handlerSwitchAgressive(Actor sender)
    {
	    return sender.SwitchAgressive();
    }

      private static ActionResult handlerChangeWeapon(Actor sender, Weapon weapon)
      {
		  return /*sender.Body.CanChangeWeapon()
				  ? sender.ChangeWeapon()
				  : */handlerInvalid(sender);
      }

	  // обработчик действия: Изменение одежды.
    private static ActionResult handlerChangeWear(Actor sender, Wear wear)
    {
#warning uncomment
      /*if (sender.Body.CanChangeWear())
      {
        // выбор одежды
        var oldWear = sender.SelectedWear;
        var newWear = sender.ChangeWear();
        var hero = World.Instance.Hero;

        // описание действия
        string wearMessage;
        if (oldWear != null && newWear != null)
          // произошла смена одежды
          wearMessage = string.Format("переодел {0} на {1}", oldWear.GetDescription(hero), newWear.GetDescription(hero));
        else if (oldWear == null && newWear != null)
          // надета новая одежда
          wearMessage = "надел на себя " + newWear.GetDescription(hero);
        else if (oldWear != null && newWear == null)
          // снята всякая одежда
          wearMessage = "снял с себя " + oldWear.GetDescription(hero);
        else
          // смены одежды не было
          return ActionResult.Empty;

        return new ActionResult(
          Balance.ActionLongevityArmor * sender.Speed,
          string.Format("{0} {1}", sender.Description, wearMessage));
      }
      else*/
        return handlerInvalid(sender);
    }

    // обработчик действия: Поедание травы.
    private static ActionResult handlerEat(Actor sender, HerbPack selectedHerbOrMushroom)
    {
      if (sender.Body.CanEat())
      {
        // поедаем
        if (selectedHerbOrMushroom != null)
        {
          string result = sender.ApplyMedicine(selectedHerbOrMushroom);
          return new ActionResult(Balance.ActionLongevityMedicine, sender.Description + " ест, " + result);
        }
        else
          return new ActionResult(Balance.ActionLongevityWait * sender.Speed, sender.Description + " ищет cъестное, но припасов нет");
      }
      else
        return handlerInvalid(sender);
    }

    // обработчик действия: Выбросить из рюкзака.
    private static ActionResult handlerDropItem(Actor sender, Item selectedItem)
    {
      if (sender.Body.CanDrop())
      {
        // выбрасываем
        if (selectedItem != null)
        {
          sender.Drop(selectedItem);
          return new ActionResult(
            Balance.ActionLongevityDrop,
            string.Format("{0} выбрасывает {1} из своего рюкзака", sender.Description,
            selectedItem.GetDescription(World.Instance.Hero)));
        }
        else
          return new ActionResult(
            Balance.ActionLongevityWait * sender.Speed,
            sender.Description + " порылся в своём рюкзаке");
      }
      else
        return handlerInvalid(sender);
    }

    // обработчик действия: Чтение книги.
    private static ActionResult handlerReadBook(Actor sender, Book book)
    {
      if (sender.Body.CanRead())
      {
          sender.ReadBook(book);
          return new ActionResult(Balance.ActionLongevityRead * sender.ReadSpeed, string.Format("{0} читает {1}", sender.Description, book.GetDescription(World.Instance.Hero)));
      }
      else
        return handlerInvalid(sender);
    }

    // обработчик действия: Осмотр по сторонам.
    private static ActionResult handlerLookAround(Actor sender)
    {
      if (sender.Body.CanLookAround())
      {
        sender.LookAround();
        return new ActionResult(
          Balance.ActionLongevityWait * sender.Speed,
#if DEBUG
          sender.Description + " осмотрелся"
#else
          null
#endif
);
      }
      else
        return handlerInvalid(sender);
    }

    // обработчик действия: движения инвалида
    private static ActionResult handlerInvalid(Actor sender)
    {
      return new ActionResult(
        Balance.ActionLongevityWait,
        sender.Description + " пытается двигаться, но не может");
    }

    #endregion
  }

  #region Дочерние типы действия

  /// <summary>
  /// Действие, которое может быть выполнено(без параметров).
  /// </summary>
  public class ActionWithoutParameters : Action
  {
    // непосредственный обработчик
    private readonly ActionHandler Handler;

    /// <summary>
    /// ctor.
    /// </summary>
    /// <param name="handler">обработчик действия</param>
    internal ActionWithoutParameters(ActionHandler handler)
    {
      Handler = handler;
    }

    /// <summary>
    /// Непосредственный вызов.
    /// </summary>
    /// <param name="sender">действующее лицо</param>
    /// <returns>результат действия</returns>
    public ActionResult Call(Actor sender)
    {
      return Handler(sender);
    }
  }

  /// <summary>
  /// Действие, которое может быть выполнено(с параметрами).
  /// </summary>
  /// <typeparam name="ParamsT">тип входного параметра</typeparam>
  public class ActionWithParameters<ParamsT> : Action
  {
    // непосредственный обработчик
    private readonly ActionHandler<ParamsT> Handler;

    /// <summary>
    /// ctor.
    /// </summary>
    /// <param name="handler">обработчик действия</param>
    internal ActionWithParameters(ActionHandler<ParamsT> handler)
    {
      Handler = handler;
    }

    /// <summary>
    /// Непосредственный вызов.
    /// </summary>
    /// <param name="sender">действующее лицо</param>
    /// <param name="parameter">входной параметр</param>
    /// <returns>результат действия</returns>
    public ActionResult Call(Actor sender, ParamsT parameter)
    {
      return Handler(sender, parameter);
    }
  }

  #endregion
}
