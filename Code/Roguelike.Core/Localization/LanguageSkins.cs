using System.Xml.Serialization;

namespace Roguelike.Core.Localization
{
	[XmlType]
	public class LanguageSkins
	{
		#region Properties

		[XmlElement]
		public string Leather
		{ get; set; }

		[XmlElement]
		public string Hide
		{ get; set; }

		[XmlElement]
		public string Fur
		{ get; set; }

		[XmlElement]
		public string Scale
		{ get; set; }

		[XmlElement]
		public string Dragonhide
		{ get; set; }

		#endregion

		public static LanguageSkins CreateDefault()
		{
			return new LanguageSkins
			{
				Leather = "Leather",
				Hide = "Hide",
				Fur = "Fur",
				Scale = "Scale",
				Dragonhide = "Dragonhide",
			};
		}
	}
}

