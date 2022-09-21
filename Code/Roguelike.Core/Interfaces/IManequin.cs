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

		ICollection<IJewelry> Jewelry
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

			foreach (var jewelry in manequin.Jewelry)
			{
				yield return jewelry;
			}
		}
	}
}
