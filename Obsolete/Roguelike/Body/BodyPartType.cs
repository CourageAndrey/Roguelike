using System.Collections.Generic;

using Sef.Common;

namespace Roguelike.Body
{
  /// <summary>
  /// Тип органа тела.
  /// </summary>
  public class BodyPartType
  {
    #region Свойства

    /// <summary>
    /// Является ли орган жизненно необходимым.
    /// </summary>
    /// <returns>true, если является</returns>
    public bool IsCritical
    { get; private set; }

    /// <summary>
    /// Сообщения об повреждении.
    /// </summary>
    public readonly List<string> DamageMessages;

    #region На какие характеристики влияет

    /// <summary>
    /// Проверка, что орган влияет на характеристику: Сила.
    /// </summary>
    public bool AffectStrength
    { get; private set; }

    /// <summary>
    /// Проверка, что орган влияет на характеристику: Ловкость.
    /// </summary>
    public bool AffectDexterity
    { get; private set; }

    /// <summary>
    /// Проверка, что орган влияет на характеристику: Интеллект.
    /// </summary>
    public bool AffectIntelligence
    { get; private set; }

    /// <summary>
    /// Проверка, что орган влияет на характеристику: Сила воли.
    /// </summary>
    public bool AffectWillpower
    { get; private set; }

    /// <summary>
    /// Проверка, что орган влияет на характеристику: Восприятие.
    /// </summary>
    public bool AffectPerception
    { get; private set; }

    /// <summary>
    /// Проверка, что орган влияет на характеристику: Обаяние.
    /// </summary>
    public bool AffectCharisma
    { get; private set; }

    /// <summary>
    /// Проверка, что орган влияет на характеристику: Внешность.
    /// </summary>
    public bool AffectAppearance
    { get; private set; }

    #endregion

    #region Для каких действий нужен

    /// <summary>
    /// Проверка, что орган влияет на способность драться.
    /// </summary>
    /// <returns>true если влияет</returns>
    public bool NeedToFight
    { get; private set; }

    /// <summary>
    /// Проверка, что орган влияет на способность ходить.
    /// </summary>
    /// <returns>true если влияет</returns>
    public bool NeedToWalk
    { get; private set; }

    /// <summary>
    /// Проверка, что орган влияет на способность плавать.
    /// </summary>
    /// <returns>true если влияет</returns>
    public bool NeedToSwim
    { get; private set; }

    /// <summary>
    /// Проверка, что орган влияет на способность летать.
    /// </summary>
    /// <returns>true если влияет</returns>
    public bool NeedToFly
    { get; private set; }

    /// <summary>
    /// Проверка, что орган влияет на способность взаимодействовать с предметами (например, с дверями).
    /// </summary>
    /// <returns>true если влияет</returns>
    public bool NeedToHandle
    { get; private set; }

    /// <summary>
    /// Проверка, что орган влияет на способность говорить.
    /// </summary>
    /// <returns>true если влияет</returns>
    public bool NeedToChat
    { get; private set; }

    /// <summary>
    /// Проверка, что орган влияет на способность менять оружие.
    /// </summary>
    /// <returns>true если влияет</returns>
    public bool NeedToChangeWeapon
    { get; private set; }

    /// <summary>
    /// Проверка, что орган влияет на способность менять одежду.
    /// </summary>
    /// <returns>true если влияет</returns>
    public bool NeedToChangeWear
    { get; private set; }

    /// <summary>
    /// Проверка, что орган влияет на способность есть.
    /// </summary>
    /// <returns>true если влияет</returns>
    public bool NeedToEat
    { get; private set; }

    /// <summary>
    /// Проверка, что орган влияет на способность выбрасывать вещи.
    /// </summary>
    /// <returns>true если влияет</returns>
    public bool NeedToDrop
    { get; private set; }

    /// <summary>
    /// Проверка, что орган влияет на способность поднимать вещи.
    /// </summary>
    /// <returns>true если влияет</returns>
    public bool NeedToPickup
    { get; private set; }

    /// <summary>
    /// Проверка, что орган влияет на способность читать.
    /// </summary>
    /// <returns>true если влияет</returns>
    public bool NeedToRead
    { get; private set; }

    /// <summary>
    /// Проверка, что орган влияет на способность осматриваться.
    /// </summary>
    /// <returns>true если влияет</returns>
    public bool NeedToLookAround
    { get; private set; }

    #endregion

    #endregion

    private BodyPartType(IEnumerable<string> damageMessages)
    {
      DamageMessages = new List<string>(damageMessages);
    }

