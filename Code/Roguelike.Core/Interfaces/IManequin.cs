using System.Collections.Generic;

namespace Roguelike.Core.Interfaces
{
	public interface IManequin
	{
		IHeadWear HeadWear
		{ get; }

		IUpperBodyWear UpperBodyWear
		{ get; }

		ILowerBodyWear LowerBodyWear
		{ get; }

		ICoverWear CoverWear
		{ get; }

		IHandWear HandsWear
		{ get; }

		IFootWear FootsWear
		{ get; }

		ICollection<INecklace> Necklaces
		{ get; }

		ICollection<IRing> Rings
		{ get; }
	}

	public static class ManequinExtensions
	{
		public static IEnumerable<IItem> GetAllIItems(this IManequin manequin)
		{
			if (manequin.HeadWear != null)
			{
				yield return manequin.HeadWear;
			}

			if (manequin.UpperBodyWear != null)
			{
				yield return manequin.UpperBodyWear;
			}

			if (manequin.LowerBodyWear != null)
			{
				yield return manequin.LowerBodyWear;
			}

			if (manequin.CoverWear != null)
			{
				yield return manequin.CoverWear;
			}

			if (manequin.HandsWear != null)
			{
				yield return manequin.HandsWear;
			}

			if (manequin.FootsWear != null)
			{
				yield return manequin.FootsWear;
			}

			foreach (var necklace in manequin.Necklaces)
			{
				yield return necklace;
			}

			foreach (var ring in manequin.Rings)
			{
				yield return ring;
			}
		}
	}
}
