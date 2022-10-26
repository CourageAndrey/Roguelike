using System.Xml.Serialization;

namespace Roguelike.Core.Localization
{
	[XmlType]
	public class LanguageInteractions
	{
		#region Properties

		[XmlElement]
		public string OpenDoor
		{ get; set; }
		[XmlElement]
		public string CloseDoor
		{ get; set; }
		[XmlElement]
		public string ChopTree
		{ get; set; }
		[XmlElement]
		public string Backstab
		{ get; set; }
		[XmlElement]
		public string Chat
		{ get; set; }
		[XmlElement]
		public string Trade
		{ get; set; }
		[XmlElement]
		public string Pickpocket
		{ get; set; }
		[XmlElement]
		public string PickItem
		{ get; set; }

		#endregion

		public static LanguageInteractions CreateDefault()
		{
			return new LanguageInteractions
			{
				OpenDoor = "Open the door.",
				CloseDoor = "Close the door",
				ChopTree = "Chop the tree",
				Backstab = "Backstab",
				Chat = "Chat",
				Trade = "Trade",
				Pickpocket = "Pickpocket",
				PickItem = "Pick item",
			};
		}
	}
}
