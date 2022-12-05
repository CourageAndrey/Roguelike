using System.Collections.Generic;
using System.Drawing;

using Roguelike.Core.Objects;

namespace Roguelike.Core.Interfaces
{
	public interface IAlive : IActive, IRequireGravitation
	{
		bool SexIsMale
		{ get; }

		Time BirthDate
		{ get; }

		IProperties Properties
		{ get; }

		IBody Body
		{ get; }

		IState State
		{ get; }

		ICollection<IItem> Inventory
		{ get; }

		IItem WeaponToFight
		{ get; }

		bool IsAgressive
		{ get; }

		double Toughness
		{ get; }

		ActionResult Attack(IAlive target);

		ActionResult Shoot(Cell target);

		event ValueChangedEventHandler<IAlive, bool> AgressiveChanged;

		event ValueChangedEventHandler<IAlive, IItem> WeaponChanged;

		bool IsDead
		{ get; }

		string DeadReason
		{ get; }

		Corpse Die(string reason);

		event EventHandler<IAlive, string> OnDeath;

		ActionResult Backstab(IAlive actor);

		Color SkinColor
		{ get; }

		ActionResult Eat(IItem food);

		ActionResult Drink(IItem drink);
	}

	public static class AliveExtensions
	{
		public static uint GetAge(this IAlive alive, Time now)
		{
			return (uint) (now - alive.BirthDate).Year;
		}
	}
}
