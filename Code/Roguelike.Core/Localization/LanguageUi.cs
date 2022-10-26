using System.Xml.Serialization;

namespace Roguelike.Core.Localization
{
	[XmlType]
	public class LanguageUi
	{
		#region Properties

		[XmlElement]
		public LanguageUiCommon Common
		{ get; set; }

		[XmlElement]
		public LanguageUiMainScreen MainScreen
		{ get; set; }

		[XmlElement]
		public LanguageUiCharacter CharacterScreen
		{ get; set; }

		#endregion

		public static LanguageUi CreateDefault()
		{
			return new LanguageUi
			{
				Common = LanguageUiCommon.CreateDefault(),
				MainScreen = LanguageUiMainScreen.CreateDefault(),
				CharacterScreen = LanguageUiCharacter.CreateDefault(),
			};
		}
	}
}
