using System;
using System.Drawing;

namespace Roguelike.Worlds
{
  /// <summary>
  /// Баланс игры.
  /// </summary>
  public static class Balance
  {
    #region Размер ячейки экрана

    /// <summary>
    /// Шрифт по умолчанию.
    /// </summary>
    public static readonly Font DefaultFont = new Font("Courier New", 14);

    /// <summary>
    /// Высота ячейки на экране.
    /// </summary>
    public static Size CellSize = new Size(16, 20);

    /// <summary>
    /// Кисть объектов по умолчанию.
    /// </summary>
    public static readonly Brush DefaultForeground = Brushes.White;

    /// <summary>
    /// Кисть фона по умолчанию.
    /// </summary>
    public static readonly Color DefaultBackground = Color.Black;

    /// <summary>
    /// Принудительное изменение размера ячейки экрана.
    /// </summary>
    /// <param name="size"></param>
    public static void ResizeCell(Size size)
    {
      CellSize = size;
    }

    /// <summary>
    /// Предпочтительный размер диалогов.
    /// </summary>
    public static Size DialogSize
    { get; internal set; }

    #endregion

    #region Настройки генератора карт

    /// <summary>
    /// Минимально возможный начальный год игры.
    /// </summary>
    public const int StartYearMin = 900;

    /// <summary>
    /// Максимально возможный начальный год игры.
    /// </summary>
    public const int StartYearMax = 1200;

    /// <summary>
    /// Размер мира (сколько х сколько).
    /// </summary>
    public static readonly Size WorldSize = new Size(512, 512);

    /// <summary>
    /// Угол ГСЧ при создании мира (от окружности, %).
    /// </summary>
    public const double WorldAngle = 0.01;

    /// <summary>
    /// Количество случайно создаваемых комнат.
    /// </summary>
    public const int RandomRoomCount = 40;

    /// <summary>
    /// Минимальный размер случайной комнаты.
    /// </summary>
    public const int MinRoomSize = 5;

    /// <summary>
    /// Максимальный размер случайной комнаты.
    /// </summary>
    public const int MaxRoomSize = 10;

    /// <summary>
    /// % посадки травы в данной точке.
    /// </summary>
    public const int HerbSeed = 5;

    /// <summary>
    /// Количество персонажей.
    /// </summary>
    public const int ActorsCount = 256;

    /// <summary>
    /// Количество костров.
    /// </summary>
    public const int FiresCount = 256;

    /// <summary>
    /// Максимальное количество сообщений в журнале.
    /// </summary>
    public const int MaxLogSize = 512;

    /// <summary>
    /// Дистанция, на которой разбойник начинает преследовать жертву.
    /// </summary>
    public const int EvilDistance = 10;

    /// <summary>
    /// Урон, наносимый огнём.
    /// </summary>
    public const int FireDamage = 10;

    /// <summary>
    /// Урон, наносимый голодом.
    /// </summary>
    public const int HungerDamage = 3;

    /// <summary>
    /// Вероятность того, что вновь созданный актёр будет мужчиной, %.
    /// </summary>
    public const int Male = 65;

    /// <summary>
    /// Ускорение действий на каждую единицу ловкости.
    /// </summary>
    /// <remarks>показывает, на сколько надо сократить продолжительность действия</remarks>
    public const double SpeedOneBase = 0.85;

    /// <summary>
    /// Количество видов трав.
    /// </summary>
    public const int HerbKindCount = 20;

    /// <summary>
    /// Вероятность того, что субъект будет вооружён, %.
    /// </summary>
    public const int ActorChanceWeapon = 25;

    /// <summary>
    /// Вероятность того, что у субъекта будет книга, %.
    /// </summary>
    public const int ActorChanceBook = 20;

    /// <summary>
    /// Минимальный вес книги (в граммах).
    /// </summary>
    public const int BookWeightMin = 200;

    /// <summary>
    /// Максимальный вес книги (в граммах).
    /// </summary>
    public const int BookWeightMax = 5000;

    #region Время действия

    /// <summary>
    /// Продолжительность действия по умолчанию.
    /// </summary>
    public const double ActionLongevityNull = 0;

    /// <summary>
    /// Время действия: Обнажение оружия.
    /// </summary>
    public const double ActionLongevityAgressive = 3;

    /// <summary>
    /// Время действия: Смена одежды.
    /// </summary>
    public const double ActionLongevityArmor = 120;

    /// <summary>
    /// Время действия: Разговор.
    /// </summary>
    public const double ActionLongevityChat = 20;

    /// <summary>
    /// Время действия: Открытие/закрытие двери.
    /// </summary>
    public const double ActionLongevityDoor = 3;

    /// <summary>
    /// Время действия: Сбор травы.
    /// </summary>
    public const double ActionLongevityHerb = 20;

    /// <summary>
    /// Время действия: Удар.
    /// </summary>
    public const double ActionLongevityHit = 1;

    /// <summary>
    /// Время действия: Поедание травы.
    /// </summary>
    public const double ActionLongevityMedicine = 5;

    /// <summary>
    /// Время действия: Шаг.
    /// </summary>
    public const double ActionLongevityStrafe = 1;

    /// <summary>
    /// Время действия: Ожидание.
    /// </summary>
    public const double ActionLongevityWait = 1;

    /// <summary>
    /// Время действия: Чтение книги.
    /// </summary>
    public const double ActionLongevityRead = 60;

    /// <summary>
    /// Время действия: Пауза между действиями стихий.
    /// </summary>
    public const double ActionLongevityElemental = 0.25;

    /// <summary>
    /// Время действия: Выбросить.
    /// </summary>
    public const double ActionLongevityDrop = 2;

    /// <summary>
    /// Время действия: Поднять вещь.
    /// </summary>
    public const double ActionLongevityPickup = 5;

    #endregion

    #endregion

    #region Расстояния прорисовки и ИИ

    /// <summary>
    /// Расстояние, на котором включается AI.
    /// </summary>
    /// <remarks>Учитывая ресурсоёмкость расчётов корня, используется квадрат этого расстояния.
    /// * при значении около 5х5 крайнее объекты на экране не будут двигаться и соображать
    /// * при значении около 100х100 компьютер будет невероятно тупить
    /// </remarks>
    public static int AiDistance = 64;

		/// <summary>
		/// Расстояние, на котором включается AI.
		/// </summary>
		/// <remarks>Квадрат расстояния для ускорения расчётов.</remarks>
		public static int AiDistanceSquare = AiDistance * AiDistance;

    /// <summary>
    /// Максимальное расстояние, на котором пишется лог.
    /// </summary>
    /// <remarks>используется квадрат расстояния</remarks>
    public static int LogDistanceSquare = 32 * 32;

    /// <summary>
    /// Пересчёт расстояния действия AI.
    /// </summary>
    /// <remarks>подбирает оптимальный размер, чтобы срабатывали все объекты на экране</remarks>
    /// <param name="screenSize">размер экрана</param>
    public static void RecalculateWorkDistance(Size screenSize)
    {
      int w = screenSize.Width / CellSize.Width;
      int h = screenSize.Height / CellSize.Height;
      int length = Math.Max(w, h) * 7 / 10; // Чтобы в кадр попало всё, что на экране!
      AiDistanceSquare = length * length;
      LogDistanceSquare = AiDistanceSquare / 2;
    }

    #endregion
  }
}
