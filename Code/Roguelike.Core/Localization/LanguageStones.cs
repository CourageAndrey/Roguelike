using System.Xml.Serialization;

namespace Roguelike.Core.Localization
{
	[XmlType]
	public class LanguageStones
	{
		#region Properties

		[XmlElement]
		public string Granite
		{ get; set; }

		[XmlElement]
		public string Marble
		{ get; set; }

		[XmlElement]
		public string Limestone
		{ get; set; }

		[XmlElement]
		public string Sandstone
		{ get; set; }

		[XmlElement]
		public string Obsidian
		{ get; set; }

		[XmlElement]
		public string Quartz
		{ get; set; }

		[XmlElement]
		public string Basalt
		{ get; set; }

		[XmlElement]
		public string Slate
		{ get; set; }

		[XmlElement]
		public string Flint
		{ get; set; }

		[XmlElement]
		public string Jade
		{ get; set; }

		#endregion

		public static LanguageStones CreateDefault()
		{
			return new LanguageStones
			{
				Granite = "Granite",
				Marble = "Marble",
				Limestone = "Limestone",
				Sandstone = "Sandstone",
				Obsidian = "Obsidian",
				Quartz = "Quartz",
				Basalt = "Basalt",
				Slate = "Slate",
				Flint = "Flint",
				Jade = "Jade",
			};
		}
	}
}

