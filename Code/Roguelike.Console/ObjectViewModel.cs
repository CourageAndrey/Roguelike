using Roguelike.Core;

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
		{ get { return " "; } }

		public override System.ConsoleColor Foreground
		{ get { return DefaultForeground; } }
	}
}
