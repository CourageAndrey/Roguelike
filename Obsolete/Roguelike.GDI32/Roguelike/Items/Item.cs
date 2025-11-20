using Roguelike.Objects;
using Roguelike.Worlds;

namespace Roguelike.Items
{
  /// <summary>
  /// Вещь в рюкзаке.
  /// </summary>
  public abstract class Item
  {
    #region Свойства

    /// <summary>
    /// Наименование.
    /// </summary>
    public string Name
    {
      get { return GetDescription(World.Instance.Hero); }
    }

    /// <summary>
    /// Вес.
    /// </summary>
    public abstract double Weight
    { get; }
    
    /// <summary>
    /// Класс.
    /// </summary>
    public abstract ItemClass Class
    { get; }

    /// <summary>
    /// Название класса.
    /// </summary>
    public string ClassName
    { get { return Class.Name; } }

    #endregion

    /// <summary>
    /// Получение описания вещи.
    /// </summary>
    /// <param name="actor">для персонажа</param>
    /// <returns>строковое описание</returns>
    public abstract string GetDescription(Actor actor);

		/// <summary>
		/// Формат веса для вывода.
		/// </summary>
		public const string WeightFormat = "N2";
  }
}
