using System;
using System.Collections.Generic;
using System.Linq;
using Roguelike.Actions;
using Roguelike.Items;
using Roguelike.Objects.Interfaces;
using Roguelike.Worlds;

using Sef.Common;

namespace Roguelike.Objects.ActiveCharacters
{
  /// <summary>
  /// Объект: прохожий.
  /// </summary>
  public class Npc : Actor
  {
    #region Overrides of WorldObject

    /// <summary>
    /// Получение строкового описания.
    /// </summary>
    /// <param name="forActor">для персонажа</param>
    /// <returns>краткое строковое описание для подсказок карты</returns>
		public override string GetDescription(Actor forActor)
    {
      return IsKnown ? Name : (Evil ? (SexIsMale ? "разбойник" : "разбойница") : (SexIsMale ? "прохожий" : "прохожая"));
    }

    #endregion

    #region Implementation of IActiveObject

    /// <summary>
    /// Выполнить действие.
    /// </summary>
    /// <returns>сообщение для лога</returns>
    public override ActionResult Do()
    {
        ActionResult result;

        // вооружаемся, если это возможно
#warning Сфигали все стали мирными
			/*
        var weapons = Inventory.AllItems.OfType<Weapon>().Where(i => i.Class == ItemClass.Weapon);
				var wears = Inventory.AllItems.OfType<Wear>().Where(i => i.Class == ItemClass.Wear);
        if (SelectedWeapon == null && weapons.Any())
          result = Actions.Action.ChangeWeapon.Call(this);
        else if (SelectedWear == null && wears.Any())
          result = Actions.Action.ChangeWear.Call(this);
        else*/
        {
          var direction = Direction.None;
          if (Evil)
          {
            // кэшируем мир
            var world = World.Instance;

            // ищем, кого бы обидеть
            var possibleVictims = world.GetCellsNear(Position, Balance.EvilDistance).Select(c => c.Objects.OfType<Actor>().FirstOrDefault()).ToList();
	          possibleVictims.RemoveAll(v => v == null);

            // удаляем себя, чтобы не было суицидов =)
            possibleVictims.Remove(this);

            // сортируем по расстоянию до цели с ближайших
            var distances = new Dictionary<Actor, int>();
            possibleVictims.ForEach(a => distances.Add(a, Position.GetDistanceSquare(a.Position)));
            possibleVictims.Sort((v1, v2) => distances[v1].CompareTo(distances[v2]));

            // выбираем ближайшую жертву
            if (possibleVictims.Count > 0)
              direction = Position.GetDirection(possibleVictims[0].Position);
          }
          // если не определились с направление движения - бродим случайным образом
          if (direction == Direction.None)
						direction = Vector.AroundDirections8.GetRandom(World.Instance.God);

          // идём или бьём в выбранном направлении
          result = StrafeOrHit(direction);
        }

        return result;
					
		}

    #endregion

    #region Социалка

    /// <summary>
    /// Знакомый (или нет).
    /// </summary>
    public bool IsKnown
    { get; private set; }

    /// <summary>
    /// ctor.
    /// </summary>
    public Npc()
    {
      IsKnown = false;

      SexIsMale = (World.Instance.God.Next(100) <= Balance.Male);
      var listNames = SexIsMale ? validNamesM : validNamesF;
      var listNicknames = SexIsMale ? validNicknamesM : validNicknamesF;
      var used = SexIsMale ? usedNamesM : usedNamesF;

      // подбор имени
      int name, nickname;
      do
      {
        name = World.Instance.God.Next(listNames.Count);
        nickname = World.Instance.God.Next(listNicknames.Count);
      } while (used[name, nickname]);
      Name = string.Format("{0}, {1}", listNames[name], listNicknames[nickname]);
      used[name, nickname] = true;
    }

    /// <summary>
    /// Список доступных имён мужчин.
    /// </summary>
    private static readonly List<string> validNamesM = new List<string>{
      "Конрад",
      "Генрих",
      "Теодор",
      "Фридрих",
      "Карл",
      "Иоган",
      "Руперт",
      "Ганс",
      "Альбрехт",
      "Жан",
      "Жак",
      "Луи",
      "Оливер",
      "Сильвестр",
      "Арнольд",
      "Балеан",
      "Рено",
      "Иероним",
      "Александр",
      "Филипп",
      "Боб",
      "Мартин",
      "Уильям"
    };

    /// <summary>
    /// Список доступных прозвищ мужчин.
    /// </summary>
    private static readonly List<string> validNicknamesM = new List<string>{
      "урод",
      "дурак",
      "косой",
      "зазнайка",
      "плотник",
      "кузнец",
      "охотник",
      "мошенник",
      "шуллер",
      "забияка",
      "выпивоха",
      "пройдоха",
      "ветеран",
      "солдат",
      "гладиатор",
      "аскет",
      "писарь",
      "следопыт",
      "гонец",
      "дровосек",
      "водонос",
      "хромой",
      "фермер",
      "рудокоп",
      "шахтёр",
      "рыбак",
      "капитан",
      "калека"
    };

