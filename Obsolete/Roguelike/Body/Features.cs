using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Roguelike.Body
{
  /// <summary>
  /// Ролевые характеристики персонажа.
  /// </summary>
  public class Features : IFeatures
  {
    #region Свойства

    /// <summary>
    /// Сила.
    /// </summary>
    public double Strength
    { get; private set; }

    /// <summary>
    /// Ловкость.
    /// </summary>
    public double Dexterity
    { get; private set; }

    /// <summary>
    /// Интеллект.
    /// </summary>
    public double Intelligence
    { get; private set; }

    /// <summary>
    /// Сила воли.
    /// </summary>
    public double Willpower
    { get; private set; }

    /// <summary>
    /// Восприятие.
    /// </summary>
    public double Perception
    { get; private set; }

    /// <summary>
    /// Обаяние.
    /// </summary>
    public double Charisma
    { get; private set; }
    
    /// <summary>
    /// Внешность.
    /// </summary>
    public double Appearance
    { get; private set; }

    #endregion

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
    public Features(
      double strength = 0,
      double dexterity = 0,
      double intelligence = 0,
      double willpower = 0,
      double perception = 0,
      double charisma = 0,
      double appearance = 0)
    {
      Strength = strength;
      Dexterity = dexterity;
      Intelligence = intelligence;
      Willpower = willpower;
      Perception = perception;
      Charisma = charisma;
      Appearance = appearance;
    }

    #region Модификация через консоль

    /// <summary>
    /// Изменение характеристики.
    /// </summary>
    /// <param name="targetString">характеристика строкой</param>
    /// <param name="addValue">на сколько увеличить</param>
    /// <returns>результат увеличения текстом</returns>
    public string UpdateFeatureModify(string targetString, double addValue)
    {
      var target = getMapping(targetString);
      target.Setter(this, addValue);
      return string.Format(
        "Изменена характеристика ({0}) на {1}",
        target.DisplayName,
        Bonus.FormatDigit(addValue));
    }

    /// <summary>
    /// Установка характеристики.
    /// </summary>
    /// <param name="targetString">характеристика строкой</param>
    /// <param name="setValue">во сколько установить</param>
    /// <returns>результат увеличения текстом</returns>
    public string UpdateFeatureSet(string targetString, double setValue)
    {
      var target = getMapping(targetString);
      target.Updater(this, setValue);
      return string.Format(
        "Изменена характеристика ({0}) в {1}",
        target.DisplayName,
        Bonus.FormatDigit(setValue));
    }

    // карта названий и модификаторов характеристик
    private static readonly List<CharacteristicMapping> mapping = new List<CharacteristicMapping>
    {
      new CharacteristicMapping("str", "сила",
        (chars, value) => { chars.Strength += value; },
        (chars, value) => { chars.Strength = value; }),
      new CharacteristicMapping("dex", "ловкость",
        (chars, value) => { chars.Dexterity += value; },
        (chars, value) => { chars.Dexterity = value; }),
      new CharacteristicMapping("int", "интеллект",
        (chars, value) => { chars.Intelligence += value; },
        (chars, value) => { chars.Intelligence = value; }),
      new CharacteristicMapping("wil", "воля",
        (chars, value) => { chars.Willpower += value; },
        (chars, value) => { chars.Willpower = value; }),
      new CharacteristicMapping("per", "восприятие",
        (chars, value) => { chars.Perception += value; },
        (chars, value) => { chars.Perception = value; }),
      new CharacteristicMapping("app", "внешность",
        (chars, value) => { chars.Appearance += value; },
        (chars, value) => { chars.Appearance = value; }),
      new CharacteristicMapping("cha", "обаяние",
        (chars, value) => { chars.Charisma += value; },
        (chars, value) => { chars.Charisma = value; }),
    };

    // поиск в карте
    private static CharacteristicMapping getMapping(string targetString)
    {
      targetString = targetString.ToLower();
      return mapping.First(t => t.Acronym == targetString);
    }
    
    /// <summary>
    /// Служебный класс, упрощающий модификацию характеристик.
    /// </summary>
    private class CharacteristicMapping
    {
      public readonly string Acronym;
      public readonly string DisplayName;
      public readonly Action<Features, double> Updater;
      public readonly Action<Features, double> Setter;

      public CharacteristicMapping(
        string acronym, string displayName,
        Action<Features, double> updater, Action<Features, double> setter)
      {
        Acronym = acronym;
        DisplayName = displayName;
        Updater = updater;
        Setter = setter;
      }
    }

    #endregion

    #region Преобразование в строку

    /// <summary>
    /// Получение строкового описания.
    /// </summary>
    /// <returns>многострочное описание</returns>
    public StringBuilder GetText()
    {
      var result = new StringBuilder();

      result.AppendLine("сила : " + Strength);
      result.AppendLine("ловкость : " + Dexterity);
      result.AppendLine("интеллект : " + Intelligence);
      result.AppendLine("воля : " + Willpower);
      result.AppendLine("восприятие : " + Perception);
      result.AppendLine("внешность : " + Appearance);
      result.AppendLine("обаяние : " + Charisma);

      return result;
    }

		//// <summary>
    /// Получение описания бонусов.
    /// <returns>однострочное описание</returns>
    public string GetBonusLine()
    {
	    var result = new List<string>();

	    if (Strength != 0)
	    {
		    result.Add(string.Format("сила {0}, ", Bonus.FormatDigit(Strength)));
	    }
	    if (Dexterity != 0)
	    {
		    result.Add(string.Format("ловкость {0}, ", Bonus.FormatDigit(Dexterity)));
	    }
	    if (Intelligence != 0)
	    {
		    result.Add(string.Format("интеллект {0}, ", Bonus.FormatDigit(Intelligence)));
	    }
	    if (Willpower != 0)
	    {
		    result.Add(string.Format("воля {0}, ", Bonus.FormatDigit(Willpower)));
	    }
	    if (Perception != 0)
	    {
		    result.Add(string.Format("восприятие {0}, ", Bonus.FormatDigit(Perception)));
	    }
	    if (Appearance != 0)
	    {
		    result.Add(string.Format("внешность {0}, ", Bonus.FormatDigit(Appearance)));
	    }
	    if (Charisma != 0)
	    {
		    result.Add(string.Format("обаяние {0}, ", Bonus.FormatDigit(Charisma)));
	    }

	    return string.Join(", ", result);
    }

    #endregion

		/// <summary>
		/// Сложение.
		/// </summary>
		/// <param name="other">второй набор характеристик</param>
		public void Merge(Features other)
		{
			Strength += other.Strength;
			Dexterity += other.Dexterity;
			Intelligence += other.Intelligence;
			Willpower += other.Willpower;
			Perception += other.Perception;
			Charisma += other.Charisma;
			Appearance += other.Appearance;
		}
  }
}
