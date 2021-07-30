using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;

using Roguelike.Actions;
using Roguelike.Body;
using Roguelike.Items;
using Roguelike.Objects.Interfaces;
using Roguelike.Objects.StaticEnvironment;
using Roguelike.Worlds;

namespace Roguelike.Objects
{
  /// <summary>
  /// Базовый класс живого активного объекта.
  /// </summary>
	public abstract class Actor : WorldObject, IActiveObject
	{
#warning добавить действия "поговорить", "обокрасть" и тому подобные, реализовав IInteractive
		#region Состояние смерти

		/// <summary>
    /// Действующее лицо мертво.
    /// </summary>
    public bool IsDead
    { get; private set; }

    /// <summary>
    /// Смерть.
    /// </summary>
    /// <param name="reason">причина смерти</param>
    internal void Die(string reason)
    {
      IsDead = true;
      DeadReason = reason;

	    var cell = World.Instance.GetCell(Position);
	    cell.Objects.Remove(this);
			SetPosition(cell.Position); // Чтобы герой после смерти не телепортировался куда ни попадя.
			cell.Objects.Add(new Corpse(this));
    }

    /// <summary>
    /// Причина смерти.
    /// </summary>
    public string DeadReason
    { get; internal set; }

    #endregion

    #region Бой

    /// <summary>
    /// Состояние: обнажено оружие.
    /// </summary>
    public bool IsAgressive
    { get; private set; }

    /// <summary>
    /// Переместиться в сторону.
    /// </summary>
    /// <param name="direction">направление</param>
    /// <returns>итог действия</returns>
    protected ActionResult StrafeOrHit(Direction direction)
    {
      Actor target = null;

      // проверяем, есть ли кого ударить
			if (IsAgressive)
      {
        var newPos = Position.Strafe(direction);
        foreach (var o in World.Instance.GetCell(newPos).Objects)
        {
          var actor = o as Actor;
          if ((actor != null) && (actor != this))
          {
            target = actor;
            break;
          }
        }
      }

      // пишем результат
      var result = (target == null)
        ? Actions.Action.Strafe.Call(this, direction)
        : Actions.Action.Hit.Call(this, target);

      // истекаем кровью, если надо
      if (Body.IsBleeding)
        Bleed();

      return result;
    }

    /// <summary>
    /// Атака.
    /// </summary>
    /// <param name="target">кого</param>
    /// <returns>сообщение об результате</returns>
    public string Hit(Actor target)
    {
      // наносим удар
	    var weapon = EquippedWeapons.FirstOrDefault();
			int power = (int)(Properties.Strength + (weapon != null ? weapon.Bonus.Damage : 0));
      string organMessage = target.Body.TakeDamage(power);
#warning EquippedWeapons

			// добавляем кровь
      target.Bleed();

      // составляем сообщение
      string logMessage = string.Format(
        "{0} ударил {1}{2} с силой {3} ({4}) {5}.",
        Description,
        target.Description,
				(weapon != null) ? string.Format(" (оружие - {0} +{1})", weapon.Name, weapon.Bonus.Damage) : string.Empty,
        power,
        organMessage,
        target.Body.Destroyed ? " и убил его" : string.Empty);

      // если много повреждений - убиваем цель
      if (target.Body.Destroyed)
        target.Die("скончался от полученных повреждений");

      // возвращаем сообщение
      return logMessage;
    }

    /// <summary>
    /// Истечь кровью.
    /// </summary>
    public void Bleed()
    {
      // если под нами нет ещё кровавой лужи
      if (World.Instance.GetObjectSingle<BloodPool>(Position) == null)
        // собственно, истекаем
        World.Instance.AddNewObject(new BloodPool(), Position);
    }

    #endregion

    #region Implementation of IActiveObject

    /// <summary>
    /// Выполнить действие.
    /// </summary>
    /// <returns>сообщение для лога</returns>
    public abstract ActionResult Do();

    /// <summary>
    /// Время следующего действия.
    /// </summary>
    public DateTime NextActionTime
    { get; set; }

    #endregion

    #region Implementation of WorldObject

