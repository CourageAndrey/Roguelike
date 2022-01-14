using Roguelike.Core.StaticObjects;

namespace Roguelike.Console.ViewModels
{
	internal class CorpseViewModel : ObjectViewModel<Corpse>
	{
		public CorpseViewModel(Corpse o)
			: base(o)
		{ }

		#region Overrides

		public override string Text
		{ get { return "%"; } }

		public override System.ConsoleColor Foreground
		{ get { return System.ConsoleColor.DarkYellow; } }

		#endregion
	}
}
