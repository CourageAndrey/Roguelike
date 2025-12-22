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
				new RangeWeapon(DamageType.Piercing, WeaponMastery.Bows, MissileType.Arrow)
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
				new RangeWeapon(DamageType.Piercing, WeaponMastery.Bows, MissileType.Bolt)
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
				new RangeWeapon(DamageType.Piercing, WeaponMastery.Throwing, MissileType.Bullet)
			);
		}

		public IItem CreateLongbow()
		{
			return new Item(
				(language, alive) => language.Items.RangeWeapons.Longbow,
				() => 1.5m,
				ItemType.Weapon,
				Material.Wood.Color,
				Material.Wood,
				new RangeWeapon(DamageType.Piercing, WeaponMastery.Bows, MissileType.Arrow)
			);
		}

		public IItem CreateShortbow()
		{
			return new Item(
				(language, alive) => language.Items.RangeWeapons.Shortbow,
				() => 0.8m,
				ItemType.Weapon,
				Material.Wood.Color,
				Material.Wood,
				new RangeWeapon(DamageType.Piercing, WeaponMastery.Bows, MissileType.Arrow)
			);
		}

		public IItem CreateHeavyCrossbow()
		{
			return new Item(
				(language, alive) => language.Items.RangeWeapons.HeavyCrossbow,
				() => 4m,
				ItemType.Weapon,
				Material.Metal.Color,
				Material.Metal,
				new RangeWeapon(DamageType.Piercing, WeaponMastery.Bows, MissileType.Bolt)
			);
		}
	}
}
