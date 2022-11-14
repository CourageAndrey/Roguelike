using Roguelike.Core.Localization;

namespace Roguelike.Core.Interfaces
{
	public interface IDescriptive
	{
		string GetDescription(Language language, IAlive forWhom);
	}
}
