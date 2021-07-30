using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;

using Roguelike.Objects;
using Roguelike.Objects.ActiveCharacters;
using Roguelike.Worlds;

using Sef.Common;

namespace Roguelike.Body
{
  /// <summary>
  /// Тело.
  /// </summary>
  public class Body
  {
    #region Свойства

    #region Общее состояние

    /// <summary>
    /// Общее состояние: текст.
    /// </summary>
    public string TotalState
    { get { return BodyPart.GetStateDescription(HpTotalCurrent, HpTotalMax); } }

    /// <summary>
    /// Общее состояние: цвет.
    /// </summary>
    public Color TotalStateColor
    { get { return BodyPart.GetStateColor(HpTotalCurrent, HpTotalMax); } }

    /// <summary>
    /// Текущее здоровье всего тела.
    /// </summary>
    public int HpTotalCurrent
    { get { return Parts.Sum(p => p.HpCurrent); } }

    /// <summary>
    /// Максимальное здоровье всего тела.
    /// </summary>
    public int HpTotalMax
    { get { return Parts.Sum(p => p.HpMax); } }

    #endregion

    #region Признаки

    /// <summary>
    /// Состояние: голод.
    /// </summary>
    public bool IsHungred
    { get { return Hunger < -1; } }

    /// <summary>
    /// Состояние: переедание.
    /// </summary>
    public bool IsBloated
    { get { return Hunger > 1; } }

    /// <summary>
    /// Состояние: усталость.
    /// </summary>
    public bool IsTired
    { get { return false; } }

    /// <summary>
    /// Состояние: оглушение.
    /// </summary>
    public bool IsStunned
    { get { return false; } }

    /// <summary>
    /// Состояние: отравление.
    /// </summary>
    public bool IsPoisoned
    { get { return false; } }

    /// <summary>
    /// Состояние: болезнь.
    /// </summary>
    public bool IsSick
    { get { return false; } }

    /// <summary>
    /// Состояние: холод.
    /// </summary>
    public bool IsChilly
    { get { return false; } }

    /// <summary>
    /// Состояние: жара.
    /// </summary>
    public bool IsHot
    { get { return false; } }

    /// <summary>
    /// Состояние: нехватка воздуха.
    /// </summary>
    public bool IsAsphyxia
    { get { return false; } }

    /// <summary>
    /// Состояние: кровотечение.
    /// </summary>
    public bool IsBleeding
    { get { return HpTotalCurrent < HpTotalMax * 3 / 4; } }

#warning IsSick надо разбить на разные подразделы. Не хватает чесотки, кашля и расстройства желудка.

		#endregion

		#region Голод и переедание

		/// <summary>
    /// Уровень голода.
    /// </summary>
    /// <remarks>0 - всё ок.
    /// &lt;0 означает голод.
    /// &gt;0 означает переедание.
    /// Каждая единица величины (недоедание - сила воли) даёт -1 к силе, а (переедание - сила воли) -1 к ловкости.</remarks>
    public double Hunger
    { get; internal set; }

    // время последней отработки голода
    private DateTime lastHungerTime = DateTime.MinValue;

    /// <summary>
    /// Отработка голода.
    /// </summary>
    /// <param name="actor">персонаж</param>
    public string ParseHunger(Actor actor)
    {
      // если ещё не отрабатывали - помечаем
      if (lastHungerTime == DateTime.MinValue)
      {
        lastHungerTime = World.Instance.TimeStamp;
        return null;
      }

      // если день прошёл - увеличиваем голод и помечаем время
      if ((World.Instance.TimeStamp - lastHungerTime).TotalDays >= 1)
      {
        // разжигаем аппетит
        Hunger--;
        // насыщаем NPC, чтобы не добавлять в AI геморройный поиск еды
        if (actor is Npc)
          Hunger++;
        // устанавливаем время
        lastHungerTime = World.Instance.TimeStamp;
        // применяем эффекты чрезмерного голода
        if (Hunger < 0 && actor.GetModifiedStrength() < 0)
          return ApplyHunger();
      }

      return null;
    }

    #endregion

    /// <summary>
    /// Части тела и органы.
    /// </summary>
    public List<BodyPart> Parts
    { get; private set; }

