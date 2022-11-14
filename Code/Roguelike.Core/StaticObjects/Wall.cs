using Roguelike.Core.Interfaces;
using Roguelike.Core.Localization;

namespace Roguelike.Core.StaticObjects
{
	public class Wall : Object
	{
		#region Properties



		#endregion

		public override string GetDescription(Language language, IAlive forWhom)
		{
			return language.Objects.Wall;
		}
	}
}
