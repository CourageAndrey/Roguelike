using System.Collections.Generic;
using System.Text;

using Roguelike.Body;
using Roguelike.Language;
using Roguelike.Objects;
using Roguelike.Worlds;

using Sef.Common;

namespace Roguelike.Items
{
  /// <summary>
  /// Оружие.
  /// </summary>
  public class Weapon : Item
  {
    #region Свойства

    /// <summary>
    /// Класс.
    /// </summary>
    public override ItemClass Class
    {
      get { return ItemClass.Weapon; }
    }

    /// <summary>
    /// Вес.
    /// </summary>
    public override double Weight
    {
      get { return weight; }
    }

    /// <summary>
    /// Базовое оружие.
    /// </summary>
    public Weapon Base
    { get; private set; }

    /// <summary>
    /// Базовое название.
    /// </summary>
    public Noun NameNoun
    { get; private set; }

    private readonly double weight;

    /// <summary>
    /// Свойства.
    /// </summary>
    public Bonus Bonus
    { get; private set; }

    #endregion

    #region Конструкторы

    /// <summary>
    /// ctor.
    /// </summary>
    /// <param name="name">название</param>
    /// <param name="nameType">род/кол-во имени</param>
    /// <param name="w">вес</param>
    /// <param name="damage">урон</param>
    private Weapon(string name, NameType nameType, double w, int damage)
    {
      NameNoun = new Noun(name, nameType);
      weight = w;

      Bonus = new Bonus(damage: damage);
      Base = null;
    }

    /// <summary>
    /// ctor.
    /// </summary>
    /// <param name="original">изначальное оружие</param>
    /// <param name="preffix">префикс</param>
    /// <param name="postfix">постфикс</param>
    private Weapon(Weapon original, Affix preffix, Affix postfix)
    {
      NameNoun = original.NameNoun;
      weight = original.Weight;
      Base = original;
      Bonus = original.Bonus;

      if (preffix != null)
      {
        Bonus = Bonus.Merge(preffix.Bonus);
        NameNoun = preffix.ModifyName(NameNoun);
      }
      if (postfix != null)
      {
        Bonus = Bonus.Merge(postfix.Bonus);
        NameNoun = postfix.ModifyName(NameNoun);
      }
    }

    #endregion

    #region Список

    /// <summary>
    /// Кастет.
    /// </summary>
    public static readonly Weapon Knuckle = new Weapon("кастет", NameType.Male, 0.1, 1);

    /// <summary>
    /// Нож.
    /// </summary>
    public static readonly Weapon Dagger = new Weapon("нож", NameType.Male, 0.4, 2);

    /// <summary>
    /// Посох.
    /// </summary>
    public static readonly Weapon Staff = new Weapon("посох", NameType.Male, 1.5, 2);

    /// <summary>
    /// Топор.
    /// </summary>
    public static readonly Weapon Axe = new Weapon("топор", NameType.Male, 3, 3);

    /// <summary>
    /// Меч.
    /// </summary>
    public static readonly Weapon Sword = new Weapon("меч", NameType.Male, 1.5, 3);

    /// <summary>
    /// Кнут.
    /// </summary>
    public static readonly Weapon Whip = new Weapon("кнут", NameType.Male, 0.3, 1);

    /// <summary>
    /// Цеп.
    /// </summary>
    public static readonly Weapon Flail = new Weapon("цеп", NameType.Male, 1.5, 3);

    /// <summary>
    /// Клевец.
    /// </summary>
    public static readonly Weapon Pickaxe = new Weapon("клевец", NameType.Male, 1, 3);

    /// <summary>
    /// Пика.
    /// </summary>
    public static readonly Weapon Pike = new Weapon("пика", NameType.Female, 3, 2);

    /// <summary>
    /// Копьё.
    /// </summary>
    public static readonly Weapon Spear = new Weapon("копьё", NameType.None, 3, 2);

    /// <summary>
    /// Алебарда.
    /// </summary>
    public static readonly Weapon Halberdine = new Weapon("алебарда", NameType.Female, 3, 2);

    static Weapon()
    {
      HandToHand = new Weapon("врукопашную", NameType.None, 0, 0);
			All = Utility.ReadFields<Weapon>();
    }

    /// <summary>
    /// Полный список.
    /// </summary>
    public static readonly List<Weapon> All;

    #endregion

    #region Случайные модификаторы

    /// <summary>
    /// Создание случайного экземпляра.
    /// </summary>
    /// <returns>новый случайный экзеспляр</returns>
    public Weapon ApplyRandomModifier()
    {
      Affix preffix = null, postfix = null;

      int p = World.Instance.God.Next(preffixes.Count * 2);
      if (p < preffixes.Count)
        preffix = preffixes[p];

      int s = World.Instance.God.Next(postfixes.Count * 2);
      if (s < postfixes.Count)
        postfix = postfixes[s];

      return new Weapon(this, preffix, postfix);
    }

