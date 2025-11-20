using System.Xml.Serialization;

namespace Roguelike.Language
{
  /// <summary>
  /// Имя существительное.
  /// </summary>
  public class Noun
  {
    #region Свойства

    /// <summary>
    /// Собственно, имя.
    /// </summary>
    [XmlElement]
    public string Name
    { get; set; }

    /// <summary>
    /// Род/количество.
    /// </summary>
    [XmlElement]
    public NameType NameType
    { get; set; }

    #endregion

    #region Конструкторы

    /// <summary>
    /// ctor.
    /// </summary>
    /// <param name="name">имя</param>
    /// <param name="type">род/количество</param>
    public Noun(string name, NameType type)
    {
      Name = name;
      NameType = type;
    }

    /// <summary>
    /// empty ctor.
    /// </summary>
    public Noun()
      : this(null, NameType.None)
    { }

    #endregion
  }
}
