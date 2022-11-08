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

		#endregion

		public static LanguageDeathReasons CreateDefault()
		{
			return new LanguageDeathReasons
			{
				Killed = "killed by {0}",
			};
		}
	}
}
