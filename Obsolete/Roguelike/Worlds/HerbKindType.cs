using System.Collections.Generic;
using System.Text;
using System.Xml.Serialization;

using Roguelike.Language;
using Roguelike.Objects;

using Sef.Common;
using Sef.Xml;

namespace Roguelike.Worlds
{
  /// <summary>
  /// Тип растения.
  /// </summary>
  public class HerbKindType
  {
    #region Свойства

    /// <summary>
    /// Название.
    /// </summary>
    public readonly string Name;

    /// <summary>
    /// Вид на карте.
    /// </summary>
    public readonly string Symbol;

    /// <summary>
    /// Список прилагательных.
    /// </summary>
    public readonly List<FullAdjunctive> Adjunctives;

    /// <summary>
    /// Список существительных.
    /// </summary>
    public readonly List<Noun> Names;

    #endregion

    /// <summary>
    /// ctor.
    /// </summary>
    /// <param name="name">название</param>
    /// <param name="symbol">символ</param>
    private HerbKindType(string name, string symbol)
    {
      Name = name;
      Symbol = symbol;
      Adjunctives = new List<FullAdjunctive>();
      Names = new List<Noun>();
    }

    #region Список

    /// <summary>
    /// Дерево.
    /// </summary>
    public static readonly HerbKindType Tree = new HerbKindType("дерево", Symbols.Tree);

    /// <summary>
    /// Кустарник.
    /// </summary>
    public static readonly HerbKindType Bush = new HerbKindType("кустарник", Symbols.Plant);

    /// <summary>
    /// Трава.
    /// </summary>
    public static readonly HerbKindType Grass = new HerbKindType("трава", Symbols.Grass);

    /// <summary>
    /// Гриб.
    /// </summary>
    public static readonly HerbKindType Mushroom = new HerbKindType("гриб", Symbols.MushroomBig);

    /// <summary>
    /// Плесень.
    /// </summary>
    public static readonly HerbKindType Fungus = new HerbKindType("плесень", Symbols.MushroomSmall);

    /// <summary>
    /// Список.
    /// </summary>
    public static readonly List<HerbKindType> All;

    static HerbKindType()
    {
      // заполнение списка
			All = Utility.ReadFields<HerbKindType>();

      // чтение имён
			var allNames = RoguelikeProgram.GetResourceFile("HerbNames.xml").DeserializeFromFile<HerbNames>();
      loadNames(Tree, allNames.HerbTree);
      loadNames(Bush, allNames.HerbBush);
      loadNames(Grass, allNames.HerbGrass);
      loadNames(Mushroom, allNames.HerbMushroom);
      loadNames(Fungus, allNames.HerbFungus);
    }

    private static void loadNames(HerbKindType type, NerbName name)
    {
      type.Names.AddRange(name.Names);
      type.Adjunctives.AddRange(name.Adjunctives);
    }

    #endregion

    #region Книги

    /// <summary>
    /// Получение книги с описанием.
    /// </summary>
    /// <returns>текст книги</returns>
    public static StringBuilder GetBook()
    {
      var sb = new StringBuilder();
      
      sb.AppendLine("Известны следующие типы растений:");
      sb.AppendLine();

      foreach (var type in All)
        sb.AppendLine(string.Format("{0} - {1}", type.Symbol, type.Name));

      sb.AppendLine();
      sb.AppendLine("Цвет же растения и его свойства зависят от конкретного вида.");

      return sb;
    }

    #endregion
  }

  #region XML-названия

  /// <summary>
  /// Список имён по типу.
  /// </summary>
  public class NerbName
  {
    #region Свойства
    
    /// <summary>
    /// Название класса.
    /// </summary>
    [XmlElement]
    public string Name
    { get; set; }

    /// <summary>
    /// Возможные имена.
    /// </summary>
    [XmlArray("Names")]
    [XmlArrayItem("Name")]
    public List<Noun> Names
    { get; set; }

    /// <summary>
    /// Вощможные прилагательные.
    /// </summary>
    [XmlArray("Adjunctives")]
    [XmlArrayItem("Adjunctive")]
    public List<FullAdjunctive> Adjunctives
    { get; set; }

    #endregion

    /// <summary>
    /// ctor.
    /// </summary>
    public NerbName()
    {
      Names = new List<Noun>();
      Adjunctives = new List<FullAdjunctive>();
    }
  }

  /// <summary>
  /// Полный список имён.
  /// </summary>
  public class HerbNames
  {
    #region Свойства

    /// <summary>
    /// Дерево.
    /// </summary>
    [XmlElement]
    public NerbName HerbTree
    { get; set; }

    /// <summary>
    /// Куст.
    /// </summary>
    [XmlElement]
    public NerbName HerbBush
    { get; set; }

    /// <summary>
    /// Трава.
    /// </summary>
    [XmlElement]
    public NerbName HerbGrass
    { get; set; }

    /// <summary>
    /// Гриб.
    /// </summary>
    [XmlElement]
    public NerbName HerbMushroom
    { get; set; }

    /// <summary>
    /// Плесень.
    /// </summary>
    [XmlElement]
    public NerbName HerbFungus
    { get; set; }

    #endregion
  }

  #endregion
}