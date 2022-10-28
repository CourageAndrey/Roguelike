using System;
using System.Drawing;

using Roguelike.Core.Interfaces;

namespace Roguelike.Core.Items
{
	public class Unarmed : Weapon
	{
		#region Properties

		public override Color Color
		{ get { throw new NotSupportedException(); } }

		public override bool IsRange
		{ get { return false; } }

		public override double Weight
		{ get { return 0; } }

		public override ItemType Type
		{ get { throw new NotSupportedException(); } }

		public IAlive Fighter
		{ get; }

		#endregion

		public Unarmed(IAlive fighter)
			: base(null)
		{
			Fighter = fighter;
		}
	}
}
