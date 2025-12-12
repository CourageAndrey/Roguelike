using System.Xml.Serialization;

namespace Roguelike.Core.Localization.Items
{
	[XmlType]
	public class LanguagePapers
	{
		#region Properties

		[XmlElement]
		public string Book
		{ get; set; }

		#endregion

		public static LanguagePapers CreateDefault()
		{
			return new LanguagePapers
			{
				Book = "Book",
			};
		}
	}
}
