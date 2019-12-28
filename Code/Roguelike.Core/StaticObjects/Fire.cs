using Roguelike.Core.Interfaces;

namespace Roguelike.Core.StaticObjects
{
	public class Fire : Object, IFireSource
	{
		#region Properties

		public override bool IsSolid
		{ get { return false; } }

		#endregion
	}
}
