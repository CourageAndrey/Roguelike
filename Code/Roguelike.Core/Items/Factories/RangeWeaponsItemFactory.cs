using Roguelike.Core.Aspects;
using Roguelike.Core.Interfaces;
using Roguelike.Core.Mechanics;

namespace Roguelike.Core.Items.Factories
{
	internal class RangeWeaponsItemFactory
	{
		public IItem CreateBow()
		{
			return new Item(
				(language, alive) => language.Items.RangeWeapons.Bow,
				() => 1,
				ItemType.Weapon,
				Material.Wood.Color,
				Material.Wood,
				new RangeWeapon(MissileType.Arrow)
			);
		}

		public IItem CreateCrossbow()
		{
			return new Item(
				(language, alive) => language.Items.RangeWeapons.Crossbow,
				() => 3,
				ItemType.Weapon,
				Material.Wood.Color,
				Material.Wood,
				new RangeWeapon(MissileType.Bolt)
			);
		}

		public IItem CreateSling()
		{
			return new Item(
				(language, alive) => language.Items.RangeWeapons.Sling,
				() => 0.25m,
				ItemType.Weapon,
				Material.Skin.Color,
				Material.Skin,
				new RangeWeapon(MissileType.Bullet)
			);
		}
	}
}
