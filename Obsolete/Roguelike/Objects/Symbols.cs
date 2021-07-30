namespace Roguelike.Objects
{
  /// <summary>
  /// Список используемых символов на экране.
  /// </summary>
  public static class Symbols
  {
    #region Персонажи

    /// <summary>
    /// Лицо.
    /// </summary>
    public const string Face = "☻";

    /// <summary>
    /// Лицо трупа.
    /// </summary>
    public const string DeadFace = "☺";

    #endregion

    #region Двери

    /// <summary>
    /// Закрытая дверь.
    /// </summary>
    public const string DoorClosed = "#";

    /// <summary>
    /// Открытая дверь.
    /// </summary>
    public const string DoorOpened = "|";

    #endregion

    #region Растения

    /// <summary>
    /// Дерево.
    /// </summary>
    public const string Tree = ";";

    /// <summary>
    /// Кустарник.
    /// </summary>
    public const string Plant = ":";

    /// <summary>
    /// Трава.
    /// </summary>
    public const string Grass = "\"";

    /// <summary>
    /// Гриб.
    /// </summary>
    public const string MushroomBig = ",";

    /// <summary>
    /// Плесень.
    /// </summary>
    public const string MushroomSmall = ".";

    #endregion

    #region Прочие

    /// <summary>
    /// Костёр.
    /// </summary>
    public const string Fire = "*";

    /// <summary>
    /// Река.
    /// </summary>
    public const string River = "~";

    /// <summary>
    /// Стена.
    /// </summary>
    public const string Wall = "▓";

	  /// <summary>
	  /// Лужа.
	  /// </summary>
	  public const string Pool = "0";

	  #endregion
  }
}
