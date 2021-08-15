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

	public static class ManequinExtensions
	{
		public static IEnumerable<Item> GetAllItems(this IManequin manequin)
		{
			if (manequin.WearHead != null)
			{
				yield return manequin.WearHead;
			}

			if (manequin.WearUpperBody != null)
			{
				yield return manequin.WearUpperBody;
			}

			if (manequin.WearLowerBody != null)
			{
				yield return manequin.WearLowerBody;
			}

			if (manequin.WearCover != null)
			{
				yield return manequin.WearCover;
			}

			if (manequin.WearHands != null)
			{
				yield return manequin.WearHands;
			}

			if (manequin.WearFoots != null)
			{
				yield return manequin.WearFoots;
			}

			foreach (var necklace in manequin.WearNecklaces)
			{
				yield return necklace;
			}

			foreach (var ring in manequin.WearRings)
			{
				yield return ring;
			}
		}
	}
}
