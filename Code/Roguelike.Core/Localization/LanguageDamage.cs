using System.Xml.Serialization;

namespace Roguelike.Core.Localization
{
	[XmlType]
	public class LanguageDamage
	{
		#region Properties

		[XmlElement]
		public string Piercing { get; set; }
		[XmlElement]
		public string Slashing { get; set; }
		[XmlElement]
		public string Chopping { get; set; }
		[XmlElement]
		public string Bludgeoning { get; set; }
		[XmlElement]
		public string Elemental { get; set; }

		#endregion

		public static LanguageDamage CreateDefault()
		{
			return new LanguageDamage
			{
				Piercing = "Piercing",
				Slashing = "Slashing",
				Chopping = "Chopping",
				Bludgeoning = "Bludgeoning",
				Elemental = "Elemental",
			};
		}
	}
}
