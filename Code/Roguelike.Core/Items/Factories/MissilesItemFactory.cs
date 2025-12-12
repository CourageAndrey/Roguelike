using System.Drawing;

using Roguelike.Core.Aspects;
using Roguelike.Core.Interfaces;
using Roguelike.Core.Mechanics;

namespace Roguelike.Core.Items.Factories
{
	internal class MissilesItemFactory
	{
		public IItem CreateMissile(MissileType missileType)
		{
			switch (missileType)
			{
				case MissileType.Arrow:
					return CreateArrow();
				case MissileType.Bolt:
					return CreateBolt();
				case MissileType.Bullet:
					return CreateBullet();
				default:
					throw new ArgumentOutOfRangeException(nameof(missileType), missileType, null);
			}
		}

		public IItem CreateArrow()
		{
			return new Item(
				(language, alive) => language.Items.Missiles.Arrow,
				() => 0.1m,
				ItemType.Weapon,
				Color.White,
				Material.Wood,
				new Missile(MissileType.Arrow));
		}

		public IItem CreateBolt()
		{
			return new Item(
				(language, alive) => language.Items.Missiles.Bolt,
				() => 0.1m,
				ItemType.Weapon,
				Color.White,
				Material.Wood,
				new Missile(MissileType.Bolt));
		}

		public IItem CreateBullet()
		{
			return new Item(
				(language, alive) => language.Items.Missiles.Bullet,
				() => 0.1m,
				ItemType.Weapon,
				Color.White,
				Material.Wood,
				new Missile(MissileType.Bullet));
		}
	}
}
