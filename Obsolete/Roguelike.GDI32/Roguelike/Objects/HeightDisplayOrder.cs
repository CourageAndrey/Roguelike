namespace Roguelike.Objects
{
  /// <summary>
  /// Высота объекта.
  /// </summary>
  public enum HeightDisplayOrder
  {
    /// <summary>
    /// Фоновый.
    /// </summary>
    Background = 0,

    /// <summary>
		/// Промежуточный (заслоняет фоновые, но заслоняется нормальным).
    /// </summary>
    Medium = 1,

    /// <summary>
    /// Нормальный.
    /// </summary>
    Normal = 2,
  }
}
