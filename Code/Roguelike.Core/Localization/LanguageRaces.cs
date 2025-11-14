using System.Xml.Serialization;

namespace Roguelike.Core.Localization
{
	[XmlType]
	public class LanguageRaces
	{
		#region Properties

		[XmlElement]
		public LanguageRace Plainsman
		{ get; set; }

		[XmlElement]
		public LanguageRace Nomad
		{ get; set; }

		[XmlElement]
		public LanguageRace Highlander
		{ get; set; }

		[XmlElement]
		public LanguageRace Jungleman
		{ get; set; }

		[XmlElement]
		public LanguageRace Nordman
		{ get; set; }

		#endregion

		public static LanguageRaces CreateDefault()
		{
			return new LanguageRaces
			{
				Plainsman = LanguageRace.CreatePlainsman(),
				Nomad = LanguageRace.CreateNomad(),
				Highlander = LanguageRace.CreateHighlander(),
				Jungleman = LanguageRace.CreateJungleman(),
				Nordman = LanguageRace.CreateNordman(),
			};
		}
	}
}
