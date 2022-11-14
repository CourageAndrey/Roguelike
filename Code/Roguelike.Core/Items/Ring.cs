using System.Drawing;

using Roguelike.Core.Interfaces;
using Roguelike.Core.Localization;

namespace Roguelike.Core.Items
{
	public class Ring : Wear
	{
		#region Properties

		public override WearSlot SuitableSlot
		{
			get { return WearSlot.Jewelry; }
		}

		public override decimal Weight
		{ get { return 0.02m; } }

		public override ItemType Type
		{ get { return ItemType.Wear; } }

		public override Material Material
		{ get { return Material.Metal; } }

		#endregion

		public Ring()
			: base(Color.Aquamarine)
		{ }

		public override string GetDescription(Language language, IAlive forWhom)
		{
			return language.Items.Ring;
		}
	}
}