    #region Список

    /// <summary>
    /// Рука.
    /// </summary>
    public static readonly BodyPartType Arm = new BodyPartType(new [] { "вывернул руку", "разорвал локоть", "сломал пальцы" })
    {
      AffectStrength = true,
      AffectDexterity = true,
      NeedToFight = true,
      NeedToSwim = true,
      NeedToHandle = true,
      NeedToChangeWeapon = true,
      NeedToChangeWear = true,
      NeedToDrop = true,
      NeedToPickup = true
    };

    /// <summary>
    /// Нога.
    /// </summary>
    public static readonly BodyPartType Leg = new BodyPartType(new [] { "сломал ногу", "расколол колено", "расплющил стопу" })
    {
      AffectStrength = true,
      AffectDexterity = true,
      NeedToFight = true,
      NeedToWalk = true,
      NeedToSwim = true
    };

    /// <summary>
    /// Хвост.
    /// </summary>
    public static readonly BodyPartType Tail = new BodyPartType(new [] { "оторвал хвост", "скрутил хвост в узел" })
    {
      AffectDexterity = true,
      AffectCharisma = true,
      NeedToFight = true,
      NeedToWalk = true,
      NeedToSwim = true
    };

    /// <summary>
    /// Крыло.
    /// </summary>
    public static readonly BodyPartType Wing = new BodyPartType(new[] { "оторвал крыло", "сломал крыло", "порвал крыло" })
    {
      AffectDexterity = true,
      AffectCharisma = true,
      NeedToFly = true
    };

    /// <summary>
    /// Корпус.
    /// </summary>
    public static readonly BodyPartType Body = new BodyPartType(new [] { "сломал рёбра", "выпустил кишки", "отбил печень", "отбил почки", "порвал кишки" })
    {
      AffectWillpower = true,
      IsCritical = true,
      NeedToChangeWear = true,
      NeedToEat = true
    };

    /// <summary>
    /// Корпус-хвост (у змеи, например).
    /// </summary>
    public static readonly BodyPartType TailedBody = new BodyPartType(new [] { "сломал хребет", "передавил тело" })
    {
      AffectWillpower = true,
      IsCritical = true,
      NeedToWalk = true,
      NeedToSwim = true,
      NeedToChangeWear = true,
      NeedToEat = true
    };

    /// <summary>
    /// Щупальца.
    /// </summary>
    public static readonly BodyPartType Tentacle = new BodyPartType(new [] { "оторвал щупальце", "завязал щупальце в узел" })
    {
      AffectDexterity = true,
      NeedToFight = true,
      NeedToSwim = true,
      NeedToHandle = true
    };

    /// <summary>
    /// Голова.
    /// </summary>
    public static readonly BodyPartType Head = new BodyPartType(new [] { "раскроил череп", "ударил в висок", "проломил темя" })
    {
      AffectCharisma = true,
      AffectIntelligence = true,
      AffectWillpower = true,
      IsCritical = true,
      NeedToChat = true,
      NeedToRead = true,
      NeedToLookAround = true
    };

    /// <summary>
    /// Лицо.
    /// </summary>
    public static readonly BodyPartType Face = new BodyPartType(new [] { "изуродовал лицо", "сломал челюсть", "выбил зубы" })
    {
      AffectCharisma = true,
      AffectAppearance = true,
      NeedToChat = true,
      NeedToEat = true,
      NeedToRead = true,
      NeedToLookAround = true
    };

    /// <summary>
    /// Глаз.
    /// </summary>
    public static readonly BodyPartType Eye = new BodyPartType(new [] { "выбил глаз", "вырвал глаз" })
    {
      AffectPerception = true,
      AffectAppearance = true,
      NeedToRead = true,
      NeedToLookAround = true
    };

    /// <summary>
    /// Нос.
    /// </summary>
    public static readonly BodyPartType Nose = new BodyPartType(new[] { "сломал нос", "пустил носом кровь", "вырвал ноздри" })
    {
      AffectPerception = true,
      AffectAppearance = true
    };

    /// <summary>
    /// Ухо.
    /// </summary>
    public static readonly BodyPartType Ear = new BodyPartType(new[] { "отгрыз ухо", "дал в ухо", "оторвал ухо" })
    {
      AffectPerception = true,
      AffectAppearance = true
    };

    /// <summary>
    /// Список.
    /// </summary>
    public static readonly List<BodyPartType> All;

    static BodyPartType()
    {
      // заполнение списка
			All = Utility.ReadFields<BodyPartType>();
    }

    #endregion
  }
}