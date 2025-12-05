using Roguelike.Core.Localization;

namespace Roguelike.Core
{
	public class Skill
	{
		#region Properties

		private readonly Func<LanguageSkills, string> _getName;

		#endregion

		private Skill(Func<LanguageSkills, string> nameGetter)
		{
			_getName = nameGetter;
		}

		public string GetName(LanguageSkills language)
		{
			return _getName(language);
		}

		#region List

		public static Skill Carpentry { get; } = new Skill(l => l.Carpentry);
		public static Skill Smithing { get; } = new Skill(l => l.Smithing);
		public static Skill Masonry { get; } = new Skill(l => l.Masonry);
		public static Skill Pottery { get; } = new Skill(l => l.Pottery);
		// NO mining and woodpicking - AXES weapon mastery instead
		public static Skill Cooking { get; } = new Skill(l => l.Cooking);
		public static Skill Alchemy { get; } = new Skill(l => l.Alchemy);
		public static Skill Herbalism { get; } = new Skill(l => l.Herbalism);
		public static Skill Hunting { get; } = new Skill(l => l.Hunting);
		public static Skill Fishing { get; } = new Skill(l => l.Fishing);
		public static Skill Agriculture { get; } = new Skill(l => l.Agriculture);
		public static Skill AnimalHusbandry { get; } = new Skill(l => l.AnimalHusbandry);
		public static Skill SpinningAndWeaving { get; } = new Skill(l => l.SpinningAndWeaving);
		public static Skill Leathercraft { get; } = new Skill(l => l.Leathercraft);
		public static Skill Healing { get; } = new Skill(l => l.Healing);
		public static Skill Sneaking { get; } = new Skill(l => l.Sneaking);
		public static Skill Pickpocketing { get; } = new Skill(l => l.Pickpocketing);
		public static Skill Mechanics { get; } = new Skill(l => l.Mechanics);
		public static Skill Alertness { get; } = new Skill(l => l.Alertness);
		public static Skill Swimming { get; } = new Skill(l => l.Swimming);
		public static Skill Climbing { get; } = new Skill(l => l.Climbing);
		public static Skill Hygiene { get; } = new Skill(l => l.Hygiene);
		public static Skill Trade { get; } = new Skill(l => l.Trade);
		public static Skill Speechcraft { get; } = new Skill(l => l.Speechcraft);
		public static Skill Etiquette { get; } = new Skill(l => l.Etiquette);
		public static Skill Literacy { get; } = new Skill(l => l.Literacy);
		public static Skill Foreign { get; } = new Skill(l => l.Foreign);
		public static Skill Music { get; } = new Skill(l => l.Music);

		#endregion
	}
}
