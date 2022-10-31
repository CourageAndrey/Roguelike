using System.Drawing;

using Roguelike.Core.Interfaces;
using Roguelike.Core.Localization;

namespace Roguelike.Core.Items
{
	public class Arrow : Item, IMissile
	{
		#region Properties

		public override Color Color
		{ get { return Color.White; } }

		public override ItemType Type
		{ get { return ItemType.Weapon; } }

		public override decimal Weight
		{ get { return 0.1m; } }

		#endregion

		public override string GetDescription(LanguageItems language, IAlive forWhom)
		{
			return language.Arrow;
		}
	}
}
