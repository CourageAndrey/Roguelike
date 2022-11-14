using Roguelike.Core.Interfaces;
using Roguelike.Core.Localization;

namespace Roguelike.Core.Items
{
	public class Bow : Weapon
	{
		#region Properties

		public override bool IsRange
		{ get { return true; } }

		public override decimal Weight
		{ get { return 1; } }

		public override Material Material
		{ get { return Material.Wood; } }

		#endregion

		public override string GetDescription(Language language, IAlive forWhom)
		{
			return language.Items.Bow;
		}
	}
}
