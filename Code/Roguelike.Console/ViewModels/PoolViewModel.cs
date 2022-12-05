using Roguelike.Core.Objects;

namespace Roguelike.Console.ViewModels
{
	internal class PoolViewModel : ObjectViewModel<Pool>
	{
		public PoolViewModel(Pool o)
			: base(o)
		{ }

		#region Overrides

		public override string Text
		{ get { return "0"; } }

		public override System.ConsoleColor Foreground
		{ get { return System.ConsoleColor.Blue; } }

		#endregion
	}
}
