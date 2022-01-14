using Roguelike.Core.StaticObjects;

namespace Roguelike.Console.ViewModels
{
	internal class BedViewModel : ObjectViewModel<Bed>
	{
		public BedViewModel(Bed o)
			: base(o)
		{ }

		#region Overrides

		public override string Text
		{ get { return "&"; } }

		public override System.ConsoleColor Foreground
		{ get { return System.ConsoleColor.DarkYellow; } }

		#endregion
	}
}
