using Roguelike.Core.ActiveObjects;

namespace Roguelike.Console.ViewModels
{
	internal class HorseViewModel : ObjectViewModel<Horse>
	{
		public HorseViewModel(Horse o)
			: base(o)
		{ }

		#region Overrides

		public override string Text
		{ get { return "H"; } }

		public override System.ConsoleColor Foreground
		{ get { return Object.SkinColor.ToConsole(); } }

		#endregion
	}
}
