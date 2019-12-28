using System.Xml.Serialization;

using Roguelike.Core.Saves;

namespace Roguelike.Core
{
	[XmlType]
	public class Save
	{
		[XmlElement("V_1", typeof(Version_1))]
		public WorldSave World
		{ get; set; }
	}
}
