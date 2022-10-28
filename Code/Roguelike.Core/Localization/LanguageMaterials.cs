using System.Xml.Serialization;

namespace Roguelike.Core.Localization
{
	[XmlType]
	public class LanguageMaterials
	{
		#region Properties

		[XmlElement]
		public string Wood
		{ get; set; }
		[XmlElement]
		public string Metal
		{ get; set; }
		[XmlElement]
		public string Stone
		{ get; set; }

		#endregion

		public static LanguageMaterials CreateDefault()
		{
			return new LanguageMaterials
			{
				Wood = "Wood",
				Metal = "Metal",
				Stone = "Stone",
			};
		}
	}
}
