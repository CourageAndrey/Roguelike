using System.Xml.Serialization;

namespace Roguelike.Core.Localization
{
	[XmlType]
	public class LanguageTraits
	{
		#region Properties

		#endregion

		public static LanguageTraits CreateDefault()
		{
			return new LanguageTraits
			{
				
			};
		}
	}
}
