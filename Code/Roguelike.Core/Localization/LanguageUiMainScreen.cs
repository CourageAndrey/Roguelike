using System.Xml.Serialization;

namespace Roguelike.Core.Localization
{
	[XmlType]
	public class LanguageUiMainScreen
	{
		#region Properties

		[XmlElement]
		public string NewGame
		{ get; set; }
		[XmlElement]
		public string LoadGame
		{ get; set; }
		[XmlElement]
		public string Help
		{ get; set; }
		[XmlElement]
		public string Exit
		{ get; set; }
		[XmlElement]
		public string SelectSave
		{ get; set; }
		[XmlElement]
		public string SavesFilter
		{ get; set; }

		#endregion

		public static LanguageUiMainScreen CreateDefault()
		{
			return new LanguageUiMainScreen
			{
				NewGame = "Start new game",
				LoadGame = "Load saved game",
				Help = "Show help",
				Exit = "Exit",
				SelectSave = "Please, choose save file name",
				SavesFilter = "SaveFiles|*.xml",
			};
		}
	}
}
