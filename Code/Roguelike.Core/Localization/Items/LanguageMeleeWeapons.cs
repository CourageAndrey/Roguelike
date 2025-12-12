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

		[XmlElement]
		public string Dagger
		{ get; set; }

		[XmlElement]
		public string Axe
		{ get; set; }

		[XmlElement]
		public string Club
		{ get; set; }

		[XmlElement]
		public string Warhammer
		{ get; set; }

		[XmlElement]
		public string Rapier
		{ get; set; }

		[XmlElement]
		public string Flail
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
				Dagger = "Dagger",
				Axe = "Axe",
				Club = "Club",
				Warhammer = "Warhammer",
				Rapier = "Rapier",
				Flail = "Flail",
			};
		}
	}
}
