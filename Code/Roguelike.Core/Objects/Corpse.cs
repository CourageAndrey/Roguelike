using System.Globalization;

using Roguelike.Core.Interfaces;
using Roguelike.Core.Localization;

namespace Roguelike.Core.Objects
{
	public class Corpse : Mechanics.Object
	{
		#region Properties

		public override bool IsSolid
		{ get { return false; } }

		public Alive Alive
		{ get; }

		#endregion

		public Corpse(Alive alive)
		{
			Alive = alive;
		}

		public override string GetDescription(Language language, IAlive forWhom)
		{
			return string.Format(CultureInfo.InvariantCulture, language.Objects.CorpseFormat, Alive.GetDescription(language, forWhom));
		}
	}
}
