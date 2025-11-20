using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

using Roguelike.Actions;
using Roguelike.GUI;
using Roguelike.Items;
using Roguelike.Objects.Interfaces;
using Roguelike.Objects.StaticEnvironment;
using Roguelike.Worlds;

using Sef.Common;

namespace Roguelike.Objects.ActiveCharacters
{
  /// <summary>
  /// Герой.
  /// </summary>
  public class Hero : Actor
  {
    #region Overrides of WorldObject

    /// <summary>
    /// Получение строкового описания.
    /// </summary>
    /// <param name="forActor">для персонажа</param>
    /// <returns>краткое строковое описание для подсказок карты</returns>
		public override string GetDescription(Actor forActor)
    {
      return Name + " (главный герой)";
    }

    #endregion

    #region Управление

    /// <summary>
    /// Обработка нажатия клавиши.
    /// </summary>
    /// <param name="key">кнопка</param>
    /// <returns>сообщение для журнала</returns>
    public IList<string> ProcessKey(Keys key)
    {
      Func<ActionResult> handler;
      if (!posibleActions.TryGetValue(key, out handler))
        return new List<string>();
      var result = handler.Invoke();
      NextActionTime = NextActionTime.AddSeconds(result.Longevity);
      return result.LogMessages;
    }

    /// <summary>
    /// Список возможных действий.
    /// </summary>
    private readonly Dictionary<Keys, Func<ActionResult>> posibleActions;

    /// <summary>
    /// ctor.
    /// </summary>
    /// <param name="settings">настройки</param>
    public Hero(StartSettings settings)
    {
      Name = string.Format("{0}, {1}", settings.Name, settings.Nickname);
      SexIsMale = settings.SexMale;

      // одеваемся
      WearItem(Wear.All.GetRandom(World.Instance.God).ApplyRandomModifier());

      // применяем стартовый бонус
			settings.Bonus.Apply(this);

      posibleActions = new Dictionary<Keys, Func<ActionResult>> {
#region управление движением
        { Keys.NumPad8, () => strafe(Direction.Up) },
        { Keys.NumPad2, () => strafe(Direction.Down) },
        { Keys.NumPad4, () => strafe(Direction.Left) },
        { Keys.NumPad6, () => strafe(Direction.Right) },
        { Keys.NumPad1, () => strafe(Direction.DownLeft) },
        { Keys.NumPad3, () => strafe(Direction.DownRight) },
        { Keys.NumPad7, () => strafe(Direction.UpLeft) },
        { Keys.NumPad9, () => strafe(Direction.UpRight) },
        { Keys.NumPad5, () => strafe(Direction.Center) },
        { Keys.NumPad0, () => strafe(Direction.Center) },
        { Keys.Up, () => strafe(Direction.Up) },
        { Keys.Down, () => strafe(Direction.Down) },
        { Keys.Left, () => strafe(Direction.Left) },
        { Keys.Right, () => strafe(Direction.Right) },
        { Keys.End, () => strafe(Direction.DownLeft) },
        { Keys.PageDown, () => strafe(Direction.DownRight) },
        { Keys.Home, () => strafe(Direction.UpLeft) },
        { Keys.PageUp, () => strafe(Direction.UpRight) },
        { Keys.Insert, () => strafe(Direction.Center) },
#endregion
#region прочее
        //  взаимодействовать с предметом
        { Keys.H, () =>
        {
            IInteractive handler;
            if (SelectHandler(out handler))
                return Actions.Action.Handle.Call(this, handler);
            else
                return ActionResult.Empty;
        }
        },
        //  диалог
        { Keys.S, () =>
        {
            var actor = SelectChatPair();
            if (actor != null) 
                return Actions.Action.StartChat.Call(this, actor);
            else
                return ActionResult.Empty;
        }
        },
        //  выбросить предмет
        { Keys.D, () =>
        {
            // выбираем вещь
        var selectedItem = SelectDrop();
        return Actions.Action.DropItem.Call(this, selectedItem);
        }
        },
        //  одеть броню
        { Keys.W, () =>
        {
#warning ChangeWear
            return Actions.Action.ChangeWear.Call(this, null);
        }
        },
        // приготовить оружие к бою
        { Keys.F, () => Actions.Action.SwitchAgressive.Call(this) },
        // зохавать травы
        { Keys.M, () =>
        {
            return Actions.Action.Eat.Call(this, SelectMedicine());
        }
        },
        // прочитать книгу
        { Keys.R, () =>
        {

#warning if (SelectBook()
            Book book = null;
            return Actions.Action.ReadBook.Call(this, book);
        }
        },
        // осмотреться
        { Keys.L, () => Actions.Action.LookAround.Call(this) },
#warning Упорядочить список действий
#endregion
      };
#warning Сделать нормальный Dictionary
#warning Сделать настройку клавиатуры
		}

