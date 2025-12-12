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

		[XmlElement]
		public string HealingPotion
		{ get; set; }

		[XmlElement]
		public string ManaPotion
		{ get; set; }

		[XmlElement]
		public string StrengthPotion
		{ get; set; }

		#endregion

		public static LanguagePotions CreateDefault()
		{
			return new LanguagePotions
			{
				BottleOFWater = "Bottle of plain water",
				HealingPotion = "Healing potion",
				ManaPotion = "Mana potion",
				StrengthPotion = "Strength potion",
			};
		}
	}
}
