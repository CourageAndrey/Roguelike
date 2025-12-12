using System.Xml.Serialization;

namespace Roguelike.Core.Localization.Items
{
	[XmlType]
	public class LanguageRangeWeapons
	{
		#region Properties

		[XmlElement]
		public string Bow
		{ get; set; }

		[XmlElement]
		public string Crossbow
		{ get; set; }

		[XmlElement]
		public string Sling
		{ get; set; }

		#endregion

		public static LanguageRangeWeapons CreateDefault()
		{
			return new LanguageRangeWeapons
			{
				Bow = "Bow",
				Crossbow = "Crossbow",
				Sling = "Sling",
			};
		}
	}
}
