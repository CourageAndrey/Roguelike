using System.Xml.Serialization;

namespace Roguelike.Core.Localization.Items
{
	[XmlType]
	public class LanguageClothes
	{
		#region Properties

		[XmlElement]
		public string Trousers
		{ get; set; }

		[XmlElement]
		public string Skirt
		{ get; set; }

		[XmlElement]
		public string Shirt
		{ get; set; }

		[XmlElement]
		public string Mantle
		{ get; set; }

		[XmlElement]
		public string Gown
		{ get; set; }

		[XmlElement]
		public string HoodedCloak
		{ get; set; }

		[XmlElement]
		public string Cap
		{ get; set; }

		[XmlElement]
		public string Boots
		{ get; set; }

		[XmlElement]
		public string Gloves
		{ get; set; }

		#endregion

		public static LanguageClothes CreateDefault()
		{
			return new LanguageClothes
			{
				Trousers = "Trousers",
				Skirt = "Skirt",
				Shirt = "Shirt",
				Mantle = "Mantle",
				Gown = "Gown",
				HoodedCloak = "HoodedCloak",
				Cap = "Cap",
				Boots = "Boots",
				Gloves = "Gloves",
			};
		}
	}
}
