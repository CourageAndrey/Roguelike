using Roguelike.Core.Interfaces;
using Roguelike.Core.Localization;

namespace Roguelike.Core.Items
{
	public class Hatchet : Weapon
	{
		#region Properties

		public override bool IsRange
		{ get { return false; } }

		public override decimal Weight
		{ get { return 2; } }

		public override Material Material
		{ get { return Material.Metal; } }

		#endregion

		public override string GetDescription(LanguageItems language, IAlive forWhom)
		{
			return language.Hatchet;
		}
	}
}
