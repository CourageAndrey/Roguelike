using System.Collections.Generic;

namespace Roguelike.Core.Interfaces
{
	public interface IManequin
	{
		IItem WearHead
		{ get; }

		IItem WearUpperBody
		{ get; }

		IItem WearLowerBody
		{ get; }

		IItem WearCover
		{ get; }

		IItem WearHands
		{ get; }

		IItem WearFoots
		{ get; }

		ICollection<IItem> WearNecklaces
		{ get; }

		ICollection<IItem> WearRings
		{ get; }
	}

	public static class ManequinExtensions
	{
		public static IEnumerable<IItem> GetAllIItems(this IManequin manequin)
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