    /// <summary>
    /// Прорисовка.
    /// </summary>
    /// <param name="canvas">поверхность рисования</param>
    /// <param name="bounds">границы</param>
    public override void Draw(Graphics canvas, Rectangle bounds)
    {
	    canvas.DrawString(
        Symbols.Face,
        Balance.DefaultFont,
				GetWearingBrush(),
        bounds);
    }

		/// <summary>
		/// Определение кисти одежды.
		/// </summary>
		/// <returns>кисть для прорисовки</returns>
	  public Brush GetWearingBrush()
	  {
			if (Cloak != null)
			{
				return Cloak.DrawBrush;
			}
			else if (BodyWear != null)
			{
				return BodyWear.DrawBrush;
			}
			else
			{
				return Wear.NullWearingBrush;
			}
	  }

    #endregion

    #region Ролевая составляющая

    /// <summary>
    /// Имя.
    /// </summary>
    public string Name
    { get; protected set; }

    /// <summary>
    /// Пол (мужской).
    /// </summary>
		public bool SexIsMale
    { get; protected set; }

    /// <summary>
    /// Возраст.
    /// </summary>
    public double Age
    { get; protected set; }

    /// <summary>
    /// Характеристики.
    /// </summary>
    public Features Properties
    { get; private set; }

    /// <summary>
    /// Тело.
    /// </summary>
    public Body.Body Body
    { get; private set; }

		#region Текущая экипировка

		/// <summary>
		/// Носимые инвентари.
		/// </summary>
		public IList<Inventory> Inventories
		{ get; private set; }

		/// <summary>
		/// Подготовленные к бою оружия.
		/// </summary>
		public IList<Weapon> EquippedWeapons
		{ get; private set; }

		/// <summary>
		/// Головной убор.
		/// </summary>
		public Wear HeadWear
		{ get; private set; }

		/// <summary>
		/// Рукавицы.
		/// </summary>
		public Wear HandWear
		{ get; private set; }

		/// <summary>
		/// Обувь.
		/// </summary>
		public Wear FootWear
		{ get; private set; }

		/// <summary>
		/// Комплект белья.
		/// </summary>
		public Wear UnderWear
		{ get; private set; }

		/// <summary>
		/// Костюм.
		/// </summary>
		public Wear BodyWear
		{ get; private set; }

		/// <summary>
		/// Плащ.
		/// </summary>
		public Wear Cloak
		{ get; private set; }

		/// <summary>
		/// Бирюльки.
		/// </summary>
		public IList<Wear> Accessories
		{ get; private set; }

		/// <summary>
		/// Одеть предмет.
		/// </summary>
		/// <param name="wear">предмет одежды</param>
	  public void WearItem(Wear wear)
		{
#warning Не так всё просто.
			BodyWear = wear;
		}

		#endregion

    #region Производные характеристики

    /// <summary>
    /// Получить силу с модификаторами.
    /// </summary>
    /// <returns>модифицированная сила</returns>
    public int GetModifiedStrength()
    {
#warning Все характеристики должны считаться в double и под конец округляться в int.
      // учёт голода
      int hungerModifier = 0;
      if (Body.IsHungred)
        hungerModifier = (int) Math.Abs(Body.Hunger) / (1 + GetModifiedWillpower());

      // учёт повреждений органов
      double damageModifier = Body.GetAgilityMinus(); 
#warning Тут похоже нужен GetStrengthMinus

      // одежда
			double wearModifier = (BodyWear != null) ? BodyWear.Bonus.Strength : 0;
#warning BodyWear вставлено вместо SelectedWear топором - но вычисление должно быть сложнее

			return (int)(Properties.Strength - hungerModifier - damageModifier + wearModifier);
    }

    /// <summary>
    /// Получить ловкость с модификаторами.
    /// </summary>
    /// <returns>модифицированная ловкость</returns>
    public int GetModifiedDexterity()
    {
      // учёт веса вещей
      double requiredStrength = Inventories.Sum(i => i.TotalWeight) / 10;
      double weightModifier = Math.Max(0, requiredStrength - GetModifiedStrength());

      // учёт повреждений органов
      double damageModifier = Body.GetAgilityMinus();

      // учёт переедания
      int bloatModifier = 0;
      if (Body.IsBloated)
        bloatModifier = (int)Body.Hunger / (1 + GetModifiedWillpower());

      // одежда
			double wearModifier = (BodyWear != null) ? BodyWear.Bonus.Strength : 0;
#warning BodyWear вставлено вместо SelectedWear топором - но вычисление должно быть сложнее
#warning Почему взят бонус силы?

      return (int)(Properties.Dexterity - weightModifier - bloatModifier - damageModifier + wearModifier);
    }

