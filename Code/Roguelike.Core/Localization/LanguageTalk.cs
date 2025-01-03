﻿using System.Xml.Serialization;

namespace Roguelike.Core.Localization
{
	[XmlType]
	public class LanguageTalk
	{
		#region Properties

		[XmlElement]
		public LanguageQuestions Questions
		{ get; set; }

		[XmlElement]
		public LanguageAnswerFormats AnswerFormats
		{ get; set; }

		[XmlElement]
		public string KnownPersonFormat
		{ get; set; }

		[XmlElement]
		public string UnknownPersonFormat
		{ get; set; }

		#endregion

		public static LanguageTalk CreateDefault()
		{
			return new LanguageTalk
			{
				Questions = LanguageQuestions.CreateDefault(),
				AnswerFormats = LanguageAnswerFormats.CreateDefault(),
				KnownPersonFormat = "{2}, {0} {1}",
				UnknownPersonFormat = "Unknown {0} {1}",
			};
		}
	}
}
