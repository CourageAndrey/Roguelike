using System.Xml.Serialization;

namespace Roguelike.Core.Localization
{
	[XmlType]
	public class LanguageFabrics
	{
		#region Properties

		[XmlElement]
		public string Cotton
		{ get; set; }

		[XmlElement]
		public string Wool
		{ get; set; }

		[XmlElement]
		public string Silk
		{ get; set; }

		[XmlElement]
		public string Linen
		{ get; set; }

		[XmlElement]
		public string Canvas
		{ get; set; }

		[XmlElement]
		public string Velvet
		{ get; set; }

		[XmlElement]
		public string Satin
		{ get; set; }

		[XmlElement]
		public string Burlap
		{ get; set; }

		[XmlElement]
		public string Denim
		{ get; set; }

		#endregion

		public static LanguageFabrics CreateDefault()
		{
			return new LanguageFabrics
			{
				Cotton = "Cotton",
				Wool = "Wool",
				Silk = "Silk",
				Linen = "Linen",
				Canvas = "Canvas",
				Velvet = "Velvet",
				Satin = "Satin",
				Burlap = "Burlap",
				Denim = "Denim",
			};
		}
	}
}

