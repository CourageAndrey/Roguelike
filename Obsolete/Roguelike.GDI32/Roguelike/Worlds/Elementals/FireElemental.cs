using System;
using System.Collections.Generic;
using System.Linq;

using Roguelike.Actions;
using Roguelike.Objects;
using Roguelike.Objects.ActiveEnvironment;
using Roguelike.Objects.Interfaces;

namespace Roguelike.Worlds.Elementals
{
  /// <summary>
  /// Класс для нанесения огненных повреждений.
  /// </summary>
  public sealed class FireElemental : IActiveObject
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

      // обжигаем наступивших в костёр
      foreach (var fire in world.GetVisibleObjects().OfType<Fire>())
				foreach (var victim in world.GetCell(fire.Position).Objects.OfType<Actor>().ToList())
        {
					victim.Body.TakeDamage(Balance.FireDamage);
          if (victim.Body.Destroyed)
            victim.Die("сгорел в пламени костра");
          messages.Add(string.Format("Костёр {0} {1}",
            victim.Body.Destroyed ? "сжигает насмерть" : "обжигает",
            victim.Description));
        }

      // обжигаем попавших в лаву вулкана
			foreach (var victim in world.GetVisibleObjects().FindAll(a => world.GetCell(a.Position).Background == CellBackground.Lava).OfType<Actor>().ToList())
      {
				victim.Body.TakeDamage(Balance.FireDamage);
        if (victim.Body.Destroyed)
          victim.Die("сгорел в лаве");
        messages.Add(string.Format("{0} {1} в лаве",
          victim.Description,
          victim.Body.Destroyed ? "сгорает насмерть" : "горит"));
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
