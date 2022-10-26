using Roguelike.Core.StaticObjects;

using System.Collections.Generic;

namespace Roguelike.Core.Interfaces
{
	public interface IAlive : IActive, IRequireGravitation
	{
		bool SexIsMale
		{ get; }

		uint Age
		{ get; }

		IProperties Properties
		{ get; }

		IBody Body
		{ get; }

		IState State
		{ get; }

		ICollection<IItem> Inventory
		{ get; }

		IWeapon WeaponToFight
		{ get; }

		bool IsAgressive
		{ get; }

		double Toughness
		{ get; }

		ActionResult Attack(IAlive target);

		event ValueChangedEventHandler<IAlive, bool> AgressiveChanged;

		event ValueChangedEventHandler<IAlive, IWeapon> WeaponChanged;

		bool IsDead
		{ get; }

		string DeadReason
		{ get; }

		Corpse Die(string reason);

		event EventHandler<IAlive, string> OnDeath;

		void Backstab(IAlive actor);
	}
}
