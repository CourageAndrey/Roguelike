using System.Collections.Generic;
using System.Xml.Serialization;

namespace Roguelike.Core.Localization
{
	[XmlType]
	public class LanguageProperties
	{
		#region Properties

		[XmlElement]
		public string Strength
		{ get; set; }
		[XmlElement]
		public string Endurance
		{ get; set; }
		[XmlElement]
		public string Reaction
		{ get; set; }
		[XmlElement]
		public string Perception
		{ get; set; }
		[XmlElement]
		public string Intelligence
		{ get; set; }
		[XmlElement]
		public string Willpower
		{ get; set; }

		#endregion

		public static LanguageProperties CreateDefault()
		{
			return new LanguageProperties
			{
				Strength = "Strength",
				Endurance = "Endurance",
				Reaction = "Reaction",
				Perception = "Perception",
				Intelligence = "Intelligence",
				Willpower = "Willpower",
			};
		}

		public List<string> GetAll()
		{
			return new List<string>
			{
				Strength,
				Endurance,
				Reaction,
				Perception,
				Intelligence,
				Willpower,
			};
		}
	}
}
