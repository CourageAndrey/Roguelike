using System.Xml.Serialization;

namespace Roguelike.Core.Localization
{
	[XmlType]
	public class LanguageCharacter
	{
		#region Properties

		[XmlElement]
		public string SexIsMale
		{ get; set; }

		[XmlElement]
		public string SexIsFemale
		{ get; set; }

		[XmlElement]
		public string AgeYears
		{ get; set; }

		[XmlElement]
		public LanguageProperties Properties
		{ get; set; }

		[XmlElement]
		public LanguageBodyParts BodyParts
		{ get; set; }

		[XmlElement]
		public LanguageState State
		{ get; set; }

		[XmlElement]
		public LanguageRaces Races
		{ get; set; }

		[XmlElement]
		public LanguageMannequin Mannequin
		{ get; set; }

		[XmlElement]
		public LanguageProfessions Professions
		{ get; set; }

		[XmlElement]
		public LanguageHaircuts Haircuts
		{ get; set; }

		[XmlElement]
		public LanguageTraits Traits
		{ get; set; }

		[XmlElement]
		public LanguageSkills Skills
		{ get; set; }

		[XmlElement]
		public LanguageWeaponMasteries WeaponMasteries
		{ get; set; }

		#endregion

		public static LanguageCharacter CreateDefault()
		{
			return new LanguageCharacter
			{
				SexIsMale = "Male",

				SexIsFemale = "Female",

				AgeYears = "years old",

				Properties = LanguageProperties.CreateDefault(),

				BodyParts = LanguageBodyParts.CreateDefault(),

				State = LanguageState.CreateDefault(),

				Races = LanguageRaces.CreateDefault(),

				Mannequin = LanguageMannequin.CreateDefault(),

				Professions = LanguageProfessions.CreateDefault(),

				Haircuts = LanguageHaircuts.CreateDefault(),

				Traits = LanguageTraits.CreateDefault(),

				Skills = LanguageSkills.CreateDefault(),

				WeaponMasteries = LanguageWeaponMasteries.CreateDefault(),
			};
		}
	}
}
