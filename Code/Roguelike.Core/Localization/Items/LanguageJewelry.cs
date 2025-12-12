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

		#endregion

		public static LanguageJewelry CreateDefault()
		{
			return new LanguageJewelry
			{
				Ring = "Ring",
			};
		}
	}
}
