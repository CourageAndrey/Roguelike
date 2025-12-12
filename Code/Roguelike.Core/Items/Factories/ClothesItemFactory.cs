using System.Drawing;

using Roguelike.Core.Aspects;
using Roguelike.Core.Interfaces;
using Roguelike.Core.Mechanics;

namespace Roguelike.Core.Items.Factories
{
	internal class ClothesItemFactory
	{
		public IItem CreateShirt(Color clothColor)
		{
			return new Item(
				(language, alive) => language.Items.Clothes.Shirt,
				() => 1,
				ItemType.Wear,
				clothColor,
				Material.Fabric,
				new Wear(WearSlot.UpperBody)
			);
		}

		public IItem CreateSkirt(Color clothColor)
		{
			return new Item(
				(language, alive) => language.Items.Clothes.Skirt,
				() => 1,
				ItemType.Wear,
				clothColor,
				Material.Fabric,
				new Wear(WearSlot.LowerBody)
			);
		}

		public IItem CreateTrousers(Color clothColor)
		{
			return new Item(
				(language, alive) => language.Items.Clothes.Trousers,
				() => 1,
				ItemType.Wear,
				clothColor,
				Material.Fabric,
				new Wear(WearSlot.LowerBody)
			);
		}

		public IItem CreateMantle(Color clothColor)
		{
			return new Item(
				(language, alive) => language.Items.Clothes.Mantle,
				() => 2,
				ItemType.Wear,
				clothColor,
				Material.Fabric,
				new Wear(WearSlot.UpperBody)
			);
		}

		public IItem CreateGown(Color clothColor)
		{
			return new Item(
				(language, alive) => language.Items.Clothes.Gown,
				() => 3,
				ItemType.Wear,
				clothColor,
				Material.Fabric,
				new Wear(WearSlot.UpperBody)
			);
		}

		public IItem CreateHoodedCloak(Color clothColor)
		{
			return new Item(
				(language, alive) => language.Items.Clothes.HoodedCloak,
				() => 2,
				ItemType.Wear,
				clothColor,
				Material.Fabric,
				new Wear(WearSlot.Cover)
			);
		}

		public IItem CreateCap(Color clothColor)
		{
			return new Item(
				(language, alive) => language.Items.Clothes.Cap,
				() => 0.3m,
				ItemType.Wear,
				clothColor,
				Material.Fabric,
				new Wear(WearSlot.Head)
			);
		}

		public IItem CreateBoots(Color clothColor)
		{
			return new Item(
				(language, alive) => language.Items.Clothes.Boots,
				() => 0.8m,
				ItemType.Wear,
				clothColor,
				Material.Fabric,
				new Wear(WearSlot.Foots)
			);
		}

		public IItem CreateGloves(Color clothColor)
		{
			return new Item(
				(language, alive) => language.Items.Clothes.Gloves,
				() => 0.2m,
				ItemType.Wear,
				clothColor,
				Material.Fabric,
				new Wear(WearSlot.Hands)
			);
		}
	}
}
