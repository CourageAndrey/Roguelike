using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;

using Roguelike.Body;
using Roguelike.Objects;

using Sef.Common;

namespace Roguelike.Worlds
{
  /// <summary>
  /// Вид растения.
  /// </summary>
  public class HerbKind
  {
    #region Свойства

    /// <summary>
    /// Тип.
    /// </summary>
    public readonly HerbKindType Type;

    /// <summary>
    /// Название.
    /// </summary>
    public readonly string Name;

    /// <summary>
    /// Кисть прорисовки.
    /// </summary>
    public readonly Brush Brush;

    /// <summary>
    /// Вес пачки с этим растением.
    /// </summary>
    public readonly double Weight;

    /// <summary>
    /// Как изменится здоровье.
    /// </summary>
    public readonly int HpBonus;

    /// <summary>
    /// Питательность.
    /// </summary>
    public readonly double Nutricity;

    #endregion

    /// <summary>
    /// ctor.
    /// </summary>
    /// <param name="type">тип</param>
    /// <param name="name">наименование</param>
    /// <param name="color">цвет</param>
    /// <param name="weight">вес</param>
    /// <param name="hp">здоровье</param>
    /// <param name="nutricity">питательность</param>
    private HerbKind(HerbKindType type, string name, Color color, double weight, int hp, double nutricity)
    {
      Type = type;
      Name = name;
      Brush = new SolidBrush(color);
      Weight = weight;
      HpBonus = hp;
      Nutricity = nutricity;
    }

    #region Список

    /// <summary>
    /// Список видов.
    /// </summary>
    public static readonly List<HerbKind> All = new List<HerbKind> ();

    static HerbKind()
    {
      int kinds = 0;
      foreach (var type in HerbKindType.All)
        kinds += (type.Names.Count * type.Adjunctives.Count);
      if (Balance.HerbKindCount > kinds)
        throw new ApplicationException("Недостаточно названий для указанного в балансе количества видов растений!");

      var random = World.Instance.God;
      while (All.Count < Balance.HerbKindCount)
      {
        HerbKindType type;
        string fullName;
        do
        {
					type = HerbKindType.All.GetRandom(World.Instance.God);
					var name = type.Names.GetRandom(World.Instance.God);
					var adjunctive = type.Adjunctives.GetRandom(World.Instance.God);
          fullName = string.Format("{0} {1}", name.Name, adjunctive.GetName(name.NameType));
        }
        while (All.Find(kind => kind.Name == fullName) != null);
        All.Add(new HerbKind(
          type,
          fullName,
          Color.FromArgb(random.Next(256), random.Next(256), random.Next(256)),
          random.Next(100) / 50d,
          random.Next(-10, 10),
          random.Next(-20, 20) / 10d));
      }
    }

    #endregion

    /// <summary>
    /// Получение корректного описания.
    /// </summary>
    /// <param name="actor">для персонажа</param>
    /// <returns>описание растения</returns>
    public string GetDescription(Actor actor)
    {
      // определяем питательность и ядовитость
      string healing = "?";
      string nutricious = "?";
      if (actor.Properties.Intelligence >= 3)
      {
        healing = string.Format("здоровье {0}", Bonus.FormatDigit(HpBonus));
        nutricious = string.Format("питательность {0}", Bonus.FormatDigit(Nutricity));
      }
      else if (actor.Properties.Intelligence >= 2)
      {
        healing = HpBonus >= 0 ? "лекарственное" : "ядовитое";
        nutricious = Nutricity >= 0 ? "питательное" : "слабительное";
      }

      // составляем описание
      string description = String.Format(
        "{0} ({1}, {2})",
        Name,
        healing,
        nutricious);

      // возвращаем по интеллекту
      if (actor.Properties.Intelligence >= 1)
        return description;
      if (actor.Properties.Intelligence >= 0)
        return Type.Name;
      return "неизвестное растение";
    }
    
    #region Книги

    /// <summary>
    /// Получение книги с описанием.
    /// </summary>
    /// <returns>текст книги</returns>
    public static StringBuilder GetBook()
    {
      var sb = new StringBuilder();
      
      sb.AppendLine("Известны следующие виды растений:");
      sb.AppendLine();

      foreach (var kind in All)
      {
        var brushColor = ((SolidBrush)kind.Brush).Color;
        sb.AppendLine(string.Format("Вид: {0}", kind.Name));
        sb.AppendLine(string.Format("Тип: {0}", kind.Type.Name));
        sb.AppendLine(string.Format("Влияние на здоровье: {0}", Bonus.FormatDigit(kind.HpBonus)));
        sb.AppendLine(string.Format("Питательность: {0}", Bonus.FormatDigit(kind.Nutricity)));
        sb.AppendLine(string.Format("Цвет RGB: ({0}, {1}, {2})", brushColor.R, brushColor.G, brushColor.B));
        sb.AppendLine();
      }

      return sb;
    }

    #endregion
  }
}
