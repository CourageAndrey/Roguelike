using System;
using System.Xml.Serialization;

namespace Roguelike.Core.Saves
{
	[XmlType]
	public class Version_1 : WorldSave
	{
		#region Constructors

		public Version_1()
		{ }

		public Version_1(World world)
		{
#warning Implement save/load
			throw new NotImplementedException();
		}

		#endregion

		#region Implementation of ISave

		[XmlIgnore]
		public override bool CanBeUpdated
		{ get { return false; } }

		public override WorldSave UpdateToNewestVersion()
		{
			throw new NotSupportedException();
		}

		public override World Load()
		{
#warning Implement save/load
			throw new NotImplementedException();
		}

		#endregion
	}
}
