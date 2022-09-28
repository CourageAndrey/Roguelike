using System;

using Roguelike.Core.ActiveObjects;

namespace Roguelike.Core.Items
{
	public class Unarmed : Weapon
	{
		#region Properties

		public override double Weight
		{ get { return 0; } }

		public override ItemType Type
		{ get { throw new NotSupportedException(); } }

		public Alive Fighter
		{ get; }

		#endregion

		public Unarmed(Alive fighter)
		{
			Fighter = fighter;
		}
	}
}
