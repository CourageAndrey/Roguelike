using System.Collections.Generic;
using System.Drawing;
using System.Text;

using Roguelike.Body;
using Roguelike.Language;
using Roguelike.Objects;
using Roguelike.Worlds;

using Sef.Common;

namespace Roguelike.Items
{
  /// <summary>
  /// Носимая вещь.
  /// </summary>
  public class Wear : Item
  {
    #region Свойства

    /// <summary>
    /// Класс.
    /// </summary>
    public override ItemClass Class
    {
      get { return ItemClass.Wear; }
    }

    /// <summary>
    /// Вес.
    /// </summary>
    public override double Weight
    {
      get { return weight; }
    }

    /// <summary>
    /// Базовая броня.
    /// </summary>
    public Wear Base
    { get; private set; }

    /// <summary>
    /// Базовое название.
    /// </summary>
    public Noun NameNoun
    { get; private set; }

    private readonly double weight;

    /// <summary>
    /// Кисть, которой будет вестись прорисовка.
    /// </summary>
    public Brush DrawBrush
    { get; private set; }

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
    /// <param name="brush">кисть</param>
    /// <param name="defence">бонус к защите</param>
    /// <param name="appearance">бонус к внешности</param>
    /// <param name="strength">бонус к силе</param>
    private Wear(string name, NameType nameType, double w, Brush brush, int defence = 0, int appearance = 0, int strength = 0)
    {
      NameNoun = new Noun(name, nameType);
      weight = w;
      DrawBrush = brush;

      Bonus = new Bonus(defence: defence, appearance: appearance, strength: strength);
      Base = null;
    }

    /// <summary>
    /// ctor.
    /// </summary>
    /// <param name="original">изначальная броня</param>
    /// <param name="preffix">префикс</param>
    /// <param name="postfix">постфикс</param>
    private Wear(Wear original, Affix preffix, Affix postfix)
    {
      NameNoun = original.NameNoun;
      weight = original.Weight;
      DrawBrush = original.DrawBrush;
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
    /// Доспехи.
    /// </summary>
    public static readonly Wear Armor = new Wear("доспехи", NameType.Many, 8, Brushes.DarkCyan, strength: -1, defence: 1);

    /// <summary>
    /// Одежды.
    /// </summary>
    public static readonly Wear Clothes = new Wear("одежды", NameType.Many, 2, Brushes.BlueViolet, appearance: 1);

    /// <summary>
    /// Костюм.
    /// </summary>
    public static readonly Wear Suit = new Wear("костюм", NameType.Male, 1.5, Brushes.DarkGreen, appearance: 1);

    /// <summary>
    /// Мантрия.
    /// </summary>
    public static readonly Wear Mantle = new Wear("мантия", NameType.Female, 2, Brushes.Red, appearance: 1);

    /// <summary>
    /// Наряд.
    /// </summary>
    public static readonly Wear FanceClothes = new Wear("наряд", NameType.Male, 1.5, Brushes.Orange, appearance: 1);

    /// <summary>
    /// Броня.
    /// </summary>
    public static readonly Wear Plate = new Wear("броня", NameType.Female, 8, Brushes.LightCyan, strength: -1, defence: 1);

    /// <summary>
    /// Рваньё.
    /// </summary>
    public static readonly Wear Wrappings = new Wear("рваньё", NameType.None, 8, Brushes.WhiteSmoke, appearance: -1);

    /// <summary>
    /// Кисть для прорисовки голого тела.
    /// </summary>
    public static Brush NullWearingBrush
    { get; private set; }

    static Wear()
    {
      NullWearingBrush = Brushes.Gold;
      Naked = new Wear("Снять надетое", NameType.None, 0, NullWearingBrush);
			All = Utility.ReadFields<Wear>();
    }

    /// <summary>
    /// Полный список.
    /// </summary>
    public static readonly List<Wear> All;

    #endregion
    
    #region Случайные модификаторы

    /// <summary>
    /// Создание случайного экземпляра.
    /// </summary>
    /// <returns>новый случайный экзеспляр</returns>
    public Wear ApplyRandomModifier()
    {
      Affix preffix = null, postfix = null;

      int p = World.Instance.God.Next(preffixes.Count * 2);
      if (p < preffixes.Count)
        preffix = preffixes[p];

      int s = World.Instance.God.Next(postfixes.Count * 2);
      if (s < postfixes.Count)
        postfix = postfixes[s];

      return new Wear(this, preffix, postfix);
    }

    // префиксы
    private static readonly List<Affix> preffixes = new List<Affix>{
#warning Modifier.FromWeight(new FullAdjunctive("облегчённый", "облегчённая", "облегчённое", "облегчённые"), -0.5),
#warning Modifier.FromWeight(new FullAdjunctive("утяжелённый", "утяжелённая", "утяжелённое", "утяжелённые"), +0.5),
      new Affix(new FullAdjunctive("укреплённый", "укреплённая", "укреплённое", "укреплённые"), true, defence: 1),
      new Affix(new FullAdjunctive("красивый", "красивая", "красивое", "красивые"), true, appearance: 1),
      new Affix(new FullAdjunctive("нарядный", "нарядная", "нарядное", "нарядные"), true, appearance: 1),
      new Affix(new FullAdjunctive("изящный", "изящная", "изящное", "изящные"), true, appearance: 1),
      new Affix(new FullAdjunctive("уродливый", "уродливая", "уродливое", "уродливые"), true, appearance: -1),
      new Affix(new FullAdjunctive("неряшливый", "неряшливая", "неряшливое", "неряшливые"), true, appearance: -1),
      new Affix(new FullAdjunctive("разодранный", "разодранная", "разодранное", "разодранные"), true, appearance: -1) };

    // суффиксы
    private static readonly List<Affix> postfixes = new List<Affix>{
      new Affix(new FullAdjunctive("пропускания"), false, defence: -1),
      new Affix(new FullAdjunctive("защиты"), false, defence: 1),
      new Affix(new FullAdjunctive("вора"), false, dexterity: 1),
      new Affix(new FullAdjunctive("неуклюжести"), false, dexterity: -1) };

    #endregion

    /// <summary>
    /// Получение описания вещи.
    /// </summary>
    /// <param name="actor">для персонажа</param>
    /// <returns>строковое описание</returns>
    public override string GetDescription(Actor actor)
    {
      if (actor.Properties.Intelligence >= 2)
        return string.Format("{0} {1}", NameNoun.Name, Bonus.GetDescription());
      if (actor.Properties.Intelligence >= 1)
        return NameNoun.Name;
      if (actor.Properties.Intelligence >= 0)
        return (Base != null) ? Base.NameNoun.Name : NameNoun.Name;

      return "одежда";
    }

    #region Книги

    /// <summary>
    /// Получение книги с основами.
    /// </summary>
    /// <returns>текст книги</returns>
    public static StringBuilder GetBook()
    {
      var sb = new StringBuilder();

      sb.AppendLine("Известны следующие виды снаряжения:");
      sb.AppendLine();

      foreach (var kind in All)
      {
        sb.AppendLine(kind.Name);
        sb.AppendLine("Эффект ношения: " + kind.Bonus.GetDescription());
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

      sb.AppendLine("При изменении некоторых характеристик название снаряжения получает следующие приставки:");
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

      sb.AppendLine("При изменении некоторых характеристик название снаряжения получает следующие суффиксы:");
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
    /// Пустое значение для отработки снятия одежды.
    /// </summary>
    public static Wear Naked
    { get; private set; }
  }
}
