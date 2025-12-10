using Roguelike.Core.Aspects;
using Roguelike.Core.Interfaces;
using Roguelike.Core.Localization;

namespace Roguelike.Core.Objects
{
	public class Bed : Mechanics.Object
	{
		#region Properties

		public override bool IsSolid
		{ get { return false; } }

		#endregion

		public Bed()
		{
			AddAspects(new SleepingArea());
		}

		public override string GetDescription(Language language, IAlive forWhom)
		{
			return language.Objects.Bed;
		}
	}
}
