using System.Xml.Serialization;

namespace Roguelike.Core.Localization
{
	[XmlType]
	public class LanguageSkills
	{
		#region Properties

		[XmlElement]
		public string Carpentry { get; set; }
		[XmlElement]
		public string Smithing { get; set; }
		[XmlElement]
		public string Masonry { get; set; }
		[XmlElement]
		public string Pottery { get; set; }
		[XmlElement]
		public string Cooking { get; set; }
		[XmlElement]
		public string Alchemy { get; set; }
		[XmlElement]
		public string Herbalism { get; set; }
		[XmlElement]
		public string Hunting { get; set; }
		[XmlElement]
		public string Fishing { get; set; }
		[XmlElement]
		public string Agriculture { get; set; }
		[XmlElement]
		public string AnimalHusbandry { get; set; }
		[XmlElement]
		public string SpinningAndWeaving { get; set; }
		[XmlElement]
		public string Leathercraft { get; set; }
		[XmlElement]
		public string Healing { get; set; }
		[XmlElement]
		public string Sneaking { get; set; }
		[XmlElement]
		public string Pickpocketing { get; set; }
		[XmlElement]
		public string Mechanics { get; set; }
		[XmlElement]
		public string Alertness { get; set; }
		[XmlElement]
		public string Swimming { get; set; }
		[XmlElement]
		public string Climbing { get; set; }
		[XmlElement]
		public string Hygiene { get; set; }
		[XmlElement]
		public string Trade { get; set; }
		[XmlElement]
		public string Speechcraft { get; set; }
		[XmlElement]
		public string Etiquette { get; set; }
		[XmlElement]
		public string Literacy { get; set; }
		[XmlElement]
		public string Foreign { get; set; }
		[XmlElement]
		public string Music { get; set; }

		#endregion

		public static LanguageSkills CreateDefault()
		{
			return new LanguageSkills
			{
				Carpentry = "Carpentry",
				Smithing = "Smithing",
				Masonry = "Masonry",
				Pottery = "Pottery",
				Cooking = "Cooking",
				Alchemy = "Alchemy",
				Herbalism = "Herbalism",
				Hunting = "Hunting",
				Fishing = "Fishing",
				Agriculture = "Agriculture",
				AnimalHusbandry = "Animal husbandry",
				SpinningAndWeaving = "Spinning and weaving",
				Leathercraft = "Leathercraft",
				Healing = "Healing",
				Sneaking = "Sneaking",
				Pickpocketing = "Pickpocketing",
				Mechanics = "Mechanics",
				Alertness = "Alertness",
				Swimming = "Swimming",
				Climbing = "Climbing",
				Hygiene = "Hygiene",
				Trade = "Trade",
				Speechcraft = "Speechcraft",
				Etiquette = "Etiquette",
				Literacy = "Literacy",
				Foreign = "Foreign",
				Music = "Music",
			};
		}
	}
}
