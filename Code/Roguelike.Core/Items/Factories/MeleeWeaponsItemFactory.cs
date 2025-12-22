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
				(language, alive) => language.Items.MeleeWeapons.Hatchet,
				() => 2,
				ItemType.Weapon,
				Material.Metal.Color,
				Material.Metal,
				new MeleeWeapon(DamageType.Chopping, WeaponMastery.Axes), new TreeChopper()
			);
		}

		public IItem CreateSword()
		{
			return new Item(
				(language, alive) => language.Items.MeleeWeapons.Sword,
				() => 1.5m,
				ItemType.Weapon,
				Material.Metal.Color,
				Material.Metal,
				new MeleeWeapon(DamageType.Slashing, WeaponMastery.Blades)
			);
		}

		public IItem CreateMace()
		{
			return new Item(
				(language, alive) => language.Items.MeleeWeapons.Mace,
				() => 2,
				ItemType.Weapon,
				Material.Wood.Color,
				Material.Wood,
				new MeleeWeapon(DamageType.Bludgeoning, WeaponMastery.Maces)
			);
		}

		public IItem CreateSpear()
		{
			return new Item(
				(language, alive) => language.Items.MeleeWeapons.Spear,
				() => 3,
				ItemType.Weapon,
				Material.Wood.Color,
				Material.Wood,
				new MeleeWeapon(DamageType.Piercing, WeaponMastery.Polearms)
			);
		}

		public IItem CreateDagger()
		{
			return new Item(
				(language, alive) => language.Items.MeleeWeapons.Dagger,
				() => 0.5m,
				ItemType.Weapon,
				Material.Metal.Color,
				Material.Metal,
				new MeleeWeapon(DamageType.Piercing, WeaponMastery.Blades)
			);
		}

		public IItem CreateAxe()
		{
			return new Item(
				(language, alive) => language.Items.MeleeWeapons.Axe,
				() => 2.5m,
				ItemType.Weapon,
				Material.Metal.Color,
				Material.Metal,
				new MeleeWeapon(DamageType.Chopping, WeaponMastery.Axes)
			);
		}

		public IItem CreateClub()
		{
			return new Item(
				(language, alive) => language.Items.MeleeWeapons.Club,
				() => 1.5m,
				ItemType.Weapon,
				Material.Wood.Color,
				Material.Wood,
				new MeleeWeapon(DamageType.Bludgeoning, WeaponMastery.Maces)
			);
		}

		public IItem CreateWarhammer()
		{
			return new Item(
				(language, alive) => language.Items.MeleeWeapons.Warhammer,
				() => 3.5m,
				ItemType.Weapon,
				Material.Metal.Color,
				Material.Metal,
				new MeleeWeapon(DamageType.Bludgeoning, WeaponMastery.Maces)
			);
		}

		public IItem CreateRapier()
		{
			return new Item(
				(language, alive) => language.Items.MeleeWeapons.Rapier,
				() => 1m,
				ItemType.Weapon,
				Material.Metal.Color,
				Material.Metal,
				new MeleeWeapon(DamageType.Piercing, WeaponMastery.Blades)
			);
		}

		public IItem CreateFlail()
		{
			return new Item(
				(language, alive) => language.Items.MeleeWeapons.Flail,
				() => 2.5m,
				ItemType.Weapon,
				Material.Metal.Color,
				Material.Metal,
				new MeleeWeapon(DamageType.Bludgeoning, WeaponMastery.Maces)
			);
		}
	}
}
