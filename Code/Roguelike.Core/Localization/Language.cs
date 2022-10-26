using System.Xml.Serialization;

namespace Roguelike.Core.Localization
{
	[XmlType]
	public class Language
	{
		#region Properties

		[XmlIgnore]
		public string Name
		{ get; set; }

		[XmlIgnore]
		public LanguagePromts Promts
		{ get; set; }

		[XmlElement]
		public LanguageInteractions Interactions
		{ get; set; }

		[XmlElement]
		public string SelectDirectionsPromt
		{ get; set; }

		[XmlElement]
		public LanguageLogActionFormats LogActionFormats
		{ get; set; }

		[XmlElement]
		public LanguageActivities Activities
		{ get; set; }

		[XmlElement]
		public string DeathReasonKilled
		{ get; set; }

		[XmlElement]
		public LanguageUi Ui
		{ get; set; }

		[XmlElement]
		public string HelpTitle
		{ get; set; }

		[XmlElement]
		public string HelpText
		{ get; set; }

		[XmlElement]
		public LanguageDirections Directions
		{ get; set; }

		[XmlElement]
		public LanguageBodyParts BodyParts
		{ get; set; }

		[XmlElement]
		public string GameWin
		{ get; set; }

		[XmlElement]
		public string GameDefeat
		{ get; set; }

		[XmlElement]
		public LanguageItemTypes ItemTypes
		{ get; set; }

		[XmlElement]
		public LanguageTalk Talk
		{ get; set; }

		[XmlElement]
		public LanguageBooks Books
		{ get; set; }

		#endregion

		public static Language CreateDefault()
		{
			return new Language
			{
				Name = "English",

				Promts = LanguagePromts.CreateDefault(),

				SelectDirectionsPromt = "In which direction?",

				Interactions = LanguageInteractions.CreateDefault(),

				LogActionFormats = LanguageLogActionFormats.CreateDefault(),

				Activities = LanguageActivities.CreateDefault(),

				DeathReasonKilled = "killed by {0}",

				Ui = LanguageUi.CreateDefault(),

				HelpTitle = "Help",
				HelpText = @"Key bindings:

F1 - show help
F2 - show Character's menu

arrow keys & NumPad keys - move (or attack if agressive)

H - interact with object (sleep on bed, cook on fire, etc.)
F - change aggressive state
D - drop item
W - select weapon
C - chat
T - trade
P - pickpocket
B - backstab
R - read book
O - open/close (door)
, - pick item",

				Directions = LanguageDirections.CreateDefault(),

				BodyParts = LanguageBodyParts.CreateDefault(),

				GameWin = "Congratulations! You win the game.",
				GameDefeat = "Sorry, but game is over.",

				ItemTypes = LanguageItemTypes.CreateDefault(),

				Talk = LanguageTalk.CreateDefault(),

				Books = LanguageBooks.CreateDefault(),
			};
		}

		public override string ToString()
		{
			return Name;
		}
	}
}
