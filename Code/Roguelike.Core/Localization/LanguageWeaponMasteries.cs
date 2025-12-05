using System.Xml.Serialization;

namespace Roguelike.Core.Localization
{
	[XmlType]
	public class LanguageWeaponMasteries
	{
		#region Properties

		[XmlElement]
		public string HandToHand { get; set; }
		[XmlElement]
		public string Blades { get; set; }
		[XmlElement]
		public string Maces { get; set; }
		[XmlElement]
		public string Polearms { get; set; }
		[XmlElement]
		public string Bows { get; set; }
		[XmlElement]
		public string Slings { get; set; }

		#endregion

		public static LanguageWeaponMasteries CreateDefault()
		{
			return new LanguageWeaponMasteries
			{
				HandToHand = "Hand-to-hand",
				Blades = "Blades",
				Maces = "Maces",
				Polearms = "Polearms",
				Bows = "Bows",
				Slings = "Slings",
			};
		}
	}
}
