using System.Drawing;

using Roguelike.Core.Interfaces;
using Roguelike.Core.Localization;

namespace Roguelike.Core.Items
{
	public class Shirt : Wear, IUpperBodyWear
	{
		#region Properties

		public override decimal Weight
		{ get { return 1; } }

		public override Material Material
		{ get { return Material.Fabric; } }

		#endregion

		public Shirt(Color clothColor)
			: base(clothColor)
		{ }

		public override string GetDescription(LanguageItems language, IAlive forWhom)
		{
			return language.Shirt;
		}
	}
}
