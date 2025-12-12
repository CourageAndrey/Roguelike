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
		[XmlElement]
		public string Skin
		{ get; set; }
		[XmlElement]
		public string Fabric
		{ get; set; }
		[XmlElement]
		public string Paper
		{ get; set; }
		[XmlElement]
		public string Bone
		{ get; set; }
		[XmlElement]
		public string Food
		{ get; set; }
		[XmlElement]
		public string Liquid
		{ get; set; }

		[XmlElement]
		public LanguageMetals Metals
		{ get; set; }

		[XmlElement]
		public LanguageWoods Woods
		{ get; set; }

		[XmlElement]
		public LanguageStones Stones
		{ get; set; }

		[XmlElement]
		public LanguageSkins Skins
		{ get; set; }

		[XmlElement]
		public LanguageFabrics Fabrics
		{ get; set; }

		#endregion

		public static LanguageMaterials CreateDefault()
		{
			return new LanguageMaterials
			{
				Wood = "Wood",
				Metal = "Metal",
				Stone = "Stone",
				Skin = "Skin",
				Fabric = "Fabric",
				Paper = "Paper",
				Bone = "Bone",
				Food = "Food",
				Liquid = "Liquid",
				Metals = LanguageMetals.CreateDefault(),
				Woods = LanguageWoods.CreateDefault(),
				Stones = LanguageStones.CreateDefault(),
				Skins = LanguageSkins.CreateDefault(),
				Fabrics = LanguageFabrics.CreateDefault(),
			};
		}
	}
}
