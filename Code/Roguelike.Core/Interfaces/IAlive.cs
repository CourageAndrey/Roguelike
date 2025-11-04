using System.Drawing;

using Roguelike.Core.Aspects;
using Roguelike.Core.Objects;

namespace Roguelike.Core.Interfaces
{
	public interface IAlive : IActive, IVariableMassy
	{
		bool SexIsMale
		{ get; }

		Time BirthDate
		{ get; }

		Properties Properties
		{ get; }

		Body Body
		{ get; }

		State State
		{ get; }

		Inventory Inventory
		{ get; }

		Fighter Fighter
		{ get; }

		Thief Thief
		{ get; }

		double Toughness
		{ get; }

		double Speed
		{ get; }

		bool IsDead
		{ get; }

		string DeadReason
		{ get; }

		Corpse Die(string reason);

		event EventHandler<IAlive, string> OnDeath;

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
