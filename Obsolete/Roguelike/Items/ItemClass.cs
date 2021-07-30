using System.Collections.Generic;

using Sef.Common;

namespace Roguelike.Items
{
  /// <summary>
  /// Класс вещи.
  /// </summary>
  public class ItemClass
  {
    #region Свойства

    /// <summary>
    /// Название.
    /// </summary>
    public string Name
    { get; set; }

    #endregion

    #region Конструкторы

    private ItemClass(string name)
    {
      Name = name;
    }

    #endregion

    #region Список

    /// <summary>
    /// Оружие.
    /// </summary>
    public static readonly ItemClass Weapon = new ItemClass("оружие");

    /// <summary>
    /// Одежда и доспехи.
    /// </summary>
    public static readonly ItemClass Wear = new ItemClass("одежда и доспехи");

    /// <summary>
    /// Растение.
    /// </summary>
    public static readonly ItemClass Herb = new ItemClass("растение");

    /// <summary>
    /// Книга.
    /// </summary>
    public static readonly ItemClass Book = new ItemClass("книга");

    static ItemClass()
    {
			All = Utility.ReadFields<ItemClass>();
    }

    /// <summary>
    /// Полный список.
    /// </summary>
    public static readonly List<ItemClass> All;

    #endregion
  }
}