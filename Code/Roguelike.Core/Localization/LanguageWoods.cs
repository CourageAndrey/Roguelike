using System.Xml.Serialization;

namespace Roguelike.Core.Localization
{
	[XmlType]
	public class LanguageWoods
	{
		#region Properties

		[XmlElement]
		public string Oak
		{ get; set; }

		[XmlElement]
		public string Pine
		{ get; set; }

		[XmlElement]
		public string Birch
		{ get; set; }

		[XmlElement]
		public string Maple
		{ get; set; }

		[XmlElement]
		public string Ash
		{ get; set; }

		[XmlElement]
		public string Cedar
		{ get; set; }

		[XmlElement]
		public string Willow
		{ get; set; }

		[XmlElement]
		public string Yew
		{ get; set; }

		[XmlElement]
		public string Ebony
		{ get; set; }

		[XmlElement]
		public string Mahogany
		{ get; set; }

		#endregion

		public static LanguageWoods CreateDefault()
		{
			return new LanguageWoods
			{
				Oak = "Oak",
				Pine = "Pine",
				Birch = "Birch",
				Maple = "Maple",
				Ash = "Ash",
				Cedar = "Cedar",
				Willow = "Willow",
				Yew = "Yew",
				Ebony = "Ebony",
				Mahogany = "Mahogany",
			};
		}
	}
}

