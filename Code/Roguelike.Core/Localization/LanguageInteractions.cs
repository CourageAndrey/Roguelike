using System.Xml.Serialization;

namespace Roguelike.Core.Localization
{
	[XmlType]
	public class LanguageInteractions
	{
		#region Properties

		[XmlElement]
		public string ChopTree
		{ get; set; }

		#endregion

		public static LanguageInteractions CreateDefault()
		{
			return new LanguageInteractions
			{
				ChopTree = "Chop the tree",
			};
		}
	}
}
