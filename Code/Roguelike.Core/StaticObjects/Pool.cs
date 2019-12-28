using Roguelike.Core.Interfaces;

namespace Roguelike.Core.StaticObjects
{
	public class Pool : Object, IWaterSource
	{
		#region Properties

		public override bool IsSolid
		{ get { return false; } }

		#endregion
	}
}
