using System.Xml.Serialization;

namespace Roguelike.Core.Localization.Items
{
	[XmlType]
	public class LanguageMeleeWeapons
	{
		#region Properties

		[XmlElement]
		public string Hatchet
		{ get; set; }

		[XmlElement]
		public string Sword
		{ get; set; }

		[XmlElement]
		public string Mace
		{ get; set; }

		[XmlElement]
		public string Spear
		{ get; set; }

		#endregion

		public static LanguageMeleeWeapons CreateDefault()
		{
			return new LanguageMeleeWeapons
			{
				Hatchet = "Hatchet",
				Sword = "Sword",
				Mace = "Mace",
				Spear = "Spear",
			};
		}
	}
}
