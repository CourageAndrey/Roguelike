using System;

using Roguelike.Core.ActiveObjects;

namespace Roguelike.Core.Items
{
	public class Unarmed : Item
	{
		#region Properties

		public override double Weight
		{ get { return 0; } }

		public override ItemType Type
		{ get { throw new NotSupportedException(); } }

		public AliveObject Fighter
		{ get; }

		#endregion

		public Unarmed(AliveObject fighter)
		{
			Fighter = fighter;
		}
	}
}
