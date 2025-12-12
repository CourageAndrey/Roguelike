using System.Xml.Serialization;

namespace Roguelike.Core.Localization.Items
{
	[XmlType]
	public class LanguageTools
	{
		#region Properties

		[XmlElement]
		public string Log
		{ get; set; }

		#endregion

		public static LanguageTools CreateDefault()
		{
			return new LanguageTools
			{
				Log = "Wooden log",
			};
		}
	}
}
