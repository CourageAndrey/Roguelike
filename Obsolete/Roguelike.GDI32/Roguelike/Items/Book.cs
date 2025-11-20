using System.Collections.Generic;
using System.Xml.Serialization;

using Roguelike.Objects;
using Roguelike.Worlds;

using Sef.Xml;

namespace Roguelike.Items
{
  /// <summary>
  /// Книга.
  /// </summary>
  public class Book : Item
  {
    #region Свойства

    /// <summary>
    /// Класс.
    /// </summary>
    public override ItemClass Class
    {
      get { return ItemClass.Book; }
    }

    /// <summary>
    /// Вес.
    /// </summary>
    public override double Weight
    { get { return weight; } }

    /// <summary>
    /// Текст.
    /// </summary>
    public string Text
    { get; private set; }

    private readonly string name;
    private readonly double weight;

    #endregion

    #region Конструкторы

    /// <summary>
    /// ctor.
    /// </summary>
    /// <param name="n">название</param>
    /// <param name="text">текст</param>
    /// <param name="w">вес</param>
    public Book(string n, string text, double w)
    {
      name = n;
      weight = w;
      Text = text;
    }

    /// <summary>
    /// ctor.
    /// </summary>
    /// <param name="book">XML-книга</param>
    public Book(XmlBook book)
      : this(book.Name, book.Text, book.Weight)
    { }

    #endregion

    /// <summary>
    /// Получение описания вещи.
    /// </summary>
    /// <param name="actor">для персонажа</param>
    /// <returns>строковое описание</returns>
    public override string GetDescription(Actor actor)
    {
      if (actor.Properties.Intelligence >= 2)
        return string.Format("книга \"{0}\"", name);
      if (actor.Properties.Intelligence >= 1)
        return "книга";

      return "кипа бумаг";
    }

    /// <summary>
    /// Полный список.
    /// </summary>
    public static readonly List<Book> All = new List<Book>();

    static Book()
    {
      #region Чтение предзаданных книг

			var books = RoguelikeProgram.GetResourceFile("Books.xml").DeserializeFromFile<XmlBooks>();
      foreach (var book in books.Books)
        All.Add(new Book(book));

      #endregion

      #region Чтение динамических книг

      All.Add(new Book("Ботаника, том I", HerbKindType.GetBook().ToString(), getRandomWeight()));
      All.Add(new Book("Ботаника, том II", HerbKind.GetBook().ToString(), getRandomWeight()));
      All.Add(new Book("Оружие, том I", Weapon.GetBook().ToString(), getRandomWeight()));
      All.Add(new Book("Оружие, том II", Weapon.GetBookPreffixes().ToString(), getRandomWeight()));
      All.Add(new Book("Оружие, том III", Weapon.GetBookPostfixes().ToString(), getRandomWeight()));
      All.Add(new Book("Снаряжение, том I", Wear.GetBook().ToString(), getRandomWeight()));
      All.Add(new Book("Снаряжение, том II", Wear.GetBookPreffixes().ToString(), getRandomWeight()));
      All.Add(new Book("Снаряжение, том III", Wear.GetBookPostfixes().ToString(), getRandomWeight()));
      All.Add(new Book("Анатомия человека", Body.Body.CreateHumanoid().GetDescription().ToString(), getRandomWeight()));

      #endregion
    }

    // получение случайного веса для новой книги
    private static double getRandomWeight()
    {
      return World.Instance.God.Next(Balance.BookWeightMin, Balance.BookWeightMax) / 1000d;
    }
  }

  #region XML

  /// <summary>
  /// Загружаемая из XML книга.
  /// </summary>
  public class XmlBook
  {
    /// <summary>
    /// Название.
    /// </summary>
    [XmlAttribute]
    public string Name
    { get; set; }

    /// <summary>
    /// Вес.
    /// </summary>
    [XmlAttribute]
    public double Weight
    { get; set; }

    /// <summary>
    /// Текст.
    /// </summary>
    [XmlElement]
    public string Text
    { get; set; }
  }

  /// <summary>
  /// Список XML-книг.
  /// </summary>
  public class XmlBooks
  {
    /// <summary>
    /// Книги.
    /// </summary>
    [XmlArray("Books")]
    [XmlArrayItem("Book")]
    public List<XmlBook> Books
    { get; set; }

    /// <summary>
    /// ctor.
    /// </summary>
    public XmlBooks()
    {
      Books = new List<XmlBook>();
    }
  }

  #endregion
}
