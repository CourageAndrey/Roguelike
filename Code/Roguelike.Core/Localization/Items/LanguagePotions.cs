using System.Xml.Serialization;

namespace Roguelike.Core.Localization.Items
{
	[XmlType]
	public class LanguagePotions
	{
		#region Properties

		[XmlElement]
		public string BottleOFWater
		{ get; set; }

		#endregion

		public static LanguagePotions CreateDefault()
		{
			return new LanguagePotions
			{
				BottleOFWater = "Bottle of plain water",
			};
		}
	}
}
