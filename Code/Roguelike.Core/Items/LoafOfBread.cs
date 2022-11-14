using System.Drawing;

using Roguelike.Core.Interfaces;
using Roguelike.Core.Localization;

namespace Roguelike.Core.Items
{
	public class LoafOfBread : Item, IFood
	{
		#region Properties

		public override decimal Weight
		{ get { return 0.4m; } }

		public override ItemType Type
		{ get { return ItemType.Food; } }

		public override Color Color
		{ get { return Color.Brown; } }

		public override Material Material
		{ get { return Material.Food; } }

		public int Nutricity
		{ get { return 500; } }

		public int Water
		{ get { return -50; } }

		#endregion

		public override string GetDescription(Language language, IAlive forWhom)
		{
			return language.Items.LoafOfBread;
		}
	}
}
