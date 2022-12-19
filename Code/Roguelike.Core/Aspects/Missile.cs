using Roguelike.Core.Interfaces;

namespace Roguelike.Core.Aspects
{
	public class Missile : IAspect
	{
		#region Properties

		public MissileType Type
		{ get; }

		#endregion

		public Missile(MissileType type)
		{
			Type = type;
		}
	}

	public enum MissileType
	{
		Arrow,
		Bolt,
		Bullet,
	}
}
