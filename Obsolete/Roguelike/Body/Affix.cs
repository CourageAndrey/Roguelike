using Roguelike.Language;

namespace Roguelike.Body
{
  /// <summary>
  /// Именованный бонус.
  /// </summary>
  public class Affix : IAffix
  {
    #region Свойства

    /// <summary>
    /// Бонус.
    /// </summary>
    internal Bonus Bonus
    { get; private set; }

    /// <summary>
    /// Название.
    /// </summary>
    public FullAdjunctive Name
    { get; private set; }

    /// <summary>
    /// Помещается перед именем предмета (преффиксность).
    /// </summary>
    public bool BeforeNoun
    { get; private set; }

    #endregion

    #region Implementation of IBonus

    /// <summary>
    /// Сила.
    /// </summary>
    /// <remarks>Каждая единица даёт +1 к физическому урону и
    /// позволяет переносить без штрафа на 10 кг хабара больше.</remarks>
    public double Strength
    { get { return Bonus.Strength; } }

    /// <summary>
    /// Ловкость.
    /// </summary>
    /// <remarks>Каждая единица даёт ускорение абсолютного большинства действий на 15%
    /// (зависит от значения константы Balance.SpeedOneBase).</remarks>
    public double Dexterity
    { get { return Bonus.Dexterity; } }

    /// <summary>
    /// Интеллект.
    /// </summary>
    /// <remarks>Позволяет верно идентифицировать предметы (всех типов).</remarks>
    public double Intelligence
    { get { return Bonus.Intelligence; } }

    /// <summary>
    /// Сила воли.
    /// </summary>
    /// <remarks>Каждая 1 уменьшает действие голода и переедания.</remarks>
    public double Willpower
    { get { return Bonus.Willpower; } }

    /// <summary>
    /// Восприятие.
    /// </summary>
    public double Perception
    { get { return Bonus.Perception; } }

    /// <summary>
    /// Обаяние.
    /// </summary>
    public double Charisma
    { get { return Bonus.Charisma; } }

    /// <summary>
    /// Внешность.
    /// </summary>
    public double Appearance
    { get { return Bonus.Appearance; } }

    /// <summary>
    /// Прибавка к урону.
    /// </summary>
    public double Damage
    { get { return Bonus.Appearance; } }

    /// <summary>
    /// Прибавка к защите.
    /// </summary>
    public double Defence
    { get { return Bonus.Appearance; } }

    /// <summary>
    /// Получение развёрнутого описания.
    /// </summary>
    /// <returns>строка бонусов</returns>
    public string GetDescription()
    {
      return Bonus.GetDescription();
    }

    #endregion

		/// <summary>
    /// ctor.
    /// </summary>
    /// <param name="name">название</param>
    /// <param name="beforeNoun">префикс</param>
    /// <param name="strength">сила</param>
    /// <param name="dexterity">ловкость</param>
    /// <param name="intelligence">интеллект</param>
    /// <param name="willpower">сила воли</param>
    /// <param name="perception">восприятие</param>
    /// <param name="charisma">обаяние</param>
    /// <param name="appearance">внешность</param>
    /// <param name="damage">урон</param>
    /// <param name="defence">защита</param>
    public Affix(
      FullAdjunctive name, bool beforeNoun,
      double strength = 0,
      double dexterity = 0,
      double intelligence = 0,
      double willpower = 0,
      double perception = 0,
      double appearance = 0,
      double charisma = 0,
      double damage = 0,
      double defence = 0)
    {
      Bonus = new Bonus(strength, dexterity, intelligence, willpower, perception, charisma, appearance, damage, defence);
      Name = name;
      BeforeNoun = beforeNoun;
    }

		/// <summary>
    /// Изменение имени в соответствии с аффиксом.
    /// </summary>
    /// <param name="original">оригинальное имя</param>
    /// <returns>изменённое имя</returns>
    public Noun ModifyName(Noun original)
    {
      return new Noun(
        string.Format(BeforeNoun ? "{0} {1}" : "{1} {0}", Name.GetName(original.NameType), original.Name),
        original.NameType);
    }
  }
}
