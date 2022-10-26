using System.Xml.Serialization;

namespace Roguelike.Core.Localization
{
	[XmlType]
	public class LanguageBooks
	{
		#region Properties

		[XmlElement]
		public string HelloWorld
		{ get; set; }

		#endregion

		public static LanguageBooks CreateDefault()
		{
			return new LanguageBooks
			{
				HelloWorld = "Hello world!",
			};
		}
	}
}
