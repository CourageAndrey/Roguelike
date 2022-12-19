using Roguelike.Core.Interfaces;
using Roguelike.Core.Localization;

namespace Roguelike.Core.Objects
{
	public abstract class Disease : IDisease
	{
		public abstract string GetName(LanguageDiseases language);
	}
}
