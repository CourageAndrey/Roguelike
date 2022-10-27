using System.Xml.Serialization;

namespace Roguelike.Core.Localization
{
	[XmlType]
	public class LanguageUiCharacter
	{
		#region Properties

		[XmlElement]
		public string General
		{ get; set; }
		[XmlElement]
		public string Body
		{ get; set; }
		[XmlElement]
		public string State
		{ get; set; }
		[XmlElement]
		public string Stats
		{ get; set; }
		[XmlElement]
		public string Skills
		{ get; set; }
		[XmlElement]
		public string WearedItems
		{ get; set; }
		[XmlElement]
		public string Inventory
		{ get; set; }

		#endregion

		public static LanguageUiCharacter CreateDefault()
		{
			return new LanguageUiCharacter
			{
				General = "Character",
				Body = "Body",
				State = "State",
				Stats = "Stats",
				Skills = "Skills",
				WearedItems = "Weared Items",
				Inventory = "Inventory",
			};
		}
	}
}
