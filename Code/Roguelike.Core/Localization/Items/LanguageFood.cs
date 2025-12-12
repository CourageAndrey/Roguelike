using System.Xml.Serialization;

namespace Roguelike.Core.Localization.Items
{
	[XmlType]
	public class LanguageFood
	{
		#region Properties

		[XmlElement]
		public string LoafOfBread
		{ get; set; }

		[XmlElement]
		public string Apple
		{ get; set; }

		[XmlElement]
		public string Meat
		{ get; set; }

		[XmlElement]
		public string Cheese
		{ get; set; }

		[XmlElement]
		public string Fish
		{ get; set; }

		[XmlElement]
		public string Berries
		{ get; set; }

		#endregion

		public static LanguageFood CreateDefault()
		{
			return new LanguageFood
			{
				LoafOfBread = "Loaf of bread",
				Apple = "Apple",
				Meat = "Meat",
				Cheese = "Cheese",
				Fish = "Fish",
				Berries = "Berries",
			};
		}
	}
}
