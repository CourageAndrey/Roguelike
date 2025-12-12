using System.Xml.Serialization;

namespace Roguelike.Core.Localization.Items
{
	[XmlType]
	public class LanguageTools
	{
		#region Properties

		[XmlElement]
		public string Log
		{ get; set; }

		[XmlElement]
		public string Pickaxe
		{ get; set; }

		[XmlElement]
		public string Hammer
		{ get; set; }

		[XmlElement]
		public string Shovel
		{ get; set; }

		[XmlElement]
		public string Torch
		{ get; set; }

		[XmlElement]
		public string Rope
		{ get; set; }

		#endregion

		public static LanguageTools CreateDefault()
		{
			return new LanguageTools
			{
				Log = "Wooden log",
				Pickaxe = "Pickaxe",
				Hammer = "Hammer",
				Shovel = "Shovel",
				Torch = "Torch",
				Rope = "Rope",
			};
		}
	}
}
