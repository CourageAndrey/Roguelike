using Roguelike.Core.Objects;

namespace Roguelike.Console.ViewModels
{
	internal class WallViewModel : ObjectViewModel<Wall>
	{
		public WallViewModel(Wall o)
			: base(o)
		{ }

		#region Overrides

		public override string Text
		{ get { return "#"; } }

		public override System.ConsoleColor Foreground
		{ get { return System.ConsoleColor.Gray; } }

		#endregion
	}
}