    /// <summary>
    /// Список доступных имён женщин.
    /// </summary>
    private static readonly List<string> validNamesF = new List<string>{
      "Марта",
      "Елена",
      "Виктория",
      "Клаудия",
      "Елизавта",
      "Диана",
      "Ольга",
      "Клара",
      "Нина",
      "Вероника",
      "Юлия",
      "Агриппина",
      "Зоя"
    };

    /// <summary>
    /// Список доступных прозвищ женщин.
    /// </summary>
    private static readonly List<string> validNicknamesF = new List<string>{
      "знахарка",
      "кухарка",
      "блудница",
      "языкастая",
      "ведьма",
      "вдова",
      "красавица",
      "рукоделица",
      "жнея",
      "травница",
      "швея",
      "гадалка",
      "сплетница",
      "высокая",
      "хромая"
    };
    
    /// <summary>
    /// список уже использованных имён мужчин.
    /// </summary>
    private static readonly bool[,] usedNamesM = new bool[validNamesM.Count, validNicknamesM.Count];

    /// <summary>
    /// список уже использованных имён женщин.
    /// </summary>
    private static readonly bool[,] usedNamesF = new bool[validNamesF.Count, validNicknamesF.Count];

    static Npc()
    {
      if (Balance.ActorsCount * Balance.Male / 100 * 2 > validNamesM.Count * validNicknamesM.Count)
        throw new NotImplementedException("Необходимо добавить ещё мужских имён!");
      if (Balance.ActorsCount * (100 - Balance.Male) / 100 * 2 > validNamesF.Count * validNicknamesF.Count)
        throw new NotImplementedException("Необходимо добавить ещё женских имён!");
    }
    
    #endregion

    /// <summary>
    /// Знакомство.
    /// </summary>
    public void Known()
    {
      IsKnown = true;
    }

    /// <summary>
    /// Злой обыватель.
    /// </summary>
    public bool Evil
    { get { return EquippedWeapons.Count > 0; } }
#warning Тупое свойство

    #region Взаимодействие

    /// <summary>
    /// Выбор предмета для взаимодействия.
    /// </summary>
    /// <param name="handler">объект для взаимодействия</param>
    /// <returns><b>true</b>, если предмет выбран, иначе - <b>false</b></returns>
    public override bool SelectHandler(out IInteractive handler)
    {
#warning AI пока не умеет работать с предметами!
      handler = null;
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
#warning AI пока не умеет работать выбирать взаимодействие!
			interaction = 0;
			return false;
	  }

    /// <summary>
    /// Выбор собеседника для разговора.
    /// </summary>
    /// <param name="actor">выбранный собеседник</param>
    /// <returns><b>true</b>, если предмет выбран, иначе - <b>false</b></returns>
    public override bool SelectChatPair(out Actor actor)
    {
#warning AI пока не умеет разговаривать!
      actor = null;
      return false;
    }

    /// <summary>
    /// Выбор с кем поговорить.
    /// </summary>
    /// <returns>выбранный собеседник</returns>
    public override Actor SelectChatPair()
    {
      throw new NotImplementedException("AI пока не умеет разговаривать!");
    }

    /// <summary>
    /// Разговор.
    /// </summary>
    /// <param name="pair">собеседник</param>
    public override void Chat(Actor pair)
    {
      throw new NotImplementedException("AI пока не умеет разговаривать!");
    }

    /// <summary>
    /// Выбрать пачку трав.
    /// </summary>
    /// <returns>выбранное растение</returns>
    public override HerbPack SelectMedicine()
    {
      throw new NotImplementedException("AI пока не умеет лечиться!");
    }

    /// <summary>
    /// Осмотреться.
    /// </summary>
    public override void LookAround()
    {
      throw new NotImplementedException("AI пока не умеет осматриваться!");
    }

    /// <summary>
    /// Выбрать вещь на выброс.
    /// </summary>
    /// <returns>выбранный предмет</returns>
    public override Item SelectDrop()
    {
      throw new NotImplementedException("AI пока не умеет выбрасывать вещи!");
    }

    /// <summary>
    /// Выбрать вещь на поднятие.
    /// </summary>
    /// <returns>выбранный предмет</returns>
    public override KeyValuePair<Item, Actor> SelectFromOther()
    {
      throw new NotImplementedException("AI пока не умеет мародёрствовать!");
    }

    /// <summary>
    /// Прочесть книгу.
    /// </summary>
    /// <returns></returns>
    /// <param name="book">выбранная книга</param>
    public override void ReadBook(Book book)
    {
      throw new NotImplementedException("AI пока не умеет читать!");
    }

    #endregion
  }
}
