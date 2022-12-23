using System.Xml.Serialization;

namespace Roguelike.Core.Localization
{
	[XmlType]
	public class LanguageHaircuts
	{
		#region Properties

		[XmlElement]
		public string Bald
		{ get; set; }
		[XmlElement]
		public string LongHairs
		{ get; set; }
		[XmlElement]
		public string ShortHairs
		{ get; set; }

		#endregion

		public static LanguageHaircuts CreateDefault()
		{
			return new LanguageHaircuts
			{
				Bald = "bald",
				LongHairs = "long hairs",
				ShortHairs = "short hairs",
			};
		}
	}
}
