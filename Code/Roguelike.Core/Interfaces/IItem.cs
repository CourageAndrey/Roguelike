using System.Drawing;

using Roguelike.Core.Localization;

namespace Roguelike.Core.Interfaces
{
	public interface IItem : IRequireGravitation
	{
		ItemType Type
		{ get; }

		Color Color
		{ get; }

		Material Material
		{ get; }

		string GetDescription(LanguageItems language, IAlive forWhom);

		event EventHandler<IItem, IAlive> Picked;

		event EventHandler<IItem, IAlive> Dropped;

		void RaisePicked(IAlive who);

		void RaiseDropped(IAlive who);
	}
}