    /// <summary>
    /// Получить интеллект с модификаторами.
    /// </summary>
    /// <returns>модифицированный интеллект</returns>
    public int GetModifiedIntelligence()
    {
      return (int) Properties.Intelligence;
    }

    /// <summary>
    /// Получить волю с модификаторами.
    /// </summary>
    /// <returns>модифицированная воля</returns>
    public int GetModifiedWillpower()
    {
      return (int) Properties.Willpower;
    }

    /// <summary>
    /// Получить воспрриятие с модификаторами.
    /// </summary>
    /// <returns>модифицированное воспрриятие</returns>
    public int GetModifiedPerception()
    {
      // одежда
			double wearModifier = (BodyWear != null) ? BodyWear.Bonus.Perception : 0;
#warning BodyWear вставлено вместо SelectedWear топором - но вычисление должно быть сложнее

      return (int)(Properties.Perception + wearModifier);
    }

    /// <summary>
    /// Получить внешность с модификаторами.
    /// </summary>
    /// <returns>модифицированная внешность</returns>
    public int GetModifiedAppearance()
    {
      return (int) Properties.Appearance;
#warning Учесть надетые одёжки, атлетичность персонажа и замызганость его шмоток, состояние гигиены
    }

    /// <summary>
    /// Получить обаяние с модификаторами.
    /// </summary>
    /// <returns>модифицированное обаяние</returns>
    public int GetModifiedCharisma()
    {
      // одежда
			double wearModifier = (BodyWear != null) ? BodyWear.Bonus.Charisma : 0;
#warning BodyWear вставлено вместо SelectedWear топором - но вычисление должно быть сложнее
#warning Вот тут одежда вообще никак влиять не должна

      return (int)(Properties.Charisma + wearModifier);
    }

		/// <summary>
		/// Получить максимальный переносимый вес.
		/// </summary>
		/// <returns>вес в кг</returns>
		public int GetWeightMax()
		{
			return GetModifiedStrength()*10;
		}

		/// <summary>
		/// Получить оптимальный переносимый вес.
		/// </summary>
		/// <returns>вес в кг</returns>
		public int GetWeightOptimal()
		{
			return (int) (GetWeightMax()*0.75);
		}
#warning Возможно, все эти методы нужно сделать свойствами. главное - они не должны вызывать друг друга. А также результаты должны быть в double?
    /// <summary>
    /// Скорость выполнения действий.
    /// </summary>
    /// <remarks>описывает коэффициент, на который умножается время всех действий</remarks>
    public double Speed
    { get { return Math.Pow(Balance.SpeedOneBase, GetModifiedDexterity() - 1); } }

    /// <summary>
    /// Скорость чтения.
    /// </summary>
    /// <remarks>описывает коэффициент, на который умножается время чтения</remarks>
    public double ReadSpeed
    { get { return Math.Pow(Balance.SpeedOneBase, GetModifiedIntelligence() - 3); } }

		#endregion
		
		#endregion

		/// <summary>
    /// ctor.
    /// </summary>
    protected Actor()
    {
      Properties = new Features(
        World.Instance.God.Next(1, 2),
        World.Instance.God.Next(1, 2),
        World.Instance.God.Next(1, 2),
        World.Instance.God.Next(1, 2),
        World.Instance.God.Next(1, 2),
        World.Instance.God.Next(1, 2),
        World.Instance.God.Next(1, 2));
      Body = Roguelike.Body.Body.CreateHumanoid();
			Inventories = new List<Inventory>();
			EquippedWeapons = new List<Weapon>();
			Accessories = new List<Wear>();
    }

    #region Взаимодействие

    /// <summary>
    /// Выбор предмета для взаимодействия.
    /// </summary>
    /// <param name="handler">объект для взаимодействия</param>
    /// <returns><b>true</b>, если предмет выбран, иначе - <b>false</b></returns>
    public abstract bool SelectHandler(out IInteractive handler);

