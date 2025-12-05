using Roguelike.Core.Localization;

namespace Roguelike.Core
{
	public class WeaponMastery
	{
		#region Properties

		private readonly Func<LanguageWeaponMasteries, string> _getName;

		#endregion

		private WeaponMastery(Func<LanguageWeaponMasteries, string> nameGetter)
		{
			_getName = nameGetter;
		}

		public string GetName(LanguageWeaponMasteries language)
		{
			return _getName(language);
		}

		#region List

		public static WeaponMastery HandToHand { get; } = new WeaponMastery(l => l.HandToHand);
		public static WeaponMastery Blades { get; } = new WeaponMastery(l => l.Blades);
		public static WeaponMastery Maces { get; } = new WeaponMastery(l => l.Maces);
		public static WeaponMastery Polearms { get; } = new WeaponMastery(l => l.Polearms);
		public static WeaponMastery Bows { get; } = new WeaponMastery(l => l.Bows);
		public static WeaponMastery Slings { get; } = new WeaponMastery(l => l.Slings);

		#endregion
	}
}
