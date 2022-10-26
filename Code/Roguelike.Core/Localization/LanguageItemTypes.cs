using System.Xml.Serialization;

namespace Roguelike.Core.Localization
{
	[XmlType]
	public class LanguageItemTypes
	{
		#region Properties

		[XmlElement]
		public string Weapon
		{ get; set; }
		[XmlElement]
		public string Wear
		{ get; set; }
		[XmlElement]
		public string Tool
		{ get; set; }
		[XmlElement]
		public string Food
		{ get; set; }

		#endregion

		public static LanguageItemTypes CreateDefault()
		{
			return new LanguageItemTypes
			{
				Weapon = "Weapon",
				Wear = "Wear",
				Food = "Food",
				Tool = "Tool",
			};
		}
	}
}
