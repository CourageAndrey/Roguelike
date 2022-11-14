using System;

using Roguelike.Core.Interfaces;
using Roguelike.Core.Localization;

namespace Roguelike.Core.Items
{
	public class Naked : Wear
	{
		#region Properties

		public override WearSlot SuitableSlot
		{ get { throw new NotSupportedException(); } }

		public override decimal Weight
		{ get { return 0; } }

		public override ItemType Type
		{ get { throw new NotSupportedException(); } }

		public override Material Material
		{ get { throw new NotSupportedException(); } }

		public IHumanoid Owner
		{ get; }

		#endregion

		public Naked(IHumanoid owner)
			: base(owner.Race.SkinColor)
		{
			Owner = owner;
		}

		public override string GetDescription(Language language, IAlive forWhom)
		{
			throw new NotSupportedException();
		}
	}
}
