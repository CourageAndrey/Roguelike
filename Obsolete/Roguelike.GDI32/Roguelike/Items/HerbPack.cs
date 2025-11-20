using Roguelike.Objects;
using Roguelike.Worlds;

namespace Roguelike.Items
{
  /// <summary>
  /// Пачка трав.
  /// </summary>
  public class HerbPack : Item
  {
    #region Свойства

    /// <summary>
    /// Класс.
    /// </summary>
    public override ItemClass Class
    {
      get { return ItemClass.Herb; }
    }

    /// <summary>
    /// Вес.
    /// </summary>
    public override double Weight
    {
      get { return Kind.Weight; }
    }

    /// <summary>
    /// Вид растения.
    /// </summary>
    public HerbKind Kind
    { get; private set; }

    #endregion

    /// <summary>
    /// ctor.
    /// </summary>
    /// <param name="kind">вид растения</param>
    public HerbPack(HerbKind kind)
    {
      Kind = kind;
    }

    /// <summary>
    /// Получение описания вещи.
    /// </summary>
    /// <param name="actor">для персонажа</param>
    /// <returns>строковое описание</returns>
    public override string GetDescription(Actor actor)
    {
      return "охапка растений:" + Kind.GetDescription(actor);
    }
  }
}
