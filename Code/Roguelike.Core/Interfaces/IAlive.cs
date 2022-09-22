namespace Roguelike.Core.Interfaces
{
	public interface IAlive : IActive
	{
		bool SexIsMale
		{ get; }

		uint Age
		{ get; }

		IProperties Properties
		{ get; }

		IBody Body
		{ get; }

		IInventory Inventory
		{ get; }

		bool IsDead
		{ get; }

		string DeadReason
		{ get; }

		IWeapon WeaponToFight
		{ get; }

		bool IsAgressive
		{ get; }

		double Toughness
		{ get; }

		ActionResult Attack(IAlive target);
	}
}
