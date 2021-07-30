using System;
using System.Collections.Generic;
using System.Drawing;

using Roguelike.Actions;
using Roguelike.Body;
using Roguelike.Items;
using Roguelike.Objects.Base;
using Roguelike.Objects.StaticEnvironment;
using Roguelike.Worlds;

namespace Roguelike.Objects.Base
{
  /// <summary>
  /// Базовый класс живого активного объекта.
  /// </summary>
  public abstract class Actor : WorldObject, IActiveObject
  {
    #region Состояние смерти

    /// <summary>
    /// Действующее лицо мертво.
    /// </summary>
    public bool Dead
    { get; private set; }

    /// <summary>
    /// Смерть.
    /// </summary>
    /// <param name="reason">причина смерти</param>
    internal void Die(string reason)
    {
      Dead = true;
      DeadReason = reason;
    }

    /// <summary>
    /// Причина смерти.
    /// </summary>
    public string DeadReason
    { get; internal set; }

    #endregion

    #region Бой

    /// <summary>
    /// Текущее оружие.
    /// </summary>
    /// <remarks>null - режим боя отключён, Weapon.HandToHand - рукопашный бой</remarks>
    public Weapon SelectedWeapon
    { get; internal set; }

    /// <summary>
    /// Текущая одежда.
    /// </summary>
    /// <remarks>null - без одежды</remarks>
    public Wear SelectedWear
    { get; internal set; }

    /// <summary>
    /// Состояние: готов к бою.
    /// </summary>
    public bool IsArmed
    { get { return SelectedWeapon != null; } }

    /// <summary>
    /// Состояние: раздет.
    /// </summary>
    public bool IsNaked
    { get { return SelectedWear == null; } }

    /// <summary>
    /// Переместиться в сторону.
    /// </summary>
    /// <param name="direction">направление</param>
    /// <returns>итог действия</returns>
    protected ActionResult StrafeOrHit(Direction direction)
    {
      Actor target = null;

      // проверяем, есть ли кого ударить
      if (IsArmed)
      {
        var newPos = Position.Strafe(direction);
        foreach (var o in World.Instance.GetObjectsInPos(newPos))
        {
          var actor = o as Actor;
          if ((actor != null) && !actor.Dead && (actor != this))
          {
            target = actor;
            break;
          }
        }
      }

      // пишем результат
      var result = (target == null)
        ? Actions.Action.Strafe.Call(this, direction)
        : Actions.Action.Hit.Call(this, target);

      // истекаем кровью, если надо
      if (Body.HpTotalCurrent < Body.HpTotalMax * 3 / 4)
        Bleed();

      return result;
    }

    /// <summary>
    /// Атака.
    /// </summary>
    /// <param name="target">кого</param>
    /// <returns>сообщение об результате</returns>
    public string Hit(Actor target)
    {
      // наносим удар
      int power = (int) (Properties.Strength + (SelectedWeapon != null ? SelectedWeapon.Bonus.Damage : 0));
      string organMessage = target.Body.TakeDamage(power);

      // добавляем кровь
      target.Bleed();

      // составляем сообщение
      string logMessage = string.Format(
        "{0} ударил {1}{2} с силой {3} ({4}) {5}.",
        Description,
        target.Description,
        (SelectedWeapon != null) ? string.Format(" (оружие - {0} +{1})", SelectedWeapon.Name, SelectedWeapon.Bonus.Damage) : string.Empty,
        power,
        organMessage,
        target.Body.Destroyed ? " и убил его" : string.Empty);

      // если много повреждений - убиваем цель
      if (target.Body.Destroyed)
        target.Die("скончался от полученных повреждений");

      // возвращаем сообщение
      return logMessage;
    }

    /// <summary>
    /// Истечь кровью.
    /// </summary>
    public void Bleed()
    {
      // если под нами нет ещё кровавой лужи
      if (World.Instance.GetObjectInPos<BloodPool>(Position) == null)
        // собственно, истекаем
        World.Instance.CorrectAdd(new BloodPool(), Position);
    }

    #endregion

    #region Implementation of IActiveObject

    /// <summary>
    /// Выполнить действие.
    /// </summary>
    /// <returns>сообщение для лога</returns>
    public abstract IList<string> Do();

    /// <summary>
    /// Время следующего действия.
    /// </summary>
    public DateTime NextActionTime
    { get; set; }

    #endregion

    #region Implementation of WorldObject

    /// <summary>
    /// Прорисовка.
    /// </summary>
    /// <param name="canvas">поверхность рисования</param>
    /// <param name="bounds">границы</param>
    public override void Draw(Graphics canvas, Rectangle bounds)
    {
      canvas.DrawString(
        Dead ? Symbols.DeadFace : Symbols.Face,
        Balance.DefaultFont,
        (SelectedWear != null) ? SelectedWear.DrawBrush : Wear.NullWearingBrush,
        bounds);
    }

