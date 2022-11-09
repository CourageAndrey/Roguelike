using System;
using System.Collections.Generic;

using Roguelike.Core.Configuration;
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

		public static void Dress(this IManequin manequin, IWear wear)
		{
			if (wear is IHeadWear)
			{
				manequin.HeadWear = wear as IHeadWear;
			}
			else if (wear is IUpperBodyWear)
			{
				manequin.UpperBodyWear = wear as IUpperBodyWear;
			}
			else if (wear is ILowerBodyWear)
			{
				manequin.LowerBodyWear = wear as ILowerBodyWear;
			}
			else if (wear is ICoverWear)
			{
				manequin.CoverWear = wear as ICoverWear;
			}
			else if (wear is IHandWear)
			{
				manequin.HandsWear = wear as IHandWear;
			}
			else if (wear is IFootWear)
			{
				manequin.FootsWear = wear as IFootWear;
			}
			else if (wear is IJewelry)
			{
				manequin.Jewelry.Add(wear as IJewelry);
			}
			else
			{
				throw new NotSupportedException();
			}
		}

		public static void Undress(this IManequin manequin, IWear wear)
		{
			if (wear is IHeadWear)
			{
				manequin.HeadWear = null;
			}
			else if (wear is IUpperBodyWear)
			{
				manequin.UpperBodyWear = null;
			}
			else if (wear is ILowerBodyWear)
			{
				manequin.LowerBodyWear = null;
			}
			else if (wear is ICoverWear)
			{
				manequin.CoverWear = null;
			}
			else if (wear is IHandWear)
			{
				manequin.HandsWear = null;
			}
			else if (wear is IFootWear)
			{
				manequin.FootsWear = null;
			}
			else if (wear is IJewelry)
			{
				manequin.Jewelry.Remove(wear as IJewelry);
			}
			else
			{
				throw new NotSupportedException();
			}
		}

		public static int GetDressTime(this IWear wear, DressTimeBalance balance)
		{
			if (wear is IHeadWear)
			{
				return balance.HeadWear;
			}
			else if (wear is IUpperBodyWear)
			{
				return balance.UpperBodyWear;
			}
			else if (wear is ILowerBodyWear)
			{
				return balance.LowerBodyWear;
			}
			else if (wear is ICoverWear)
			{
				return balance.CoverWear;
			}
			else if (wear is IHandWear)
			{
				return balance.HandsWear;
			}
			else if (wear is IFootWear)
			{
				return balance.FootsWear;
			}
			else if (wear is IJewelry)
			{
				return balance.Jewelry;
			}
			else
			{
				throw new NotSupportedException();
			}
		}
	}
}
