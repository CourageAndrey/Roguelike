using System.Drawing;

using Roguelike.Core.Interfaces;
using Roguelike.Core.Localization;

namespace Roguelike.Core.Items
{
	public class BottleOFWater : Item, IDrink
	{
		#region Properties

		public override decimal Weight
		{ get { return 0.6m; } }

		public override ItemType Type
		{ get { return ItemType.Potion; } }

		public override Color Color
		{ get { return Color.DodgerBlue; } }

		public override Material Material
		{ get { return Material.Liquid; } }

		public int Nutricity
		{ get { return 0; } }

		public int Water
		{ get { return 500; } }

		#endregion

		public override string GetDescription(LanguageItems language, IAlive forWhom)
		{
			return language.BottleOFWater;
		}
	}
}
