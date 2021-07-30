using Roguelike.Language;

namespace Roguelike.Body
{
  /// <summary>
  /// Интерфейс ролевых характеристик персонажа.
  /// </summary>
  public interface IFeatures
  {
    /// <summary>
    /// Сила.
    /// </summary>
    /// <remarks>Каждая единица даёт +1 к физическому урону и
    /// позволяет переносить без штрафа на 10 кг хабара больше.</remarks>
    double Strength
    { get; }

    /// <summary>
    /// Ловкость.
    /// </summary>
    /// <remarks>Каждая единица даёт ускорение абсолютного большинства действий на 15%
    /// (зависит от значения константы Balance.SpeedOneBase).</remarks>
    double Dexterity
    { get; }

    /// <summary>
    /// Интеллект.
    /// </summary>
    /// <remarks>Позволяет верно идентифицировать предметы (всех типов).</remarks>
    double Intelligence
    { get; }

    /// <summary>
    /// Сила воли.
    /// </summary>
    /// <remarks>Каждая 1 уменьшает действие голода и переедания.</remarks>
    double Willpower
    { get; }

    /// <summary>
    /// Восприятие.
    /// </summary>
    double Perception
    { get; }

    /// <summary>
    /// Обаяние.
    /// </summary>
    double Charisma
    { get; }

    /// <summary>
    /// Внешность.
    /// </summary>
    double Appearance
    { get; }
  }
#warning Класс Bonus, класс Affix и этот файл нужно удалить нахрен.
  /// <summary>
  /// Интерфейс прибавки к ролевым свойствам персонажа.
  /// </summary>
  public interface IBonus : IFeatures
  {
    /// <summary>
    /// Прибавка к урону.
    /// </summary>
    double Damage
    { get; }

    /// <summary>
    /// Прибавка к защите.
    /// </summary>
    double Defence
    { get; }

    /// <summary>
    /// Получение развёрнутого описания.
    /// </summary>
    /// <returns>строка бонусов</returns>
    string GetDescription();
  }

  /// <summary>
  /// Интерфейс именованного бонуса.
  /// </summary>
  public interface IAffix : IBonus
  {
    /// <summary>
    /// Название.
    /// </summary>
    FullAdjunctive Name
    { get; }

    /// <summary>
    /// Помещается перед именем предмета (преффиксность).
    /// </summary>
    bool BeforeNoun
    { get; }
  }
}