    /// <summary>
    /// Переместиться в сторону.
    /// </summary>
    /// <param name="direction">направление</param>
    /// <returns>итог действия</returns>
    private ActionResult strafe(Direction direction)
    {
      return StrafeOrHit(direction);
    }

    #endregion

    #region Implementation of IActiveObject

    /// <summary>
    /// Выполнить действие.
    /// </summary>
    /// <returns>сообщение для лога</returns>
    public override ActionResult Do()
    {
      // проверка на предыдущие ранения
      if (IsDead)
      {
        World.EndGame(string.Format("{0} {1}", Description, DeadReason));
        return new ActionResult(Balance.ActionLongevityNull, Description + " " + DeadReason);
      }

#if DEBUG
      return new  ActionResult(Balance.ActionLongevityNull, new List<string> { "отработка игровой механики..." });
#else
      return ActionResult.Empty;
#endif
    }

    #endregion

    #region Взаимодействие

    /// <summary>
    /// Выбор предмета для взаимодействия.
    /// </summary>
    /// <param name="handler">объект для взаимодействия</param>
    /// <returns><b>true</b>, если предмет выбран, иначе - <b>false</b></returns>
    public override bool SelectHandler(out IInteractive handler)
    {
      // объекта по умолчанию нет
      handler = null;

      // поиск объектов для взаимодействия
			var handlers = new Dictionary<IInteractive, Direction>();
	    foreach (var pair in World.Instance.GetCellsAround(Position))
	    {
		    foreach (var i in pair.Value.Objects.OfType<IInteractive>())
		    {
			    handlers.Add(i, pair.Key);
		    }
	    }

      // если объектов нет
      if (handlers.Count == 0)
      {
        MessageBox.Show("Ни одного объекта взаимодействия в пределах досягаемости не найдено!", string.Empty);
        return false;
      }

      // если объект есть только один
      if (handlers.Count == 1)
      {
	      handler = handlers.Keys.First();
        return true;
      }

      // выбор объектов из списка
      var choises = new List<ChoiseObject>();
      foreach (var h in handlers)
        choises.Add(new Choise<IInteractive>(
          string.Format("{0} в направлении {1}", h.Key.Description, DirectionHelper.GetDescription(h.Value)),
					h.Key));
      var choise = SelectionDialog.Show("С чем взаимодействовать?", choises);
      if (choise != CancelChoise.Value)
      {
        handler = ((Choise<IInteractive>) choise).Value;
        return true;
      }
      else
        return false;
    }

	  /// <summary>
	  /// Выбор предмета для взаимодействия.
	  /// </summary>
	  /// <param name="interactions">список возможных взаимодействий</param>
	  /// <param name="interaction">вид взаимодействия</param>
	  /// <returns><b>true</b>, если взаимодействие выбрано, иначе - <b>false</b></returns>
	  public override bool SelectInteraction(IList<Interaction> interactions, out int interaction)
	  {
			// выбор объектов из списка
			var choises = new List<ChoiseObject>();
			foreach (var i in interactions)
				choises.Add(new Choise<Interaction>(i.Description, i, i.IsPossible(this)));
			choises.Add(CancelChoise.Value);
			var choise = SelectionDialog.Show("Что сделать?", choises);
		  if (choise != CancelChoise.Value)
		  {
			  interaction = ((Choise<Interaction>) choise).Value.Id;
			  return true;
		  }
		  else
		  {
			  interaction = int.MinValue;
			  return false;
		  }
	  }

