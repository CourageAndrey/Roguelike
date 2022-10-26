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
		public string InteractionFormat
		{ get; set; }

		[XmlElement]
		public LanguageInteractions Interactions
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

		#endregion

		public static Language CreateDefault()
		{
			return new Language
			{
				Name = "English",

				Promts = LanguagePromts.CreateDefault(),

				InteractionFormat = "{0} ({1})",

				Interactions = LanguageInteractions.CreateDefault(),

				LogActionFormats = LanguageLogActionFormats.CreateDefault(),

				Activities = LanguageActivities.CreateDefault(),

				DeathReasonKilled = "killed by {0}",

				Ui = LanguageUi.CreateDefault(),

				HelpTitle = "Help",
				HelpText = "Under construction...",

				Directions = LanguageDirections.CreateDefault(),

				BodyParts = LanguageBodyParts.CreateDefault(),

				GameWin = "Congratulations! You win the game.",
				GameDefeat = "Sorry, but game is over.",

				ItemTypes = LanguageItemTypes.CreateDefault(),

				Talk = LanguageTalk.CreateDefault(),
			};
		}

		public override string ToString()
		{
			return Name;
		}
	}
}