    /// <summary>
    /// "Прозрачность".
    /// </summary>
    /// <remarks>определяет, можно ли пройти сквозь объект</remarks>
    public override bool Transparent
    {
      get
      {
        return Dead;
      }
    }

    #endregion

    #region Ролевая составляющая

    /// <summary>
    /// Имя.
    /// </summary>
    public string Name
    { get; protected set; }

    /// <summary>
    /// Пол (мужской).
    /// </summary>
    public bool SexMale
    { get; protected set; }

    /// <summary>
    /// Возраст.
    /// </summary>
    public double Age
    { get; protected set; }

    /// <summary>
    /// Характеристики.
    /// </summary>
    public CharacterProperties Properties
    { get; private set; }

    /// <summary>
    /// Тело.
    /// </summary>
    public Body.Body Body
    { get; private set; }

    /// <summary>
    /// Рюкзак.
    /// </summary>
    public Inventory Inventory
    { get; private set; }

    #region Производные характеристики

    /// <summary>
    /// Получить силу с модификаторами.
    /// </summary>
    /// <returns>модифицированная сила</returns>
    public int GetModifiedStrength()
    {
      // учёт голода
      int hungerModifier = 0;
      if (Body.IsHungred)
        hungerModifier = (int) Math.Abs(Body.Hunger) / (1 + GetModifiedWillpower());

      // учёт повреждений органов
      double damageModifier = Body.GetAgilityMinus();

      // одежда
      double wearModifier = (SelectedWear != null) ? SelectedWear.Bonus.Strength : 0;

      return (int)(Properties.Strength - hungerModifier - damageModifier + wearModifier);
    }

    /// <summary>
    /// Получить ловкость с модификаторами.
    /// </summary>
    /// <returns>модифицированная ловкость</returns>
    public int GetModifiedDexterity()
    {
      // учёт веса вещей
      double requiredStrength = Inventory.TotalWeight / 10;
      double weightModifier = Math.Max(0, requiredStrength - GetModifiedStrength());

      // учёт повреждений органов
      double damageModifier = Body.GetAgilityMinus();

      // учёт переедания
      int bloatModifier = 0;
      if (Body.IsBloated)
        bloatModifier = (int)Body.Hunger / (1 + GetModifiedWillpower());

      // одежда
      double wearModifier = (SelectedWear != null) ? SelectedWear.Bonus.Strength : 0;

      return (int)(Properties.Dexterity - weightModifier - bloatModifier - damageModifier + wearModifier);
    }

    /// <summary>
    /// Получить интеллект с модификаторами.
    /// </summary>
    /// <returns>модифицированный интеллект</returns>
    public int GetModifiedIntelligence()
    {
      return (int)Properties.Intelligence;
    }

    /// <summary>
    /// Получить волю с модификаторами.
    /// </summary>
    /// <returns>модифицированная воля</returns>
    public int GetModifiedWillpower()
    {
      return (int)Properties.Willpower;
    }

    /// <summary>
    /// Получить воспрриятие с модификаторами.
    /// </summary>
    /// <returns>модифицированное воспрриятие</returns>
    public int GetModifiedPerception()
    {
      // одежда
      double wearModifier = (SelectedWear != null) ? SelectedWear.Bonus.Perception : 0;

      return (int)(Properties.Perception + wearModifier);
    }

    /// <summary>
    /// Получить внешность с модификаторами.
    /// </summary>
    /// <returns>модифицированная внешность</returns>
    public int GetModifiedAppearance()
    {
      return (int)Properties.Appearance;
    }

    /// <summary>
    /// Получить обаяние с модификаторами.
    /// </summary>
    /// <returns>модифицированное обаяние</returns>
    public int GetModifiedCharisma()
    {
      // одежда
      double wearModifier = (SelectedWear != null) ? SelectedWear.Bonus.Charisma : 0;

      return (int)(Properties.Charisma + wearModifier);
    }

    /// <summary>
    /// Скорость выполнения действий.
    /// </summary>
    /// <remarks>описывает коэффициент, на который умножается время всех действий</remarks>
    public double Speed
    {
      get
      {
        return Math.Pow(Balance.Instance.SpeedOneBase, GetModifiedDexterity() - 1);
      }
    }

    /// <summary>
    /// Скорость чтения.
    /// </summary>
    /// <remarks>описывает коэффициент, на который умножается время чтения</remarks>
    public double ReadSpeed
    {
      get
      {
        return Math.Pow(Balance.Instance.SpeedOneBase, GetModifiedIntelligence() - 3);
      }
    }

    #endregion

    #endregion

    /// <summary>
    /// ctor.
    /// </summary>
    protected Actor()
    {
      Properties = new CharacterProperties(
        World.Instance.God.Next(1, 2),
        World.Instance.God.Next(1, 2),
        World.Instance.God.Next(1, 2),
        World.Instance.God.Next(1, 2),
        World.Instance.God.Next(1, 2),
        World.Instance.God.Next(1, 2),
        World.Instance.God.Next(1, 2));
      Body = Roguelike.Body.Body.CreateHumanoid();
      Inventory = new Inventory();
    }

