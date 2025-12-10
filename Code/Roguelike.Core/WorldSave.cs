using System.Runtime.Serialization;
using System.Xml.Serialization;
using Roguelike.Core.Mechanics;
using Roguelike.Core.Saves;

namespace Roguelike.Core
{
	[KnownType(typeof(Version_1))]
	public abstract class WorldSave
	{
		[XmlIgnore]
		public abstract bool CanBeUpdated { get; }

		public abstract WorldSave UpdateToNewestVersion();

		public abstract World Load();
	}
}
