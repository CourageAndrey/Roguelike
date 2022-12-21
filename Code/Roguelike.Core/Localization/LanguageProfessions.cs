using System.Xml.Serialization;

namespace Roguelike.Core.Localization
{
	[XmlType]
	public class LanguageProfessions
	{
		#region Properties

		[XmlElement]
		public string Miller
		{ get; set; }
		[XmlElement]
		public string Baker
		{ get; set; }
		[XmlElement]
		public string Weaver
		{ get; set; }
		[XmlElement]
		public string Tailor
		{ get; set; }
		[XmlElement]
		public string Hunter
		{ get; set; }
		[XmlElement]
		public string Fisher
		{ get; set; }
		[XmlElement]
		public string Tanner
		{ get; set; }
		[XmlElement]
		public string Miner
		{ get; set; }
		[XmlElement]
		public string Smith
		{ get; set; }
		[XmlElement]
		public string Farmer
		{ get; set; }
		[XmlElement]
		public string WoodCutter
		{ get; set; }
		[XmlElement]
		public string Soldier
		{ get; set; }
		[XmlElement]
		public string Bandit
		{ get; set; }
		[XmlElement]
		public string Healer
		{ get; set; }
		[XmlElement]
		public string Monk
		{ get; set; }
		[XmlElement]
		public string Merchant
		{ get; set; }
		[XmlElement]
		public string Clerk
		{ get; set; }
		[XmlElement]
		public string Cook
		{ get; set; }
		[XmlElement]
		public string Everyman
		{ get; set; }

		#endregion

		public static LanguageProfessions CreateDefault()
		{
			return new LanguageProfessions
			{
				Miller = "Miller",
				Baker = "Baker",
				Weaver = "Weaver",
				Tailor = "Tailor",
				Hunter = "Hunter",
				Fisher = "Fisher",
				Tanner = "Tanner",
				Miner = "Miner",
				Smith = "Smith",
				Farmer = "Farmer",
				WoodCutter = "Wood cutter",
				Soldier = "Soldier",
				Bandit = "Bandit",
				Healer = "Healer",
				Monk = "Monk",
				Merchant = "Merchant",
				Clerk = "Clerk",
				Cook = "Cook",
				Everyman = "Everyman",
			};
		}
	}
}
