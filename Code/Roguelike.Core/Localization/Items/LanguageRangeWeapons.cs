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

		[XmlElement]
		public string Longbow
		{ get; set; }

		[XmlElement]
		public string Shortbow
		{ get; set; }

		[XmlElement]
		public string HeavyCrossbow
		{ get; set; }

		#endregion

		public static LanguageRangeWeapons CreateDefault()
		{
			return new LanguageRangeWeapons
			{
				Bow = "Bow",
				Crossbow = "Crossbow",
				Sling = "Sling",
				Longbow = "Longbow",
				Shortbow = "Shortbow",
				HeavyCrossbow = "Heavy crossbow",
			};
		}
	}
}
