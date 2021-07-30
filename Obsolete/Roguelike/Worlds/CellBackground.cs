using System.Collections.Generic;
using System.Drawing;
using Sef.Common;

namespace Roguelike.Worlds
{
  /// <summary>
  /// Фон ячейки карты.
  /// </summary>
  public class CellBackground
  {
    #region Свойства

    /// <summary>
    /// Описание.
    /// </summary>
    public string Description
    { get; private set; }

    /// <summary>
    /// Кисть, которой прорисовывается.
    /// </summary>
    public Brush Brush
    { get; private set; }

    /// <summary>
    /// На клетках могут расти растения.
    /// </summary>
    public bool IsSoil
    { get; private set; }

    /// <summary>
    /// Клетка может быть стартовой точкой.
    /// </summary>
    public bool IsStartPoint
    { get; private set; }

    /// <summary>
    /// Изначально опасная клетка.
    /// </summary>
    public bool IsDangerous
    { get; private set; }

    #endregion

    /// <summary>
    /// ctor.
    /// </summary>
    /// <param name="description">описание</param>
    /// <param name="brush">кисть прорисовки</param>
    private CellBackground(string description, Brush brush)
    {
      Description = description;
      Brush = brush;
    }

    #region Список

    /// <summary>
    /// Снег.
    /// </summary>
    public static readonly CellBackground Snow = new CellBackground("снег", Brushes.White);

    /// <summary>
    /// Вода.
    /// </summary>
    public static readonly CellBackground Water = new CellBackground("вода", Brushes.Navy) { IsDangerous = true };

    /// <summary>
    /// Камни.
    /// </summary>
    public static readonly CellBackground Rock = new CellBackground("камень", Brushes.Silver);

    /// <summary>
    /// Земля.
    /// </summary>
    public static readonly CellBackground Ground = new CellBackground("земля", Brushes.SaddleBrown) { IsSoil = true, IsStartPoint = true };

    /// <summary>
    /// Песок.
    /// </summary>
    public static readonly CellBackground Sand = new CellBackground("песок", Brushes.Yellow) { IsStartPoint = true };

    /// <summary>
    /// Трава.
    /// </summary>
    public static readonly CellBackground Grass = new CellBackground("трава", Brushes.Lime) { IsSoil = true, IsStartPoint = true };

    /// <summary>
    /// Трава.
    /// </summary>
    public static readonly CellBackground Lava = new CellBackground("лава", Brushes.DarkRed) { IsDangerous = true };

    /// <summary>
    /// Трава.
    /// </summary>
    public static readonly CellBackground Floor = new CellBackground("пол", Brushes.Gray) { IsStartPoint = true };

    /// <summary>
    /// # Фон отсутствует #
    /// </summary>
    /// <remarks>значение фона по умолчанию</remarks>
    public static readonly CellBackground Default = new CellBackground(string.Empty, Water.Brush);

    #endregion

    /// <summary>
    /// Полный список.
    /// </summary>
    public static readonly List<CellBackground> All;

    static CellBackground()
    {
			All = Utility.ReadFields<CellBackground>();
      All.Remove(Default);
    }
  }
}
