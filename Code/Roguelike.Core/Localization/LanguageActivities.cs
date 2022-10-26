using System.Xml.Serialization;

namespace Roguelike.Core.Localization
{
	[XmlType]
	public class LanguageActivities
	{
		#region Properties

		[XmlElement]
		public string Stands
		{ get; set; }
		[XmlElement]
		public string Guards
		{ get; set; }
		[XmlElement]
		public string Walks
		{ get; set; }
		[XmlElement]
		public string Fights
		{ get; set; }
		[XmlElement]
		public string ChopsTree
		{ get; set; }

		#endregion

		public static LanguageActivities CreateDefault()
		{
			return new LanguageActivities
			{
				Stands = string.Empty,
				Guards = "guards",
				Walks = "walks",
				Fights = "fights",
				ChopsTree = "chops tree",
			};
		}
	}
}
