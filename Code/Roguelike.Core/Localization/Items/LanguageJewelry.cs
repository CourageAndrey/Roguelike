using System.Xml.Serialization;

namespace Roguelike.Core.Localization.Items
{
	[XmlType]
	public class LanguageJewelry
	{
		#region Properties

		[XmlElement]
		public string Ring
		{ get; set; }

		[XmlElement]
		public string Amulet
		{ get; set; }

		[XmlElement]
		public string Necklace
		{ get; set; }

		[XmlElement]
		public string Bracelet
		{ get; set; }

		#endregion

		public static LanguageJewelry CreateDefault()
		{
			return new LanguageJewelry
			{
				Ring = "Ring",
				Amulet = "Amulet",
				Necklace = "Necklace",
				Bracelet = "Bracelet",
			};
		}
	}
}
