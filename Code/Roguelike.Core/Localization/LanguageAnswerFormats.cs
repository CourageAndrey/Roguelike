using System.Xml.Serialization;

namespace Roguelike.Core.Localization
{
	[XmlType]
	public class LanguageAnswerFormats
	{
		#region Properties

		[XmlElement]
		public string NameAgain
		{ get; set; }
		[XmlElement]
		public string NameFirst
		{ get; set; }
		[XmlElement]
		public string Age
		{ get; set; }

		#endregion

		public static LanguageAnswerFormats CreateDefault()
		{
			return new LanguageAnswerFormats
			{
				NameAgain = "As I've said before, my name is {0}.",
				NameFirst = "My name is {0}. - Nice to get aquainted, my name is {1}.",
				Age = "I'm {0} years old.",
			};
		}
	}
}
