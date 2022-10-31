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

		#endregion

		public Bow()
			: base(Material.Wood)
		{ }

		public override string GetDescription(LanguageItems language, IAlive forWhom)
		{
			return language.Bow;
		}
	}
}
