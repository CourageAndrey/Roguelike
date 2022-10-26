using Roguelike.Core.ActiveObjects;

namespace Roguelike.Console.ViewModels
{
	internal class DogViewModel : ObjectViewModel<Dog>
	{
		public DogViewModel(Dog o)
			: base(o)
		{ }

		#region Overrides

		public override string Text
		{ get { return "d"; } }

		public override System.ConsoleColor Foreground
		{ get { return System.ConsoleColor.Magenta; } }

		#endregion
	}
}
