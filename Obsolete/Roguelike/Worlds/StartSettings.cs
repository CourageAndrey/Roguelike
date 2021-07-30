using System.Linq;

namespace Roguelike.Worlds
{
  /// <summary>
  /// Стартовые настройки.
  /// </summary>
  public class StartSettings
  {
    #region Свойства

    /// <summary>
    /// Выбранное имя.
    /// </summary>
    public string Name
    { get; set; }

    /// <summary>
    /// Выбранное прозвище.
    /// </summary>
    public string Nickname
    { get; set; }

    /// <summary>
    /// Выбранный пол (мужской).
    /// </summary>
    public bool SexMale
    { get; set; }

    /// <summary>
    /// Пол строкой.
    /// </summary>
    public string SexString
    { get { return "пол : " + (SexMale ? "мужской" : "женский"); } }

    /// <summary>
    /// Стартовый бонус.
    /// </summary>
    public StartBonus Bonus
    { get; set; }

    #endregion

    /// <summary>
    /// ctor.
    /// </summary>
    public StartSettings()
    {
#if DEBUG
      Name = "Андрей";
      Nickname = "Бычко";
      Bonus = StartBonus.All.First();
#endif
      SexMale = true;
    }

    /// <summary>
    /// Проверка правильности.
    /// </summary>
    /// <param name="errorMessage">сообщение об ошибке</param>
    /// <returns>true, если всё правильно</returns>
    public bool CheckValid(out string errorMessage)
    {
      if ((string.IsNullOrEmpty(Name) || string.IsNullOrEmpty(Nickname)))
      {
        errorMessage = "Введите имя и прозвище героя!";
        return false;
      }

      if (Bonus == null)
      {
        errorMessage = "Выберите стартовый бонус!";
        return false;
      }

      errorMessage = null;
      return true;
    }
  }
}
