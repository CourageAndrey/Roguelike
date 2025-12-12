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
				(language, alive) => language.Items.Shirt,
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
				(language, alive) => language.Items.Skirt,
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
				(language, alive) => language.Items.Trousers,
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
				(language, alive) => language.Items.Mantle,
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
				(language, alive) => language.Items.Gown,
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
				(language, alive) => language.Items.HoodedCloak,
				() => 2,
				ItemType.Wear,
				clothColor,
				Material.Fabric,
				new Wear(WearSlot.Cover)
			);
		}
	}
}
