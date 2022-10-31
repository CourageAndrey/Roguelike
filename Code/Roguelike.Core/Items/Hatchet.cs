using Roguelike.Core.Interfaces;
using Roguelike.Core.Localization;

namespace Roguelike.Core.Items
{
	public class Hatchet : Weapon
	{
		#region Properties

		public override bool IsRange
		{ get { return false; } }

		public override double Weight
		{ get { return 2; } }

		#endregion

		public Hatchet()
			: base(Material.Wood)
		{ }

		public override string GetDescription(LanguageItems language, IAlive forWhom)
		{
			return language.Hatchet;
		}
	}
}
