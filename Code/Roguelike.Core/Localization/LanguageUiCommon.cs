using System.Xml.Serialization;

namespace Roguelike.Core.Localization
{
	[XmlType]
	public class LanguageUiCommon
	{
		#region Properties

		[XmlElement]
		public string Ok
		{ get; set; }
		[XmlElement]
		public string Cancel
		{ get; set; }

		#endregion

		public static LanguageUiCommon CreateDefault()
		{
			return new LanguageUiCommon
			{
				Ok = "OK",
				Cancel = "Cancel",
			};
		}
	}
}
