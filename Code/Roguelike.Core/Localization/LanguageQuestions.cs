using System.Xml.Serialization;

namespace Roguelike.Core.Localization
{
	[XmlType]
	public class LanguageQuestions
	{
		#region Properties

		[XmlElement]
		public string WhoAreYou
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
				WhoAreYou = "Who are you?",
				WhereAreWeNow = "Where are we now?",
				WhereAreYouFrom = "Where are you from?",
			};
		}
	}
}
