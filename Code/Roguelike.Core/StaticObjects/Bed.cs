using Roguelike.Core.Interfaces;
using Roguelike.Core.Localization;
using Roguelike.Core.Objects;

namespace Roguelike.Core.StaticObjects
{
	public class Bed : Object
	{
		#region Properties

		public override bool IsSolid
		{ get { return false; } }

		#endregion

		public Bed()
			: base(new IObjectAspect[] { new SleepingArea() })
		{ }

		public override string GetDescription(Language language, IAlive forWhom)
		{
			return language.Objects.Bed;
		}
	}
}
