using System.Xml.Serialization;

namespace Roguelike.Core.Localization
{
	[XmlType]
	public class LanguageCreateHero
	{
		#region Properties

		[XmlElement]
		public string SelectRace
		{ get; set; }
		[XmlElement]
		public string InputSex
		{ get; set; }
		[XmlElement]
		public string InputAge
		{ get; set; }
		[XmlElement]
		public string InputName
		{ get; set; }
		[XmlElement]
		public string SelectProfession
		{ get; set; }

		#endregion

		public static LanguageCreateHero CreateDefault()
		{
			return new LanguageCreateHero
			{
				SelectRace = "Select your race (first - default):",
				InputSex = "Select your sex (F/f for female, otherwise - male):",
				InputAge = "Input your age (number):",
				InputName = "Input your name:",
				SelectProfession = "Select your profession (first - default):",
			};
		}
	}
}
