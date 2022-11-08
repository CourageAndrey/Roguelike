using System.Xml.Serialization;

namespace Roguelike.Core.Localization
{
	[XmlType]
	public class LanguageDeathReasons
	{
		#region Properties

		[XmlElement]
		public string Killed
		{ get; set; }

		[XmlElement]
		public string Hunger
		{ get; set; }

		[XmlElement]
		public string Thirst
		{ get; set; }

		[XmlElement]
		public string Overeating
		{ get; set; }

		#endregion

		public static LanguageDeathReasons CreateDefault()
		{
			return new LanguageDeathReasons
			{
				Killed = "killed by {0}",
				Hunger = "hunger",
				Thirst = "thirst",
				Overeating = "overeated",
			};
		}
	}
}
