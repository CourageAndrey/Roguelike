using System.Xml.Serialization;

namespace Roguelike.Core.Localization
{
	[XmlType]
	public class LanguageMetals
	{
		#region Properties

		[XmlElement]
		public string Copper
		{ get; set; }
		[XmlElement]
		public string Bronze
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
		[XmlElement]
		public string Ethereum
		{ get; set; }
		[XmlElement]
		public string Silver
		{ get; set; }
		[XmlElement]
		public string Gold
		{ get; set; }
		[XmlElement]
		public string Platinum
		{ get; set; }

		#endregion

		public static LanguageMetals CreateDefault()
		{
			return new LanguageMetals
			{
				Bronze = "Bronze",
				Iron = "Iron",
				Steel = "Steel",
				Mithril = "Mithril",
				Adamantine = "Adamantine",
				Ethereum = "Ethereum",
				Copper = "Copper",
				Silver = "Silver",
				Gold = "Gold",
				Platinum = "Platinum",
			};
		}
	}
}