    /// <summary>
    /// Тело смертельно повреждено.
    /// </summary>
    public bool Destroyed
    {
      get
      {
        bool smallHp = HpTotalMax / 2 > HpTotalCurrent;
        var criticalOrgansDestroyed = Parts.Where(p => p.Type.IsCritical && p.IsDead);
        return smallHp || criticalOrgansDestroyed.Any();
      }
    }

    #endregion

    #region Конструкторы

    private Body()
    {
      Parts = new List<BodyPart>();
    }

    /// <summary>
    /// Создание гуманоидного тела.
    /// </summary>
    /// <returns>человеческое тело</returns>
    public static Body CreateHumanoid()
    {
      var body = new Body();
      body.Parts.AddRange(new[]
      {
        new BodyPart("правая рука", BodyPartType.Arm, 1),
        new BodyPart("левая рука", BodyPartType.Arm, 1),
        new BodyPart("правая нога", BodyPartType.Leg, 1),
        new BodyPart("левая нога", BodyPartType.Leg, 1),
        new BodyPart("корпус", BodyPartType.Body, 3),
        new BodyPart("голова", BodyPartType.Head, 2),
        new BodyPart("лицо", BodyPartType.Face, 1),
        new BodyPart("правое ухо", BodyPartType.Ear, 1),
        new BodyPart("левое ухо", BodyPartType.Ear, 1),
        new BodyPart("нос", BodyPartType.Nose, 1),
        new BodyPart("правый глаз", BodyPartType.Eye, 1),
        new BodyPart("левый глаз", BodyPartType.Eye, 1)
      });
      return body;
    }

    #endregion

    #region Проверка способности к определённым действиям

    /// <summary>
    /// Есть возможность драться.
    /// </summary>
    /// <returns>true == да</returns>
    public bool CanFight()
    {
      return canMore(part => part.Type.NeedToFight);
    }

    /// <summary>
    /// Есть возможность ходить.
    /// </summary>
    /// <returns>true == да</returns>
    public bool CanWalk()
    {
      return canMore(part => part.Type.NeedToWalk);
    }

    /// <summary>
    /// Есть возможность плавать.
    /// </summary>
    /// <returns>true == да</returns>
    public bool CanSwim()
    {
      return canMore(part => part.Type.NeedToSwim);
    }

    /// <summary>
    /// Есть возможность летать.
    /// </summary>
    /// <returns>true == да</returns>
    public bool CanFly()
    {
      return canAll(part => part.Type.NeedToFly);
    }

    /// <summary>
    /// Есть возможность открывать / закрывать двери.
    /// </summary>
    /// <returns>true == да</returns>
    public bool CanHandle()
    {
      return canOne(part => part.Type.NeedToHandle);
    }

    /// <summary>
    /// Есть возможность говорить.
    /// </summary>
    /// <returns>true == да</returns>
    public bool CanChat()
    {
      return canOne(part => part.Type.NeedToChat);
    }

    /// <summary>
    /// Есть возможность изменять оружие.
    /// </summary>
    /// <returns>true == да</returns>
    public bool CanChangeWeapon()
    {
      return canAll(part => part.Type.NeedToChangeWeapon);
    }

    /// <summary>
    /// Есть возможность менять доспехи.
    /// </summary>
    /// <returns>true == да</returns>
    public bool CanChangeWear()
    {
      return canAll(part => part.Type.NeedToChangeWear);
    }

    /// <summary>
    /// Есть возможность есть травы.
    /// </summary>
    /// <returns>true == да</returns>
    public bool CanEat()
    {
      return canAll(part => part.Type.NeedToEat);
    }

    /// <summary>
    /// Есть возможность выбросить.
    /// </summary>
    /// <returns>true == да</returns>
    public bool CanDrop()
    {
      return canOne(part => part.Type.NeedToDrop);
    }

    /// <summary>
    /// Есть возможность порыться в трупе.
    /// </summary>
    /// <returns>true == да</returns>
    public bool CanPickup()
    {
      return canAll(part => part.Type.NeedToPickup);
    }

    /// <summary>
    /// Есть возможность читать.
    /// </summary>
    /// <returns>true == да</returns>
    public bool CanRead()
    {
      return canAll(part => part.Type.NeedToRead);
    }

