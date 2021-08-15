using System;

using Roguelike.Core.ActiveObjects;
using Roguelike.Core.Interfaces;

namespace Roguelike.Core.Items
{
	public class Naked : Item, IHeadWear, IUpperBodyWear, ILowerBodyWear, ICoverWear, IHandWear, IFootWear
	{
		#region Properties

		public override double Weight
		{ get { return 0; } }

		public override ItemType Type
		{ get { throw new NotSupportedException(); } }

		public Humanoid Owner
		{ get; }

		#endregion

		public Naked(Humanoid owner)
		{
			Owner = owner;
		}
	}
}
