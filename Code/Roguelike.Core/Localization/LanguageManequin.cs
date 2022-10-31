using System.Xml.Serialization;

namespace Roguelike.Core.Localization
{
	[XmlType]
	public class LanguageManequin
	{
		#region Properties

		[XmlElement]
		public string HeadWear
		{ get; set; }

		[XmlElement]
		public string UpperBodyWear
		{ get; set; }

		[XmlElement]
		public string LowerBodyWear
		{ get; set; }

		[XmlElement]
		public string CoverWear
		{ get; set; }

		[XmlElement]
		public string HandsWear
		{ get; set; }

		[XmlElement]
		public string FootsWear
		{ get; set; }

		[XmlElement]
		public string Jewelry
		{ get; set; }

		#endregion

		public static LanguageManequin CreateDefault()
		{
			return new LanguageManequin
			{
				HeadWear = "Head",
				UpperBodyWear = "Upper body",
				LowerBodyWear = "Lower body",
				CoverWear = "Cover",
				HandsWear = "Hands",
				FootsWear = "Foots",
				Jewelry = "Jewelry",
			};
		}
	}
}
