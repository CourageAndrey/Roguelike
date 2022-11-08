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
		public LanguageDeathReasons DeathReasons
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
		public LanguageCharacter Character
		{ get; set; }

		[XmlElement]
		public string GameWin
		{ get; set; }

		[XmlElement]
		public string GameDefeat
		{ get; set; }

		[XmlElement]
		public LanguageItems Items
		{ get; set; }

		[XmlElement]
		public LanguageTalk Talk
		{ get; set; }

		[XmlElement]
		public LanguageBooks Books
		{ get; set; }

		[XmlElement]
		public LanguageMaterials Materials
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

				DeathReasons = LanguageDeathReasons.CreateDefault(),

				Ui = LanguageUi.CreateDefault(),

				HelpTitle = "Help",
				HelpText = @"Key bindings:

F1 - show help
F2 - show Character's menu
F3 - show Character's equipment
F4 - show Character's inventory

arrow keys & NumPad keys - move (or attack if agressive)

h - interact with object (sleep on bed, cook on fire, etc.)
f - change aggressive state
d - drop item
w - select weapon
c - chat
t - shoot
T - trade
p - pickpocket
b - backstab
r - read book
o - open/close (door)
s - ride (horse)
, - pick item",

				Directions = LanguageDirections.CreateDefault(),

				Character = LanguageCharacter.CreateDefault(),

				GameWin = "Congratulations! You win the game.",
				GameDefeat = "Sorry, but game is over.",

				Items = LanguageItems.CreateDefault(),

				Talk = LanguageTalk.CreateDefault(),

				Books = LanguageBooks.CreateDefault(),

				Materials = LanguageMaterials.CreateDefault(),
			};
		}

		public override string ToString()
		{
			return Name;
		}
	}
}
