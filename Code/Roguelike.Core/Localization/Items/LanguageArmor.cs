using System.Xml.Serialization;

namespace Roguelike.Core.Localization.Items
{
	[XmlType]
	public class LanguageArmor
	{
		#region Properties

		[XmlElement]
		public string Helmet
		{ get; set; }

		[XmlElement]
		public string Chestplate
		{ get; set; }

		[XmlElement]
		public string Leggings
		{ get; set; }

		[XmlElement]
		public string Boots
		{ get; set; }

		[XmlElement]
		public string Gauntlets
		{ get; set; }

		[XmlElement]
		public string Shield
		{ get; set; }

		#endregion

		public static LanguageArmor CreateDefault()
		{
			return new LanguageArmor
			{
				Helmet = "Helmet",
				Chestplate = "Chestplate",
				Leggings = "Leggings",
				Boots = "Boots",
				Gauntlets = "Gauntlets",
				Shield = "Shield",
			};
		}
	}
}
