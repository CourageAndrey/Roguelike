using System.Xml.Serialization;

namespace Roguelike.Core.Localization
{
	[XmlType]
	public class LanguageElements
	{
		#region Properties

		[XmlElement]
		public string Air { get; set; }
		[XmlElement]
		public string Water { get; set; }
		[XmlElement]
		public string Ground { get; set; }
		[XmlElement]
		public string Fire { get; set; }
		[XmlElement]
		public string Frost { get; set; }
		[XmlElement]
		public string Metal { get; set; }
		[XmlElement]
		public string Poison { get; set; }
		[XmlElement]
		public string Electricity { get; set; }
		[XmlElement]
		public string Acid { get; set; }
		[XmlElement]
		public string Soul { get; set; }

		#endregion

		public static LanguageElements CreateDefault()
		{
			return new LanguageElements
			{
				Air = "Air",
				Water = "Water",
				Ground = "Ground",
				Fire = "Fire",
				Frost = "Frost",
				Metal = "Metal",
				Poison = "Poison",
				Electricity = "Electricity",
				Acid = "Acid",
				Soul = "Soul",
			};
		}
	}
}
