using System.Xml.Serialization;

namespace Roguelike.Core.Localization.Items
{
	[XmlType]
	public class LanguageFood
	{
		#region Properties

		[XmlElement]
		public string LoafOfBread
		{ get; set; }

		#endregion

		public static LanguageFood CreateDefault()
		{
			return new LanguageFood
			{
				LoafOfBread = "Loaf of bread",
			};
		}
	}
}