		/// <summary>
    /// Выбор предмета для взаимодействия.
		/// </summary>
		/// <param name="interactions">список возможных взаимодействий</param>
    /// <param name="interaction">вид взаимодействия</param>
    /// <returns><b>true</b>, если взаимодействие выбрано, иначе - <b>false</b></returns>
		public abstract bool SelectInteraction(IList<Interaction> interactions, out int interaction);
    
    /// <summary>
    /// Выбор собеседника для разговора.
    /// </summary>
    /// <param name="actor">выбранный собеседник</param>
    /// <returns><b>true</b>, если предмет выбран, иначе - <b>false</b></returns>
    public abstract bool SelectChatPair(out Actor actor);

    /// <summary>
    /// Выбор с кем поговорить.
    /// </summary>
    /// <returns>выбранный собеседник</returns>
    public abstract Actor SelectChatPair();

    /// <summary>
    /// Выбор травы или гриба для сбора.
    /// </summary>
    /// <returns>собранная трава или гриб</returns>
    public Herb SelectHerb()
    {
      return World.Instance.GetObjectSingle<Herb>(Position);
    }

    /// <summary>
    /// Подобрать траву или гриб под ногами.
    /// </summary>
    /// <param name="pickupable">трава или гриб</param>
    public void PickupHerb(Herb pickupable)
		{
#warning Inventories.First().Add(new HerbPack(pickupable.Kind));
#warning Inventories вставлен топором
			World.Instance.RemoveObject(pickupable);
		}

    /// <summary>
    /// Разговор.
    /// </summary>
    /// <param name="pair">собеседник</param>
    public abstract void Chat(Actor pair);

    /// <summary>
    /// Прочесть книгу.
    /// </summary>
    /// <returns></returns>
    /// <param name="book">выбранная книга</param>
    public abstract void ReadBook(Book book);

    /// <summary>
    /// Выбрать пачку трав.
    /// </summary>
    /// <returns>выбранное растение</returns>
    public abstract HerbPack SelectMedicine();

    /// <summary>
    /// Осмотреться.
    /// </summary>
    public abstract void LookAround();

    /// <summary>
    /// Съесть пачку трав.
    /// </summary>
    /// <param name="herb">травы</param>
    /// <returns>результат</returns>
    public string ApplyMedicine(HerbPack herb)
		{
#warning Inventories.First().Remove(herb);
#warning Inventories вставлен топором
			string result = Body.ApplyHerb(herb.Kind);

      // травимся, если надо
      if (Body.Destroyed)
        Die("отравился съеденным");

      // добавляем питательность
      if (Body.IsBloated)
      {
        result += " и страдает от переедания";
        Body.TakeDamage(Balance.HungerDamage);
      }
      Body.Hunger += herb.Kind.Nutricity;
      if (Body.Destroyed)
        Die("подавился во время обжорства");

      return result;
    }

    /// <summary>
    /// Выбрать вещь на выброс.
    /// </summary>
    /// <returns>выбранный предмет</returns>
    public abstract Item SelectDrop();

    /// <summary>
    /// Выбросить предмет.
    /// </summary>
    /// <param name="item">предмет</param>
    public void Drop(Item item)
		{
#warning Inventories.First().Remove(item);
#warning Inventories вставлен топором
		}

    /// <summary>
    /// Выбрать вещь на поднятие.
    /// </summary>
    /// <returns>выбранный предмет</returns>
    public abstract KeyValuePair<Item, Actor> SelectFromOther();

    /// <summary>
    /// Подобрать предмет.
    /// </summary>
    /// <param name="target">у кого</param>
    /// <param name="item">предмет</param>
    public void PickupItem(Actor target, Item item)
		{
#warning target.Inventories.First().Remove(item);
#warning Inventories вставлен топором - вот тут вообще нихера работать не будет
#warning Inventories.First().Add(item);
		}

    /// <summary>
    /// Приготовиться к бою или расслабиться.
    /// </summary>
		public ActionResult SwitchAgressive()
    {
			IsAgressive = !IsAgressive;
			return new ActionResult(
					Balance.ActionLongevityAgressive * Speed,
					Description + (IsAgressive ? " приготовился драться" : " закончил драться"));
    }

		#endregion
	}
}
