using Roguelike.Core.Mechanics;

namespace Roguelike.Console
{
	internal abstract class ObjectViewModel
	{
		#region Methods to override

		public abstract string Text
		{ get; }

		public abstract System.ConsoleColor Foreground
		{ get; }

		public virtual System.ConsoleColor Background
		{ get { return DefaultBackground; } }

		public static readonly System.ConsoleColor DefaultBackground = System.ConsoleColor.Black;
		public static readonly System.ConsoleColor DefaultForeground = System.ConsoleColor.Black;
		public const string EmptyString = " ";

		public static readonly ObjectViewModel Empty = new EmptyObjectViewModel();

		#endregion
	}

	internal abstract class ObjectViewModel<ObjectT> : ObjectViewModel
		where ObjectT : Object
	{
		#region Properties

		public ObjectT Object
		{ get; }

		#endregion

		protected ObjectViewModel(ObjectT o)
		{
			Object = o;
		}
	}

	internal sealed class EmptyObjectViewModel : ObjectViewModel
	{
		public override string Text
		{ get { return EmptyString; } }

		public override System.ConsoleColor Foreground
		{ get { return DefaultForeground; } }
	}

	internal sealed class OverlayViewModel : ObjectViewModel
	{
		public override string Text
		{ get; }

		public override System.ConsoleColor Foreground
		{ get; }

		public override System.ConsoleColor Background
		{ get; }

		public OverlayViewModel(string text, System.ConsoleColor foreground, System.ConsoleColor background)
		{
			Text = text;
			Foreground = foreground;
			Background = background;
		}

		public static readonly OverlayViewModel Aim = new OverlayViewModel(ConsoleUiHelper.Aim, System.ConsoleColor.Yellow, System.ConsoleColor.DarkMagenta);
	}
}