    /// <summary>
    /// Выбор собеседника для разговора.
    /// </summary>
    /// <param name="actor">выбранный собеседник</param>
    /// <returns><b>true</b>, если предмет выбран, иначе - <b>false</b></returns>
    public override bool SelectChatPair(out Actor actor)
    {
      // собеседника по умолчанию нет
      actor = null;
#warning Implement
        return false;
    }

    /// <summary>
    /// Выбор с кем поговорить.
    /// </summary>
    /// <returns>выбранный собеседник</returns>
    public override Actor SelectChatPair()
    {
      var possibleActors = new Dictionary<Direction, Npc>();
	    foreach (var pair in World.Instance.GetCellsAround(Position))
	    {
		    foreach (var npc in pair.Value.Objects.OfType<Npc>())
		    {
			    possibleActors.Add(pair.Key, npc);
		    }
	    }
      return World.Instance.SelectObjectFromAround(
        possibleActors,
        "С кем поговорить?",
        "Нельзя разговаривать с самим собой!",
        "отмена - молчать");
    }

    /// <summary>
    /// Разговор.
    /// </summary>
    /// <param name="pair">собеседник</param>
    public override void Chat(Actor pair)
    {
      if (pair.Body.CanChat())
      {
#warning Разобраться с этим левым приведением! Но просто так его удалять нельзя.
        var actor = (Npc) pair;
        var choises = new List<ChoiseObject>();
        if (!actor.IsKnown)
					choises.Add(new Choise<object>("Спросить имя...", new object()));
        var choise = SelectionDialog.Show("О чём говорить?", choises).ValueObject;
        if (choise != null)
        {
          MessageBox.Show("Меня зовут " + actor.Name, "Диалог");
          actor.Known();
        }
        else
          MessageBox.Show(Description + " не может говорить", "Диалог");
#warning Нужно ли тут это предупреждение?
      }
    }
#warning Чат нужно реализовать в виде отдельного диалога взаимодействия.
		/*
    /// <summary>
    /// Выбрать оружие.
    /// </summary>
    /// <returns>выбранное оружие</returns>
    public override Weapon SelectWeapon()
    {
			var weapons = Inventory.AllItems.OfType<Weapon>().Where(i => i.Class == ItemClass.Weapon);
      if (weapons.Any())
      {
        var choises = new List<ChoiseObject>();
        foreach (var weapon in weapons)
					choises.Add(new Choise<Weapon>(weapon.GetDescription(this), weapon));
				choises.Add(new Choise<Weapon>("--- драться врукопашную ---", Weapon.HandToHand));
        return SelectionDialog.Show("Выберите, чем драться...", choises).ValueObject as Weapon;
      }
      else
      {
        MessageBox.Show("Оружия нет - имеется в виду рукопашный бой!", "предупреждение");
        return null;
      }
    }

    /// <summary>
    /// Выбрать одежду.
    /// </summary>
    /// <returns>выбранное одеяние</returns>
    public override Wear SelectWear()
    {
			var wears = Inventory.AllItems.OfType<Wear>().Where(i => i.Class == ItemClass.Wear);
      if (wears.Any())
      {
        var choises = new List<ChoiseObject>();
        foreach (var wear in wears)
					choises.Add(new Choise<Wear>(wear.GetDescription(this), wear));
				choises.Add(new Choise<Wear>("--- снять уже надетое ---", Wear.Naked));
        return SelectionDialog.Show("Выберите, что одеть...", choises).ValueObject as Wear;
      }
      else
      {
        MessageBox.Show("Одежды и доспехов нет!", "предупреждение");
        return null;
      }
    }*/

