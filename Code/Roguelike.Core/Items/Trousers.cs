using System.Drawing;

using Roguelike.Core.Interfaces;
using Roguelike.Core.Localization;

namespace Roguelike.Core.Items
{
	public class Trousers : Wear
	{
		#region Properties

		public override WearSlot SuitableSlot
		{
			get { return WearSlot.LowerBody; }
		}

		public override decimal Weight
		{ get { return 1; } }

		public override Material Material
		{ get { return Material.Fabric; } }

		#endregion

		public Trousers(Color clothColor)
			: base(clothColor)
		{ }

		public override string GetDescription(Language language, IAlive forWhom)
		{
			return language.Items.Trousers;
		}
	}
}
