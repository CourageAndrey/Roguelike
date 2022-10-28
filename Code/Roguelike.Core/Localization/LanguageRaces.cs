using System.Xml.Serialization;

namespace Roguelike.Core.Localization
{
	[XmlType]
	public class LanguageRaces
	{
		#region Properties

		//[XmlElement]
		//public string RACE
		//{ get; set; }

		#endregion

		public static LanguageRaces CreateDefault()
		{
			return new LanguageRaces
			{
			};
		}
	}
}
