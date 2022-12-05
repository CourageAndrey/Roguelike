using Roguelike.Core.Objects;

namespace Roguelike.Console.ViewModels
{
	internal class StumpViewModel : ObjectViewModel<Stump>
	{
		public StumpViewModel(Stump o)
			: base(o)
		{ }

		#region Overrides

		public override string Text
		{ get { return "т"; } }

		public override System.ConsoleColor Foreground
		{ get { return System.ConsoleColor.DarkGray; } }

		#endregion
	}
}