    // префиксы
    private static readonly List<Affix> preffixes = new List<Affix>{
      new Affix(new FullAdjunctive("ржавый", "ржавая", "ржавое", "ржавые"), true, damage: -2),
      new Affix(new FullAdjunctive("ржавый", "ржавая", "ржавое", "ржавые"), true, damage: -2),
      new Affix(new FullAdjunctive("старый", "старая", "старое", "старые"), true, damage: -1),
      new Affix(new FullAdjunctive("обычный", "обычная", "обычное", "обычные"), true, damage: 0),
      new Affix(new FullAdjunctive("удлиннённый", "удлиннённая", "удлиннённое", "удлиннённые"), true, damage: 0),
      new Affix(new FullAdjunctive("укороченный", "укороченная", "укороченное", "укороченные"), true, damage: 0),
      new Affix(new FullAdjunctive("укреплённый", "укреплённая", "укреплённое", "укреплённые"), true, damage: +1),
      new Affix(new FullAdjunctive("хороший", "хорошая", "хорошее", "хорошие"), true, damage: +1),
      new Affix(new FullAdjunctive("качественный", "качественная", "качественное", "качественные"), true, damage: +1),
      new Affix(new FullAdjunctive("заострённый", "заострённая", "заострённое", "заострённые"), true, damage: +1),
      new Affix(new FullAdjunctive("сбалансированный", "сбалансированная", "сбалансированное", "сбалансированные"), true, damage: +2) };
#warning Modifier.FromWeight(new FullAdjunctive("облегчённый", "облегчённая", "облегчённое", "облегчённые"), -0.5),
#warning Modifier.FromWeight(new FullAdjunctive("утяжелённый", "утяжелённая", "утяжелённое", "утяжелённые"), +0.5) };

    // суффиксы
    private static readonly List<Affix> postfixes = new List<Affix>{
      new Affix(new FullAdjunctive("царапин"), false, damage: -1),
      new Affix(new FullAdjunctive("ранений"), false, damage: 0),
      new Affix(new FullAdjunctive("переломов"), false, damage: 1),
      new Affix(new FullAdjunctive("раздробления"), false, damage: 1),
      new Affix(new FullAdjunctive("убийства"), false, damage: 2),
      new Affix(new FullAdjunctive("расчленения"), false, damage: 2) };

    #endregion

    /// <summary>
    /// Получение описания вещи.
    /// </summary>
    /// <param name="actor">для персонажа</param>
    /// <returns>строковое описание</returns>
    public override string GetDescription(Actor actor)
    {
      if (actor.Properties.Intelligence >= 2)
        return string.Format("{0} ({1})", NameNoun.Name, Bonus.GetDescription());
      if (actor.Properties.Intelligence >= 1)
        return NameNoun.Name;
      if (actor.Properties.Intelligence >= 0)
        return (Base != null) ? Base.NameNoun.Name : NameNoun.Name;

      return "оружие";
    }

    #region Книги

    /// <summary>
    /// Получение книги с основами.
    /// </summary>
    /// <returns>текст книги</returns>
    public static StringBuilder GetBook()
    {
      var sb = new StringBuilder();

      sb.AppendLine("Известны следующие виды оружия:");
      sb.AppendLine();

      foreach (var kind in All)
      {
        sb.AppendLine(kind.Name);
        sb.AppendLine("Эффект применения: " + kind.Bonus.GetDescription());
        sb.AppendLine();
      }

      return sb;
    }

    /// <summary>
    /// Получение книги с префиксами.
    /// </summary>
    /// <returns>текст книги</returns>
    public static StringBuilder GetBookPreffixes()
    {
      var sb = new StringBuilder();

      sb.AppendLine("При изменении некоторых характеристик название оружия получает следующие приставки:");
      sb.AppendLine();

      foreach (var preffix in preffixes)
      {
        sb.AppendLine(preffix.Name.GetName(NameType.Male));
        sb.AppendLine("эффект: " + preffix.GetDescription());
        sb.AppendLine();
      }

      return sb;
    }

    /// <summary>
    /// Получение книги с постфиксами.
    /// </summary>
    /// <returns>текст книги</returns>
    public static StringBuilder GetBookPostfixes()
    {
      var sb = new StringBuilder();

      sb.AppendLine("При изменении некоторых характеристик название оружия получает следующие суффиксы:");
      sb.AppendLine();

      foreach (var postfix in postfixes)
      {
        sb.AppendLine(postfix.Name.GetName(NameType.None));
        sb.AppendLine("эффект: " + postfix.GetDescription());
        sb.AppendLine();
      }

      return sb;
    }

    #endregion

    /// <summary>
    /// Пустое значение для получения строк бонусов и отработки рукопашного боя.
    /// </summary>
    public static Weapon HandToHand
    { get; private set; }
  }
}
