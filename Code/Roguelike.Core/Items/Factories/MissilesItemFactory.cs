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
			return new Item(
				(language, alive) =>
				{
					switch (missileType)
					{
						case MissileType.Arrow:
							return language.Items.Arrow;
						case MissileType.Bolt:
							return language.Items.Bolt;
						case MissileType.Bullet:
							return language.Items.Bullet;
						default:
							throw new ArgumentOutOfRangeException(nameof(missileType), missileType, null);
					}
				},
				() => 0.1m,
				ItemType.Weapon,
				Color.White,
				Material.Wood,
				new Missile(missileType));
		}
	}
}
