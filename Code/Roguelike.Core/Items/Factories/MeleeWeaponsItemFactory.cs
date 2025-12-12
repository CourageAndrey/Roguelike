using Roguelike.Core.Aspects;
using Roguelike.Core.Interfaces;
using Roguelike.Core.Mechanics;

namespace Roguelike.Core.Items.Factories
{
	internal class MeleeWeaponsItemFactory
	{
		public IItem CreateHatchet()
		{
			return new Item(
				(language, alive) => language.Items.Hatchet,
				() => 2,
				ItemType.Weapon,
				Material.Metal.Color,
				Material.Metal,
				new MeleeWeapon(), new TreeChopper()
			);
		}

		public IItem CreateSword()
		{
			return new Item(
				(language, alive) => language.Items.Sword,
				() => 1.5m,
				ItemType.Weapon,
				Material.Metal.Color,
				Material.Metal,
				new MeleeWeapon()
			);
		}

		public IItem CreateMace()
		{
			return new Item(
				(language, alive) => language.Items.Mace,
				() => 2,
				ItemType.Weapon,
				Material.Wood.Color,
				Material.Wood,
				new MeleeWeapon()
			);
		}

		public IItem CreateSpear()
		{
			return new Item(
				(language, alive) => language.Items.Spear,
				() => 3,
				ItemType.Weapon,
				Material.Wood.Color,
				Material.Wood,
				new MeleeWeapon()
			);
		}
	}
}
