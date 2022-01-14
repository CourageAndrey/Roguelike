using Roguelike.Core.StaticObjects;

namespace Roguelike.Console.ViewModels
{
	internal class DoorViewModel : ObjectViewModel<Door>
	{
		public DoorViewModel(Door o)
			: base(o)
		{ }

		#region Overrides

		public override string Text
		{ get { return Object.IsOpened ? "/" : "+"; } }

		public override System.ConsoleColor Foreground
		{ get { return System.ConsoleColor.DarkYellow; } }

		#endregion
	}
}
