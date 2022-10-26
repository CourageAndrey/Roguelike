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

		#endregion

		public static LanguagePromts CreateDefault()
		{
			return new LanguagePromts
			{
				SelectInteraction = "Please, select what to do.",
				SelectWeapon = "Please, select weapon to equip.",
				SelectItemToDrop = "Please, select item to drop.",
				SelectItemToPick = "Please, select item to pick.",
			};
		}
	}
}
