using System.Xml.Serialization;

namespace Roguelike.Core.Localization
{
	[XmlType]
	public class LanguageAnswerFormats
	{
		#region Properties

		[XmlElement]
		public string GetAcquainted
		{ get; set; }
		[XmlElement]
		public string AlreadyKnown
		{ get; set; }
		[XmlElement]
		public string Origination
		{ get; set; }
		[XmlElement]
		public string CurrentLocation
		{ get; set; }

		#endregion

		public static LanguageAnswerFormats CreateDefault()
		{
			return new LanguageAnswerFormats
			{
				GetAcquainted = "{0}My name is {1}. I'm {2} and have {3} years old. - And my name is {4}.",
				AlreadyKnown = "We've already got acquainted. ",
				Origination = "I'm from {0}.",
				CurrentLocation = "We are in {0} now.",
			};
		}
	}
}
