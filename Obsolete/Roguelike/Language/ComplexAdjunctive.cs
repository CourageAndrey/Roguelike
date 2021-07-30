using System;
using System.Xml.Serialization;

namespace Roguelike.Language
{
  /// <summary>
  /// ������ ��������������.
  /// </summary>
  public class ComplexAdjunctive
  {
    #region ��������

    /// <summary>
    /// �������.
    /// </summary>
    [XmlElement]
    public string Male
    { get; set; }

    /// <summary>
    /// �������.
    /// </summary>
    [XmlElement]
    public string Female
    { get; set; }

    /// <summary>
    /// �������.
    /// </summary>
    [XmlElement]
    public string None
    { get; set; }

    /// <summary>
    /// �������������� �����.
    /// </summary>
    [XmlElement]
    public string Many
    { get; set; }

    /// <summary>
    /// �������� �� ���������.
    /// </summary>
    [XmlElement]
    public string Default
    { get; set; }

    #endregion

    #region ������������

    /// <summary>
    /// ctor.
    /// </summary>
    /// <param name="male">�</param>
    /// <param name="female">�</param>
    /// <param name="none">��</param>
    /// <param name="many">��</param>
    /// <param name="def">�� ���������</param>
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
    /// <param name="male">�</param>
    /// <param name="female">�</param>
    /// <param name="none">��</param>
    /// <param name="many">��</param>
    public ComplexAdjunctive(string male, string female, string none, string many)
      : this(male, female, none, many, null)
    { }

    /// <summary>
    /// ctor.
    /// </summary>
    /// <param name="def">�� ���������</param>
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
    /// ��������� ���������������� �����.
    /// </summary>
    /// <param name="type">���</param>
    /// <returns>���</returns>
    public string GetName(NameType type)
    {
      // ���� ������� �������� ��-���������, �� ��� ������������
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