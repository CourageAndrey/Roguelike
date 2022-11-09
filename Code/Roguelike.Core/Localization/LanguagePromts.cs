using System.Xml.Serialization;

namespace Roguelike.Core.Localization
{
	[XmlType]
	public class LanguagePromts
	{
		#region Properties

		[XmlElement]
		public string SelectInteraction
		{ get; set; }
		[XmlElement]
		public string SelectWeapon
		{ get; set; }
		[XmlElement]
		public string SelectItemToDrop
		{ get; set; }
		[XmlElement]
		public string SelectItemToPick
		{ get; set; }
		[XmlElement]
		public string SelectItemToRead
		{ get; set; }
		[XmlElement]
		public string SelectItemToEat
		{ get; set; }
		[XmlElement]
		public string SelectItemToDrink
		{ get; set; }
		[XmlElement]
		public string SelectWear
		{ get; set; }

		#endregion

		public static LanguagePromts CreateDefault()
		{
			return new LanguagePromts
			{
				SelectInteraction = "Please, select what to do.",
				SelectWeapon = "Please, select weapon to equip.",
				SelectItemToDrop = "Please, select item to drop.",
				SelectItemToPick = "Please, select item to pick.",
				SelectItemToRead = "Please, select what to read.",
				SelectItemToEat = "Please, select what to eat.",
				SelectItemToDrink = "Please, select what to drink.",
				SelectWear = "Please, select what to equip.",
			};
		}
	}
}
