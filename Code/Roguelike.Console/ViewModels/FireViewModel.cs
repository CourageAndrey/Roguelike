using Roguelike.Core.Objects;

namespace Roguelike.Console.ViewModels
{
	internal class FireViewModel : ObjectViewModel<Fire>
	{
		public FireViewModel(Fire o)
			: base(o)
		{ }

		#region Overrides

		public override string Text
		{ get { return "&"; } }

		public override System.ConsoleColor Foreground
		{ get { return System.ConsoleColor.Red; } }

		#endregion
	}
}