    /// <summary>
    /// Выбрать пачку трав.
    /// </summary>
    /// <returns>выбранное растение</returns>
    public override HerbPack SelectMedicine()
    {
#warning inventory
			var herbs = Inventories.First().AllItems.OfType<HerbPack>().Where(i => i.Class == ItemClass.Herb);
      if (herbs.Any())
      {
        var choises = new List<ChoiseObject>();
        foreach (var herb in herbs)
					choises.Add(new Choise<HerbPack>(herb.Kind.GetDescription(this), herb));
        return SelectionDialog.Show("что будем употреблять...", choises).ValueObject as HerbPack;
      }
      else
      {
        MessageBox.Show("Растений в запасе нет!", "предупреждение");
        return null;
      }
    }

    /// <summary>
    /// Осмотреться.
    /// </summary>
    public override void LookAround()
    {
      var now = World.Instance.TimeStamp;
      string time = null, date = null;
      int int_ = GetModifiedIntelligence();
      if (int_ >= 2)
      {
        time = now.ToString("hh:mm");
        date = now.ToLongDateString();
      }
      else if (int_ >= 1)
      {
        if (now.Hour >= 6 && now.Hour < 12)
          time = "утро";
        else if (now.Hour >= 12 && now.Hour < 18)
          time = "день";
        else if (now.Hour >= 18 && now.Hour < 22)
          time = "вечер";
        else
          time = "ночь";
        if (now.Month >= 3 && now.Month < 6)
          date = "весна";
        else if (now.Month >= 6 && now.Month < 9)
          date = "лето";
        else if (now.Month >= 9 && now.Month < 12)
          date = "осень";
        else
          date = "зима";
      }
      string message = (int_ > 0) ? string.Format("Сейчас {0}, {1}", time, date) : "ничего не обнаружено";
      MessageBox.Show(message, "результат осмотра");
    }

    /// <summary>
    /// Выбрать вещь на выброс.
    /// </summary>
    /// <returns>выбранный предмет</returns>
    public override Item SelectDrop()
    {
#warning uncomment
      /*var choises = new List<ChoiseObject>();
      foreach (var item in Inventory.AllItems)
        if (item != SelectedWear && item != SelectedWeapon) // нельзя выкидывать надетую вещь!
					choises.Add(new Choise<Item>(item.Name, item));

      if (choises.Count > 0)
      {
        var selectedChoise = SelectionDialog.Show("Выберите, что выбросить...", choises);
        return (selectedChoise is CancelChoise)
          ? null
          : (selectedChoise as Choise<Item>).Value;
      }
      else*/
      {
        MessageBox.Show("Нечего выбросить!", "предупреждение");
        return null;
      }
    }

    /// <summary>
    /// Выбрать вещь на поднятие.
    /// </summary>
    /// <returns>выбранный предмет</returns>
    public override KeyValuePair<Item, Actor> SelectFromOther()
    {
#warning uncomment
      /*var possibleItems = new Dictionary<Item, Corpse>();
			foreach (var corpse in World.Instance.GetCell(Position).Objects.OfType<Corpse>())
      {
        foreach (var item in corpse.Actor.Inventory.AllItems)
          possibleItems.Add(item, corpse);
      }

      if (possibleItems.Count > 0)
      {
        var choises = new List<ChoiseObject>();
        foreach (var item in possibleItems)
        {
          string description = item.Key.Name;
          if (item.Key != null)
            description += string.Format(" (взять с {0})", item.Value.Description);
					choises.Add(new Choise<KeyValuePair<Item, Actor>>(description, new KeyValuePair<Item, Actor>(item.Key, item.Value.Actor)));
        }
        var selectedChoise = SelectionDialog.Show("Выберите, что поднять...", choises);
        return (selectedChoise is CancelChoise)
          ? new KeyValuePair<Item, Actor>(null, null)
          : (selectedChoise as Choise<KeyValuePair<Item, Actor>>).Value;
      }
      else*/
      {
        MessageBox.Show("Здесь нечего поднять!", "предупреждение");
        return new KeyValuePair<Item, Actor>();
      }
    }

    /// <summary>
    /// Прочесть книгу.
    /// </summary>
    /// <returns></returns>
    /// <param name="book">выбранная книга</param>
    public override void ReadBook(Book book)
    {
      BigTextForm.ShowDialog(book.Text, book.Name);
    }

    #endregion
  }
}
