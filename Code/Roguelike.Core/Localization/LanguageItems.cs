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
		public string Log
		{ get; set; }
		[XmlElement]
		public string Hatchet
		{ get; set; }
		[XmlElement]
		public string Bow
		{ get; set; }
		[XmlElement]
		public string Book
		{ get; set; }
		[XmlElement]
		public string Arrow
		{ get; set; }
		[XmlElement]
		public string Unarmed
		{ get; set; }

		#endregion

		public static LanguageItems CreateDefault()
		{
			return new LanguageItems
			{
				ItemTypes = LanguageItemTypes.CreateDefault(),
				Trousers = "Trousers",
				Skirt = "Skirt",
				Shirt = "Shirt",
				Log = "Wooden log",
				Hatchet = "Hatchet",
				Bow = "Bow",
				Book = "Book",
				Arrow = "Arrow",
				Unarmed = "Bare hands (fight unarmed)",
			};
		}
	}
}
