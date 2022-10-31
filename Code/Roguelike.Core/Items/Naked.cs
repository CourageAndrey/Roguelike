using System;

using Roguelike.Core.Interfaces;
using Roguelike.Core.Localization;

namespace Roguelike.Core.Items
{
	public class Naked : Wear, IHeadWear, IUpperBodyWear, ILowerBodyWear, ICoverWear, IHandWear, IFootWear
	{
		#region Properties

		public override double Weight
		{ get { return 0; } }

		public override ItemType Type
		{ get { throw new NotSupportedException(); } }

		public IHumanoid Owner
		{ get; }

		#endregion

		public Naked(IHumanoid owner)
			: base(owner.Race.SkinColor)
		{
			Owner = owner;
		}

		public override string GetDescription(LanguageItems language, IAlive forWhom)
		{
			throw new NotSupportedException();
		}
	}
}
