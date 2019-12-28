using System.Collections.Generic;
using System.Drawing;

using Roguelike.Core;

namespace Roguelike.WpfClient.ViewModels
{
	internal class BackgroundViewModel : ObjectViewModel
	{
		#region Overrides

		public override string Text
		{ get { return "."; } }

		public override Color Foreground
		{ get { return brush; } }

		#endregion

		private readonly Color brush;

		private BackgroundViewModel(Color brush)
		{
			this.brush = brush;
		}

		#region List

		public static IDictionary<CellBackground, BackgroundViewModel> All = new Dictionary<CellBackground, BackgroundViewModel>
		{
			{ CellBackground.Grass, new BackgroundViewModel(Color.Lime) },
			{ CellBackground.Snow, new BackgroundViewModel(Color.White) },
			{ CellBackground.Floor, new BackgroundViewModel(Color.Silver) },
			{ CellBackground.Water, new BackgroundViewModel(Color.Blue) },
		};

		#endregion
	}
}
