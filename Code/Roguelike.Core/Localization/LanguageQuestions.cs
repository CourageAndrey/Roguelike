using System.Xml.Serialization;

namespace Roguelike.Core.Localization
{
	[XmlType]
	public class LanguageQuestions
	{
		#region Properties

		[XmlElement]
		public string WhatIsYourName
		{ get; set; }
		[XmlElement]
		public string HowOldAreYou
		{ get; set; }
		[XmlElement]
		public string WhatDoYouDo
		{ get; set; }
		[XmlElement]
		public string WhereAreWeNow
		{ get; set; }
		[XmlElement]
		public string WhereAreYouFrom
		{ get; set; }

		#endregion

		public static LanguageQuestions CreateDefault()
		{
			return new LanguageQuestions
			{
				WhatIsYourName = "What is your name?",
				HowOldAreYou = "How old are you?",
				WhatDoYouDo = "What do you do?",
				WhereAreWeNow = "Where are we now?",
				WhereAreYouFrom = "Where are you from?",
			};
		}
	}
}
