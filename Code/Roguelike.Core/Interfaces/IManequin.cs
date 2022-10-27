using System.Collections.Generic;

namespace Roguelike.Core.Interfaces
{
	public interface IManequin
	{
		IHeadWear HeadWear
		{ get; set; }

		IUpperBodyWear UpperBodyWear
		{ get; set; }

		ILowerBodyWear LowerBodyWear
		{ get; set; }

		ICoverWear CoverWear
		{ get; set; }

		IHandWear HandsWear
		{ get; set; }

		IFootWear FootsWear
		{ get; set; }

		ICollection<IJewelry> Jewelry
		{ get; }

		event EventHandler<IManequin> EquipmentChanged;
	}

	public static class ManequinExtensions
	{
		public static IEnumerable<IWear> GetAllItems(this IManequin manequin)
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
