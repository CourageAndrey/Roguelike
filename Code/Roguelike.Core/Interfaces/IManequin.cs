using System;
using System.Collections.Generic;

using Roguelike.Core.Configuration;
using Roguelike.Core.Items;

namespace Roguelike.Core.Interfaces
{
	public interface IManequin
	{
		IWear HeadWear
		{ get; set; }

		IWear UpperBodyWear
		{ get; set; }

		IWear LowerBodyWear
		{ get; set; }

		IWear CoverWear
		{ get; set; }

		IWear HandsWear
		{ get; set; }

		IWear FootsWear
		{ get; set; }

		ICollection<IWear> Jewelry
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
			if (wear.SuitableSlot == WearSlot.Head)
			{
				manequin.HeadWear = wear;
			}
			else if (wear.SuitableSlot == WearSlot.UpperBody)
			{
				manequin.UpperBodyWear = wear;
			}
			else if (wear.SuitableSlot == WearSlot.LowerBody)
			{
				manequin.LowerBodyWear = wear;
			}
			else if (wear.SuitableSlot == WearSlot.Cover)
			{
				manequin.CoverWear = wear;
			}
			else if (wear.SuitableSlot == WearSlot.Hands)
			{
				manequin.HandsWear = wear;
			}
			else if (wear.SuitableSlot == WearSlot.Foots)
			{
				manequin.FootsWear = wear;
			}
			else if (wear.SuitableSlot == WearSlot.Jewelry)
			{
				manequin.Jewelry.Add(wear);
			}
			else
			{
				throw new NotSupportedException();
			}
		}

		public static void Undress(this IManequin manequin, IWear wear)
		{
			if (wear.SuitableSlot == WearSlot.Head)
			{
				manequin.HeadWear = null;
			}
			else if (wear.SuitableSlot == WearSlot.UpperBody)
			{
				manequin.UpperBodyWear = null;
			}
			else if (wear.SuitableSlot == WearSlot.LowerBody)
			{
				manequin.LowerBodyWear = null;
			}
			else if (wear.SuitableSlot == WearSlot.Cover)
			{
				manequin.CoverWear = null;
			}
			else if (wear.SuitableSlot == WearSlot.Hands)
			{
				manequin.HandsWear = null;
			}
			else if (wear.SuitableSlot == WearSlot.Foots)
			{
				manequin.FootsWear = null;
			}
			else if (wear.SuitableSlot == WearSlot.Jewelry)
			{
				manequin.Jewelry.Remove(wear);
			}
			else
			{
				throw new NotSupportedException();
			}
		}

		public static int GetDressTime(this IWear wear, DressTimeBalance balance)
		{
			if (wear.SuitableSlot == WearSlot.Head)
			{
				return balance.HeadWear;
			}
			else if (wear.SuitableSlot == WearSlot.UpperBody)
			{
				return balance.UpperBodyWear;
			}
			else if (wear.SuitableSlot == WearSlot.LowerBody)
			{
				return balance.LowerBodyWear;
			}
			else if (wear.SuitableSlot == WearSlot.Cover)
			{
				return balance.CoverWear;
			}
			else if (wear.SuitableSlot == WearSlot.Hands)
			{
				return balance.HandsWear;
			}
			else if (wear.SuitableSlot == WearSlot.Foots)
			{
				return balance.FootsWear;
			}
			else if (wear.SuitableSlot == WearSlot.Jewelry)
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
