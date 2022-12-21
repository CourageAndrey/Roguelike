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

		[XmlElement]
		public string AnyOtherKey
		{ get; set; }

		#endregion

		public static LanguageUi CreateDefault()
		{
			return new LanguageUi
			{
				Common = LanguageUiCommon.CreateDefault(),
				MainScreen = LanguageUiMainScreen.CreateDefault(),
				CharacterScreen = LanguageUiCharacter.CreateDefault(),
				AnyOtherKey = "any other key",
			};
		}
	}
}
