using Roguelike.Core.ActiveObjects;

namespace Roguelike.Console.ViewModels
{
	internal class AnimalViewModel : ObjectViewModel<Animal>
	{
		public AnimalViewModel(Animal o)
			: base(o)
		{ }

		#region Overrides

		public override string Text
		{ get { return "a"; } }

		public override System.ConsoleColor Foreground
		{ get { return System.ConsoleColor.Magenta; } }

		#endregion
	}
}
