using System.Collections.Generic;

using Roguelike.Core.Items;

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
			if (!(manequin.HeadWear is Naked))
			{
				yield return manequin.HeadWear;
			}

			if (!(manequin.UpperBodyWear is Naked))
			{
				yield return manequin.UpperBodyWear;
			}

			if (!(manequin.LowerBodyWear is Naked))
			{
				yield return manequin.LowerBodyWear;
			}

			if (!(manequin.CoverWear is Naked))
			{
				yield return manequin.CoverWear;
			}

			if (!(manequin.HandsWear is Naked))
			{
				yield return manequin.HandsWear;
			}

			if (!(manequin.FootsWear is Naked))
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
