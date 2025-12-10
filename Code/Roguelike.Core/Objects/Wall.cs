using Roguelike.Core.Interfaces;
using Roguelike.Core.Localization;

namespace Roguelike.Core.Objects
{
	public class Wall : Mechanics.Object
	{
		#region Properties



		#endregion

		public override string GetDescription(Language language, IAlive forWhom)
		{
			return language.Objects.Wall;
		}
	}
}
