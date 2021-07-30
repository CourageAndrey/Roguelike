using System;
using System.Collections.Generic;
using System.Drawing;

namespace Roguelike.Body
{
  /// <summary>
  /// Часть тела.
  /// </summary>
  public class BodyPart
  {
    #region Свойства

    /// <summary>
    /// Название.
    /// </summary>
    public string Name
    { get; private set; }

    /// <summary>
    /// Тип.
    /// </summary>
    public BodyPartType Type
    { get; private set; }

    /// <summary>
    /// Текущее здоровье.
    /// </summary>
    public int HpCurrent
    { get; private set; }

    /// <summary>
    /// Максимальное здоровье.
    /// </summary>
    public int HpMax
    { get; private set; }

    /// <summary>
    /// Проверка, является ли орган нефункциональным.
    /// </summary>
    public bool IsDead
    { get { return HpCurrent <= 0; } }

    /// <summary>
    /// Цвет состояния.
    /// </summary>
    public Color StateColor
    { get { return GetStateColor(HpCurrent, HpMax); } }

    /// <summary>
    /// Описание состояния.
    /// </summary>
    public string StateDescription
    { get { return GetStateDescription(HpCurrent, HpMax); } }

    #endregion

    /// <summary>
    /// ctor.
    /// </summary>
    /// <param name="name">название</param>
    /// <param name="type"></param>
    /// <param name="hpMax">здоровье</param>
    /// <param name="hp">текущее здоровье</param>
    public BodyPart(string name, BodyPartType type, int hpMax, int? hp = null)
    {
      Name = name;
      HpMax = hpMax;
      HpCurrent = hp ?? hpMax;
      Type = type;
    }

    /// <summary>
    /// Returns a <see cref="T:System.String"/> that represents the current <see cref="T:System.Object"/>.
    /// </summary>
    /// <returns>
    /// A <see cref="T:System.String"/> that represents the current <see cref="T:System.Object"/>.
    /// </returns>
    /// <filterpriority>2</filterpriority>
    public override string ToString()
    {
      return string.Format("{0} : {1}", Name, StateDescription);
    }

    /// <summary>
    /// Получение урона.
    /// </summary>
    /// <param name="damage">нанесённый урон</param>
    public void TakeDamage(int damage = 1)
    {
      HpCurrent = Math.Max(HpCurrent - damage, 0);
    }

    /// <summary>
    /// Восстановить здоровье.
    /// </summary>
    /// <param name="hp">количество восстанавливаемых очков</param>
    public void Heal(int hp = 1)
    {
      HpCurrent = Math.Min(HpCurrent + hp, HpMax);
    }

    #region Цвета состояния

    /// <summary>
    /// Определение цвета состояния.
    /// </summary>
    /// <param name="hpCurrent">текущий показатель</param>
    /// <param name="hpMax">максимальный показатель</param>
    /// <returns>цвет по шкале "красный-зелёный"</returns>
    public static Color GetStateColor(int hpCurrent, int hpMax)
    {
      double state = (double) hpCurrent / hpMax;
      return colorsAll[(byte)(Math.Max(0, colorsAll.Count - 1) * state)];
    }

    /// <summary>
    /// Определение описания состояния состояния.
    /// </summary>
    /// <param name="hpCurrent">текущий показатель</param>
    /// <param name="hpMax">максимальный показатель</param>
    /// <returns>строковое описание "повреждено/в норме"</returns>
    public static string GetStateDescription(int hpCurrent, int hpMax)
    {
      return (hpCurrent < hpMax) ? "повреждено" : "в норме";
    }

    // список возможных цветов
    private static readonly List<Color> colorsAll = new List<Color> {
      Color.Red,
      Color.OrangeRed,
      Color.Orange,
      Color.Yellow,
      Color.GreenYellow,
      Color.Lime,
      Color.Green 
    };

    #endregion
  }
}
