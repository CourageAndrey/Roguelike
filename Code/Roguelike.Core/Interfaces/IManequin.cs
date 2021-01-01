using System.Collections.Generic;

namespace Roguelike.Core.Interfaces
{
	public interface IManequin
	{
		Item WearHead
		{ get; }

		Item WearUpperBody
		{ get; }

		Item WearLowerBody
		{ get; }

		Item WearCover
		{ get; }

		Item WearHands
		{ get; }

		Item WearFoots
		{ get; }

		ICollection<Item> WearNecklaces
		{ get; }

		ICollection<Item> WearRings
		{ get; }
	}
}
