using Roguelike.Core.Objects;

namespace Roguelike.Console.ViewModels
{
	internal class TreeViewModel : ObjectViewModel<Tree>
	{
		public TreeViewModel(Tree o)
			: base(o)
		{ }

		#region Overrides

		public override string Text
		{ get { return "T"; } }

		public override System.ConsoleColor Foreground
		{ get { return System.ConsoleColor.Green; } }

		#endregion
	}
}
