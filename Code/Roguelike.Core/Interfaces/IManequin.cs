using System;
using System.Collections.Generic;

using Roguelike.Core.Configuration;
using Roguelike.Core.Items;

namespace Roguelike.Core.Interfaces
{
	public interface IManequin
	{
		IItem HeadWear
		{ get; set; }

		IItem UpperBodyWear
		{ get; set; }

		IItem LowerBodyWear
		{ get; set; }

		IItem CoverWear
		{ get; set; }

		IItem HandsWear
		{ get; set; }

		IItem FootsWear
		{ get; set; }

		ICollection<IItem> Jewelry
		{ get; }

		event EventHandler<IManequin> EquipmentChanged;
	}

	public static class ManequinExtensions
	{
		public static IEnumerable<IItem> GetAllItems(this IManequin manequin)
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

		public static void Dress(this IManequin manequin, IItem item)
		{
			var wear = item.GetAspect<Wear>();

			if (wear.SuitableSlot == WearSlot.Head)
			{
				manequin.HeadWear = item;
			}
			else if (wear.SuitableSlot == WearSlot.UpperBody)
			{
				manequin.UpperBodyWear = item;
			}
			else if (wear.SuitableSlot == WearSlot.LowerBody)
			{
				manequin.LowerBodyWear = item;
			}
			else if (wear.SuitableSlot == WearSlot.Cover)
			{
				manequin.CoverWear = item;
			}
			else if (wear.SuitableSlot == WearSlot.Hands)
			{
				manequin.HandsWear = item;
			}
			else if (wear.SuitableSlot == WearSlot.Foots)
			{
				manequin.FootsWear = item;
			}
			else if (wear.SuitableSlot == WearSlot.Jewelry)
			{
				manequin.Jewelry.Add(item);
			}
			else
			{
				throw new NotSupportedException();
			}
		}

		public static void Undress(this IManequin manequin, IItem item)
		{
			var wear = item.GetAspect<Wear>();

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
				manequin.Jewelry.Remove(item);
			}
			else
			{
				throw new NotSupportedException();
			}
		}

		public static int GetDressTime(this IItem item, DressTimeBalance balance)
		{
			var wear = item.GetAspect<Wear>();

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
