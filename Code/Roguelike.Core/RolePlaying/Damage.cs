using Roguelike.Core.Localization;

namespace Roguelike.Core.RolePlaying
{
	public class Damage
	{
		#region Properties

		private readonly Func<LanguageDamage, string> _getName;

		#endregion

		private Damage(Func<LanguageDamage, string> nameGetter)
		{
			_getName = nameGetter;
		}

		public string GetName(LanguageDamage language)
		{
			return _getName(language);
		}

		#region List

		public static Damage Piercing { get; } = new Damage(l => l.Piercing);
		public static Damage Slashing { get; } = new Damage(l => l.Slashing);
		public static Damage Chopping { get; } = new Damage(l => l.Chopping);
		public static Damage Bludgeoning { get; } = new Damage(l => l.Bludgeoning);
		public static Damage Elemental { get; } = new Damage(l => l.Elemental);

		#endregion
	}
}
