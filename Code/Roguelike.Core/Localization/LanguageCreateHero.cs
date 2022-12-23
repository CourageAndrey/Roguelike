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
		[XmlElement]
		public string SelectHairColor
		{ get; set; }
		[XmlElement]
		public string SelectHaircut
		{ get; set; }

		#endregion

		public static LanguageCreateHero CreateDefault()
		{
			return new LanguageCreateHero
			{
				SelectRace = "Select your race (first - default):",
				InputSex = "Select your sex (F/f for female, otherwise - male):",
				InputAge = "Input your age (number, default is {0}):",
				InputName = "Input your name (default is {0}):",
				SelectProfession = "Select your profession (first - default):",
				SelectHairColor = "Select your hair color (first - default):",
				SelectHaircut = "Select your haircut (first - default):",
			};
		}
	}
}
