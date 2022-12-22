using System.Xml.Serialization;

namespace Roguelike.Core.Localization
{
	[XmlType]
	public class LanguageLoadSave
	{
		#region Properties

		[XmlElement]
		public string NoSaves
		{ get; set; }

		[XmlElement]
		public string SelectPromt
		{ get; set; }

		#endregion

		public static LanguageLoadSave CreateDefault()
		{
			return new LanguageLoadSave
			{
				NoSaves = "There is no saves found. Create new character instead.",
				SelectPromt = "Select save file... (first by default)",
			};
		}
	}
}
