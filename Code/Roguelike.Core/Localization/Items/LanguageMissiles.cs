using System.Xml.Serialization;

namespace Roguelike.Core.Localization.Items
{
	[XmlType]
	public class LanguageMissiles
	{
		#region Properties

		[XmlElement]
		public string Arrow
		{ get; set; }

		[XmlElement]
		public string Bolt
		{ get; set; }

		[XmlElement]
		public string Bullet
		{ get; set; }

		#endregion

		public static LanguageMissiles CreateDefault()
		{
			return new LanguageMissiles
			{
				Arrow = "Arrow",
				Bolt = "Bolt",
				Bullet = "Sling bullet",
			};
		}
	}
}
