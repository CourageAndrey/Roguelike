using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;

using Roguelike.Actions;
using Roguelike.Body;
using Roguelike.Objects;
using Roguelike.Objects.ActiveEnvironment;
using Roguelike.Objects.Interfaces;

namespace Roguelike.Worlds.Elementals
{
  /// <summary>
  /// Класс для нанесения водных повреждений.
  /// </summary>
  public sealed class WaterElemental : IActiveObject
  {
    /// <summary>
    /// Выполнить действие.
    /// </summary>
    /// <returns>сообщение для лога</returns>
    public ActionResult Do()
    {
      // создание кэшей
      var world = World.Instance;
      var messages = new List<string>();

      // ищем все объекты в реке
      var riverVictims = new Dictionary<WorldObject, Point>();
	    foreach (var river in world.GetVisibleObjects().OfType<River>())
	    {
		    var objectsInRiver = new List<WorldObject>(world.GetCell(river.Position).Objects);
		    objectsInRiver.Remove(river);
		    foreach (var o in objectsInRiver)
		    {
					riverVictims.Add(o, river.NextCell);
		    }
	    }

      // сносим течением реки
      // второй цикл нужен, чтобы один и тот же объект не проплыл все клетки реки за один ход
      foreach (var obj in riverVictims.Keys) 
      {
        world.Teleport(obj, riverVictims[obj]);
        messages.Add(string.Format(obj.Description + " сносится течением реки"));
      }

      // добавляем в список жертв воды тех, кто находится на водных клетках
      var victims = new List<Actor>(riverVictims.Keys.OfType<Actor>());
			victims.AddRange(world.GetVisibleObjects().FindAll(a => world.GetCell(a.Position).Background == CellBackground.Water).OfType<Actor>().ToList());

      // отсеиваем тех, кто может плавать
      // (условие - достаточное для плавания тело и хорошее сочетание силы и ловкости)
      var waterDamage = new Dictionary<Actor, int>();
      foreach (var actor in victims)
      {
        int possibleDamage = actor.GetModifiedDexterity() + actor.GetModifiedStrength() - 1;
        if (!actor.Body.CanSwim() && (possibleDamage < 0))
          waterDamage.Add(actor, Math.Abs(possibleDamage));
      }

      // топим тех. кому повезло меньше
      var drownedParts = new List<BodyPartType> { BodyPartType.Body, BodyPartType.TailedBody };
      foreach (var actor in waterDamage.Keys)
      {
        actor.Body.TakeDamage(waterDamage[actor], drownedParts);
        if (actor.Body.Destroyed)
          actor.Die("утонул");
        messages.Add(string.Format("{0} {1} в воде", actor.Description, actor.Body.Destroyed ? "утонул" : "тонет"));
      }

			return new ActionResult(Balance.ActionLongevityElemental, messages);
    }

    /// <summary>
    /// Время следующего действия.
    /// </summary>
    public DateTime NextActionTime
    { get; set; }
  }
}
