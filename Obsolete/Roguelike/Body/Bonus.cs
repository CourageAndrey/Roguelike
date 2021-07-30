using System.Text;

namespace Roguelike.Body
{
  /// <summary>
  /// Прибавка к ролевым свойствам персонажа.
  /// </summary>
  public class Bonus : IBonus
  {
    #region Свойства

    /// <summary>
    /// Характеристики.
    /// </summary>
    internal Features Features
    { get; private set; }

    /// <summary>
    /// Прибавка к урону.
    /// </summary>
    public double Damage
    { get; private set; }

    /// <summary>
    /// Прибавка к защите.
    /// </summary>
    public double Defence
    { get; private set; }

    #endregion

    #region Implementation of IFeatures

    /// <summary>
    /// Сила.
    /// </summary>
    /// <remarks>Каждая единица даёт +1 к физическому урону и
    /// позволяет переносить без штрафа на 10 кг хабара больше.</remarks>
    public double Strength
    { get { return Features.Strength; } }

    /// <summary>
    /// Ловкость.
    /// </summary>
    /// <remarks>Каждая единица даёт ускорение абсолютного большинства действий на 15%
    /// (зависит от значения константы Balance.SpeedOneBase).</remarks>
    public double Dexterity
    { get { return Features.Dexterity; } }

    /// <summary>
    /// Интеллект.
    /// </summary>
    /// <remarks>Позволяет верно идентифицировать предметы (всех типов).</remarks>
    public double Intelligence
    { get { return Features.Intelligence; } }

    /// <summary>
    /// Сила воли.
    /// </summary>
    /// <remarks>Каждая 1 уменьшает действие голода и переедания.</remarks>
    public double Willpower
    { get { return Features.Willpower; } }

    /// <summary>
    /// Восприятие.
    /// </summary>
    public double Perception
    { get { return Features.Perception; } }

    /// <summary>
    /// Обаяние.
    /// </summary>
    public double Charisma
    { get { return Features.Charisma; } }

    /// <summary>
    /// Внешность.
    /// </summary>
    public double Appearance
    { get { return Features.Appearance; } }

    #endregion

    #region Конструкторы

    /// <summary>
    /// ctor.
    /// </summary>
    /// <param name="strength">сила</param>
    /// <param name="dexterity">ловкость</param>
    /// <param name="intelligence">интеллект</param>
    /// <param name="willpower">сила воли</param>
    /// <param name="perception">восприятие</param>
    /// <param name="charisma">обаяние</param>
    /// <param name="appearance">внешность</param>
    /// <param name="damage">урон</param>
    /// <param name="defence">защита</param>
    public Bonus(
      double strength = 0,
      double dexterity = 0,
      double intelligence = 0,
      double willpower = 0,
      double perception = 0,
      double charisma = 0,
      double appearance = 0,
      double damage = 0,
      double defence = 0)
    {
      Features = new Features(strength, dexterity, intelligence, willpower, perception, charisma, appearance);
      Damage = damage;
      Defence = defence;
    }

    #endregion

    /// <summary>
    /// Сложение.
    /// </summary>
    /// <param name="other">второй бонус</param>
    /// <returns>итоговый бонус</returns>
    public Bonus Merge(Bonus other)
    {
      return new Bonus(
        Strength + other.Strength,
        Dexterity + other.Dexterity,
        Intelligence + other.Intelligence,
        Willpower + other.Willpower,
        Perception + other.Perception,
        Charisma + other.Charisma,
        Appearance + other.Appearance,
        Damage + other.Damage,
        Defence + other.Defence);
    }

    #region Форматирование чисел

    /// <summary>
    /// Формат строки со значением.
    /// </summary>
    /// <param name="value">значение</param>
    /// <returns>строка</returns>
    public static string FormatDigit(long value)
    {
      string sign = (value > 0) ? "+" : string.Empty;
      return string.Format("{0}{1}", sign, value);
    }

    /// <summary>
    /// Формат строки со значением.
    /// </summary>
    /// <param name="value">значение</param>
    /// <returns>строка</returns>
    public static string FormatDigit(double value)
    {
      string sign = (value > 0) ? "+" : string.Empty;
      return string.Format("{0}{1:N1}", sign, value);
    }

    #endregion

    /// <summary>
    /// Получение развёрнутого описания.
    /// </summary>
    /// <returns>строка бонусов</returns>
    public string GetDescription()
    {
      var text = new StringBuilder();

      if (Damage != 0)
        text.AppendFormat("урон {0}, ", FormatDigit(Damage));
      if (Defence != 0)
        text.AppendFormat("защита {0}, ", FormatDigit(Defence));
      
      string dd = text.ToString();
      string parameters = Features.GetBonusLine();
      if (dd.Contains(", ") && string.IsNullOrEmpty(parameters))
        dd = dd.Remove(dd.LastIndexOf(", "));

      return dd + parameters;
    }
  }
}
