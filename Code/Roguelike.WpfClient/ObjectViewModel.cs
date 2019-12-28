using System.Drawing;

using Roguelike.Core;

namespace Roguelike.WpfClient
{
	internal abstract class ObjectViewModel
	{
		#region Methods to override

		public abstract string Text
		{ get; }

		public abstract Color Foreground
		{ get; }

		public virtual Color Background
		{ get { return DefaultBackground; } }

		public virtual Font Font
		{ get { return DefaultFont; } }

		public static readonly Font DefaultFont  = new Font(new FontFamily("Courier New"), 12, FontStyle.Regular);
		public static readonly Color DefaultBackground = Color.Black;
		public static readonly Color DefaultForeground = Color.Black;

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

		public override Color Foreground
		{ get { return DefaultForeground; } }
	}
}
