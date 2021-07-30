using System;
using System.Xml.Serialization;

namespace Roguelike.Language
{
  /// <summary>
  /// Полное прилагательное.
  /// </summary>
  public class ComplexAdjunctive
  {
    #region Свойства

    /// <summary>
    /// Мужской.
    /// </summary>
    [XmlElement]
    public string Male
    { get; set; }

    /// <summary>
    /// Женский.
    /// </summary>
    [XmlElement]
    public string Female
    { get; set; }

    /// <summary>
    /// Средний.
    /// </summary>
    [XmlElement]
    public string None
    { get; set; }

    /// <summary>
    /// Множественнное число.
    /// </summary>
    [XmlElement]
    public string Many
    { get; set; }

    /// <summary>
    /// Знечение по умолчанию.
    /// </summary>
    [XmlElement]
    public string Default
    { get; set; }

    #endregion

    #region Конструкторы

    /// <summary>
    /// ctor.
    /// </summary>
    /// <param name="male">м</param>
    /// <param name="female">ж</param>
    /// <param name="none">ср</param>
    /// <param name="many">мн</param>
    /// <param name="def">по умолчанию</param>
    private ComplexAdjunctive(string male, string female, string none, string many, string def)
    {
      Male = male;
      Female = female;
      None = none;
      Many = many;
      Default = def;
    }

    /// <summary>
    /// ctor.
    /// </summary>
    /// <param name="male">м</param>
    /// <param name="female">ж</param>
    /// <param name="none">ср</param>
    /// <param name="many">мн</param>
    public ComplexAdjunctive(string male, string female, string none, string many)
      : this(male, female, none, many, null)
    { }

    /// <summary>
    /// ctor.
    /// </summary>
    /// <param name="def">по умолчанию</param>
    public ComplexAdjunctive(string def)
      : this(null, null, null, null, def)
    { }

    /// <summary>
    /// empty ctor.
    /// </summary>
    public ComplexAdjunctive()
      : this(null, null, null, null, null)
    { }

    #endregion

    /// <summary>
    /// Получение соответствующего имени.
    /// </summary>
    /// <param name="type">род</param>
    /// <returns>имя</returns>
    public string GetName(NameType type)
    {
      // если имеется значение по-умолчанию, то род игнорируется
      if (!string.IsNullOrEmpty(Default))
        return Default;

      switch (type)
      {
        case NameType.Male:
          return Male;
        case NameType.Female:
          return Female;
        case NameType.None:
          return None;
        case NameType.Many:
          return Many;
        default:
          throw new ArgumentOutOfRangeException("type");
      }
    }
  }
}