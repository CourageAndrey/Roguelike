using System.Collections.Generic;
using System.Drawing;
using Roguelike.Core.Objects;

namespace Roguelike.Core.Interfaces
{
	public interface IAlive : IActive, IVariableMassy
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

		double Speed
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

		ActionResult ChangeAggressive(bool agressive);

		ActionResult ChangeWeapon(IItem weapon);

		ActionResult Backstab(IAlive actor);

		ActionResult DropItem(IItem item);

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
