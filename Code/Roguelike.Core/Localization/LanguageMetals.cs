using System.Xml.Serialization;

namespace Roguelike.Core.Localization
{
	[XmlType]
	public class LanguageMetals
	{
		#region Properties

		[XmlElement]
		public string Brass
		{ get; set; }
		[XmlElement]
		public string Iron
		{ get; set; }
		[XmlElement]
		public string Steel
		{ get; set; }
		[XmlElement]
		public string Mithril
		{ get; set; }
		[XmlElement]
		public string Adamantine
		{ get; set; }

		#endregion

		public static LanguageMetals CreateDefault()
		{
			return new LanguageMetals
			{
				Brass = "Brass",
				Iron = "Iron",
				Steel = "Steel",
				Mithril = "Mithril",
				Adamantine = "Adamantine",
			};
		}
	}
}