    #region Взаимодействие

    /// <summary>
    /// Выбор предмета для взаимодействия.
    /// </summary>
    /// <param name="handler">объект для взаимодействия</param>
    /// <returns><b>true</b>, если предмет выбран, иначе - <b>false</b></returns>
    public abstract bool SelectHandler(out IInteractive handler);
    
    /// <summary>
    /// Выбор собеседника для разговора.
    /// </summary>
    /// <param name="actor">выбранный собеседник</param>
    /// <returns><b>true</b>, если предмет выбран, иначе - <b>false</b></returns>
    public abstract bool SelectChatPair(out Actor actor);

    /// <summary>
    /// Выбор с кем поговорить.
    /// </summary>
    /// <returns>выбранный собеседник</returns>
    public abstract Actor SelectChatPair();

    /// <summary>
    /// Выбор травы или гриба для сбора.
    /// </summary>
    /// <returns>собранная трава или гриб</returns>
    public Herb SelectHerb()
    {
      return World.Instance.GetObjectInPos<Herb>(Position);
    }

    /// <summary>
    /// Подобрать траву или гриб под ногами.
    /// </summary>
    /// <param name="pickupable">трава или гриб</param>
    public void PickupHerb(Herb pickupable)
    {
      Inventory.Add(new HerbPack(pickupable.Kind));
      World.Instance.CorrectRemove(pickupable);
    }

    /// <summary>
    /// Разговор.
    /// </summary>
    /// <param name="pair">собеседник</param>
    public abstract void Chat(Actor pair);

    /// <summary>
    /// Выбрать оружие.
    /// </summary>
    /// <returns>выбранное оружие</returns>
    public abstract Weapon SelectWeapon();

    /// <summary>
    /// Выбрать одежду.
    /// </summary>
    /// <returns>выбранное одеяние</returns>
    public abstract Wear SelectWear();

    /// <summary>
    /// Выбрать книгу для прочтения.
    /// </summary>
    /// <returns>выбранная книга</returns>
    public abstract Book SelectBook();

    /// <summary>
    /// Прочесть книгу.
    /// </summary>
    /// <returns></returns>
    /// <param name="book">выбранная книга</param>
    public abstract void ReadBook(Book book);

    /// <summary>
    /// Выбрать пачку трав.
    /// </summary>
    /// <returns>выбранное растение</returns>
    public abstract HerbPack SelectMedicine();

    /// <summary>
    /// Осмотреться.
    /// </summary>
    public abstract void LookAround();

    /// <summary>
    /// Съесть пачку трав.
    /// </summary>
    /// <param name="herb">травы</param>
    /// <returns>результат</returns>
    public string ApplyMedicine(HerbPack herb)
    {
      Inventory.Remove(herb);
      string result = Body.ApplyHerb(herb.Kind);

      // травимся, если надо
      if (Body.Destroyed)
        Die("отравился съеденным");

      // добавляем питательность
      if (Body.IsBloated)
      {
        result += " и страдает от переедания";
        Body.TakeDamage(Balance.Instance.HungerDamage);
      }
      Body.Hunger += herb.Kind.Nutricity;
      if (Body.Destroyed)
        Die("подавился во время обжорства");

      return result;
    }

    /// <summary>
    /// Выбрать вещь на выброс.
    /// </summary>
    /// <returns>выбранный предмет</returns>
    public abstract Item SelectDrop();

    /// <summary>
    /// Выбросить предмет.
    /// </summary>
    /// <param name="item">предмет</param>
    public void Drop(Item item)
    {
      Inventory.Remove(item);
    }

    /// <summary>
    /// Выбрать вещь на поднятие.
    /// </summary>
    /// <returns>выбранный предмет</returns>
    public abstract KeyValuePair<Item, Actor> SelectFromOther();

    /// <summary>
    /// Подобрать предмет.
    /// </summary>
    /// <param name="target">у кого</param>
    /// <param name="item">предмет</param>
    public void PickupItem(Actor target, Item item)
    {
      target.Inventory.Remove(item);
      Inventory.Add(item);
    }

    /// <summary>
    /// Изменить агрессивность.
    /// </summary>
    /// <returns>выбранное оружие</returns>
    public Weapon ChangeWeapon()
    {
      if (!IsArmed)
      {
        var weapon = SelectWeapon();
        if (weapon != null)
        {
          SelectedWeapon = weapon;
          return SelectedWeapon;
        }
      }
      else
        SelectedWeapon = null;

      return null;
    }

    /// <summary>
    /// Изменить одежду.
    /// </summary>
    /// <returns>выбранная одежда</returns>
    public Wear ChangeWear()
    {
      var wear = SelectWear();
      SelectedWear = (wear != Wear.Naked) ? wear : null;
      return SelectedWear;
    }
    
    #endregion
  }
}
