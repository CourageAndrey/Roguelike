namespace Roguelike.Core.Interfaces
{
	public interface IAlive : IActive
	{
		bool SexIsMale
		{ get; }

		Time Age
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

		Item WeaponToFight
		{ get; }

		bool IsAgressive
		{ get; }

		double Toughness
		{ get; }
	}
}
