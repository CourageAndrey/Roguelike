using System.Xml.Serialization;

namespace Roguelike.Core.Localization
{
	[XmlType]
	public class LanguageItems
	{
		#region Properties

		[XmlElement]
		public LanguageItemTypes ItemTypes
		{ get; set; }

		#region Clothes

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

		#endregion

		#region Tools

		[XmlElement]
		public string Log
		{ get; set; }

		#endregion

		#region Food

		[XmlElement]
		public string LoafOfBread
		{ get; set; }

		#endregion

		#region Potions

		[XmlElement]
		public string BottleOFWater
		{ get; set; }

		#endregion

		#region Melee weapons

		[XmlElement]
		public string Hatchet
		{ get; set; }
		[XmlElement]
		public string Sword
		{ get; set; }
		[XmlElement]
		public string Mace
		{ get; set; }
		[XmlElement]
		public string Spear
		{ get; set; }

		#endregion

		#region Range weapons

		[XmlElement]
		public string Bow
		{ get; set; }

		#endregion

		#region Papers

		[XmlElement]
		public string Book
		{ get; set; }

		#endregion

		#region Missiles

		[XmlElement]
		public string Arrow
		{ get; set; }

		#endregion

		#region Jewelry

		[XmlElement]
		public string Ring
		{ get; set; }

		#endregion

		#region Special

		[XmlElement]
		public string Unarmed
		{ get; set; }

		[XmlElement]
		public string Grass
		{ get; set; }

		#endregion

		#endregion

		public static LanguageItems CreateDefault()
		{
			return new LanguageItems
			{
				ItemTypes = LanguageItemTypes.CreateDefault(),
				Trousers = "Trousers",
				Skirt = "Skirt",
				Shirt = "Shirt",
				Mantle = "Mantle",
				Gown = "Gown",
				HoodedCloak = "HoodedCloak",
				Log = "Wooden log",
				LoafOfBread = "Loaf of bread",
				BottleOFWater = "Bottle of plain water",
				Hatchet = "Hatchet",
				Sword = "Sword",
				Mace = "Mace",
				Spear = "Spear",
				Bow = "Bow",
				Book = "Book",
				Arrow = "Arrow",
				Unarmed = "Bare hands (fight unarmed)",
				Ring = "Ring",
				Grass = "Grass",
			};
		}
	}
}
