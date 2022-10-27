using System.Xml.Serialization;

namespace Roguelike.Core.Localization
{
	[XmlType]
	public class LanguageDiseases
	{
		#region Properties

		[XmlElement]
		public string Worms { get; set; }
		[XmlElement]
		public string Ulcer { get; set; }
		[XmlElement]
		public string Plague { get; set; }
		[XmlElement]
		public string Lues { get; set; }
		[XmlElement]
		public string Leprosy { get; set; }
		[XmlElement]
		public string Fever { get; set; }

		#endregion

		public static LanguageDiseases CreateDefault()
		{
			return new LanguageDiseases
			{
				Worms = "Worms",
				Ulcer = "Ulcer",
				Plague = "Plague",
				Lues = "Lues",
				Leprosy = "Leprosy",
				Fever = "Fever",
			};
		}
	}
}
