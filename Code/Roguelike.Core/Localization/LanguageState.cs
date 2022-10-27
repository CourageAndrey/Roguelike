using System.Xml.Serialization;

namespace Roguelike.Core.Localization
{
	[XmlType]
	public class LanguageState
	{
		#region Properties

		[XmlElement]
		public string IsHungry
		{ get; set; }
		[XmlElement]
		public string IsBloated
		{ get; set; }
		[XmlElement]
		public string IsThirsty
		{ get; set; }
		[XmlElement]
		public string IsTired
		{ get; set; }
		[XmlElement]
		public string IsFallingAsleep
		{ get; set; }
		[XmlElement]
		public string IsLosingBlood
		{ get; set; }
		[XmlElement]
		public string IsConfused
		{ get; set; }
		[XmlElement]
		public string IsFrozen
		{ get; set; }
		[XmlElement]
		public string IsSunburned
		{ get; set; }
		[XmlElement]
		public string IsFireBurned
		{ get; set; }
		[XmlElement]
		public string IsAcidBurned
		{ get; set; }
		[XmlElement]
		public string IsLightningBurned
		{ get; set; }
		[XmlElement]
		public string IsScared
		{ get; set; }
		[XmlElement]
		public string IsDrunk
		{ get; set; }
		[XmlElement]
		public string HasHangover
		{ get; set; }
		[XmlElement]
		public string IsPoisoned
		{ get; set; }
		[XmlElement]
		public string IsDirty
		{ get; set; }
		[XmlElement]
		public string IsSick
		{ get; set; }
		[XmlElement]
		public string Activity
		{ get; set; }
		[XmlElement]
		public LanguageActivities Activities
		{ get; set; }
		[XmlElement]
		public LanguageDiseases Diseases
		{ get; set; }

		#endregion

		public static LanguageState CreateDefault()
		{
			return new LanguageState
			{
				IsHungry = "Hungry",
				IsBloated = "Bloated",
				IsThirsty = "Thirsty",
				IsTired = "Tired",
				IsFallingAsleep = "Falling asleep",
				IsLosingBlood = "Losing blood",
				IsConfused = "Confused",
				IsFrozen = "Frozen",
				IsSunburned = "Sunburned",
				IsFireBurned = "Fire burned",
				IsAcidBurned = "Acid burned",
				IsLightningBurned = "Lightning burned",
				IsScared = "Scared",
				IsDrunk = "Drunk",
				HasHangover = "Hangover",
				IsPoisoned = "Poisoned",
				IsDirty = "Dirty",
				IsSick = "Sick",

				Activity = "Now",

				Activities = LanguageActivities.CreateDefault(),

				Diseases = LanguageDiseases.CreateDefault(),
			};
		}
	}
}
