using Roguelike.Core.Items.Factories;

namespace Roguelike.Core.Items
{
	internal static class ItemFactory
	{
		public static readonly MissilesItemFactory Missiles = new();

		public static readonly PapersItemFactory Papers = new();

		public static readonly PotionsItemFactory Potions = new();

		public static readonly RangeWeaponsItemFactory RangeWeapons = new();

		public static readonly MeleeWeaponsItemFactory MeleeWeapons = new();

		public static readonly FoodItemFactory Food = new();

		public static readonly ToolsItemFactory Tools = new();

		public static readonly JewelryItemFactory Jewelry = new();

		public static readonly ClothesItemFactory Clothes = new();

		public static readonly SpecialItemFactory Special = new();
	}
}
