using System.Xml.Serialization;

namespace Roguelike.Core.Localization
{
	[XmlType]
	public class LanguageDirections
	{
		#region Properties

		[XmlElement]
		public string None
		{ get; set; }
		[XmlElement]
		public string Left
		{ get; set; }
		[XmlElement]
		public string Right
		{ get; set; }
		[XmlElement]
		public string Up
		{ get; set; }
		[XmlElement]
		public string Down
		{ get; set; }
		[XmlElement]
		public string UpLeft
		{ get; set; }
		[XmlElement]
		public string UpRight
		{ get; set; }
		[XmlElement]
		public string DownLeft
		{ get; set; }
		[XmlElement]
		public string DownRight
		{ get; set; }

		#endregion

		public static LanguageDirections CreateDefault()
		{
			return new LanguageDirections
			{
				None = "here",
				Left = "west",
				Right = "east",
				Up = "north",
				Down = "south",
				UpLeft = "north-west",
				UpRight = "north-east",
				DownLeft = "south-west",
				DownRight = "south-east",
			};
		}
	}
}
