using Roguelike.Core.Interfaces;
using Roguelike.Core.Localization;

namespace Roguelike.Core.StaticObjects
{
	public class Bed : Object, ISleepingArea
	{
		#region Properties

		public override bool IsSolid
		{ get { return false; } }

		#endregion

		public override string GetDescription(Language language, IAlive forWhom)
		{
			return language.Objects.Bed;
		}
	}
}
