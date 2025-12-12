using System.Xml.Serialization;

namespace Roguelike.Core.Localization.Items
{
	[XmlType]
	public class LanguageSpecial
	{
		#region Properties

		[XmlElement]
		public string Unarmed
		{ get; set; }

		[XmlElement]
		public string Grass
		{ get; set; }

		#endregion

		public static LanguageSpecial CreateDefault()
		{
			return new LanguageSpecial
			{
				Unarmed = "Bare hands (fight unarmed)",
				Grass = "Grass",
			};
		}
	}
}