    /// <summary>
    /// Есть возможность осматриваться.
    /// </summary>
    /// <returns>true == да</returns>
    public bool CanLookAround()
    {
      return canAll(part => part.Type.NeedToLookAround);
    }

    // проверка на >= половина
    private bool canMore(Func<BodyPart, bool> filter)
    {
      var organs = Parts.Where(filter).ToList();
      int dead = organs.Count(part => part.IsDead);
      return organs.Count >= dead * 2;
    }

    // проверка на один
    private bool canOne(Func<BodyPart, bool> filter)
    {
      var organs = Parts.Where(filter).ToList();
      return organs.Count(part => !part.IsDead) > 0;
    }

    // проверка на все
    private bool canAll(Func<BodyPart, bool> filter)
    {
      var organs = Parts.Where(filter).ToList();
      return organs.Count(part => !part.IsDead) == organs.Count;
    }

    #endregion

    /// <summary>
    /// Получение урона.
    /// </summary>
    /// <param name="damage">нанесённый урон</param>
    /// <param name="typeFilter">фильт по частям тела</param>
    /// <returns>сообщение об ударе</returns>
    public string TakeDamage(int damage = 1, IEnumerable<BodyPartType> typeFilter = null)
    {
      var organs = Parts.Where(p => p.HpCurrent > 0).ToList();
      if (typeFilter != null)
        organs.RemoveAll(p => !typeFilter.Contains(p.Type));
			var organ = organs.GetRandom(World.Instance.God);
      organ.TakeDamage(damage);

			return organ.Type.DamageMessages.GetRandom(World.Instance.God);
    }

    /// <summary>
    /// Съесть траву.
    /// </summary>
    /// <param name="herb">тип растения</param>
    /// <returns>итог</returns>
    public string ApplyHerb(HerbKind herb)
    {
      int hp = herb.HpBonus;

      var organ = (hp >= 0)
        ? Parts.Find(p => p.HpCurrent < p.HpMax)
        : Parts.Find(p => p.HpCurrent > 0);
      if ((organ == null) || (hp == 0))
        return "ничего не происходит";

      if (hp > 0)
      {
        organ.Heal(hp);
				return string.Format("{0} {1}", organ.Name, organHealMessages.GetRandom(World.Instance.God));
      }
      else
      {
        organ.TakeDamage(-hp);
				return string.Format("{0} {1}", organ.Name, organDamageMessages.GetRandom(World.Instance.God));
      }
    }

    /// <summary>
    /// Применить действие голода.
    /// </summary>
    /// <returns>итог</returns>
    public string ApplyHunger()
    {
      var organ = Parts.Find(p => p.HpCurrent > 0);
      if (organ == null)
        throw new ApplicationException("Как это повреждены все органы и мы ещё живы???");
      organ.TakeDamage(Balance.HungerDamage);
			return string.Format("{0} {1}", organ.Name, organDamageMessages.GetRandom(World.Instance.God));
    }

    // сообщения об излечении органов
    private static readonly List<string> organHealMessages = new List<string>{
      "согревается",
      "приятно покалывает",
      "теплеет" };
    // ссобщения о повреждении органов
    private static readonly List<string> organDamageMessages = new List<string> {
      "холодеет",
      "стынет",
      "отнимается",
      "дрожит",
      "леденеет" };

    /// <summary>
    /// Получения штрафа к перемещению в зависимости от ранений ног и рук.
    /// </summary>
    /// <returns>абсолютный размер минуса</returns>
    public double GetAgilityMinus()
    {
      // составляем список важных для движения органов
      return Parts.Count(part => part.Type.AffectDexterity && (part.HpCurrent <= 0));
    }

    /// <summary>
    /// Получение строкового описания.
    /// </summary>
    /// <returns>описание</returns>
    public StringBuilder GetDescription()
    {
      var sb = new StringBuilder();

      sb.AppendLine("Тело состоит из следующих частей:");
      sb.AppendLine();

      foreach (var bodyPart in Parts)
        sb.AppendLine(string.Format(
          "{0} (выдерживает {1} повреждений{2})",
          bodyPart.Name,
          bodyPart.HpMax,
          bodyPart.Type.IsCritical ? ", жизненно необходим" : string.Empty));

      return sb;
    }
  }
}
