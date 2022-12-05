using Roguelike.Core.Interfaces;
using Roguelike.Core.Localization;

namespace Roguelike.Core.Objects
{
	public class Fire : Object
	{
		#region Properties

		public override bool IsSolid
		{ get { return false; } }

		#endregion

		public Fire()
		{
			AddAspects(new FireSource());
		}

		public override string GetDescription(Language language, IAlive forWhom)
		{
			return language.Objects.Fire;
		}
	}
}
