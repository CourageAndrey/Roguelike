using System.Collections.Generic;

using Roguelike.Core.Mechanics;

namespace Roguelike.Console.ViewModels
{
	internal class BackgroundViewModel : ObjectViewModel
	{
		#region Overrides

		public override string Text
		{ get { return "."; } }

		public override System.ConsoleColor Foreground
		{ get { return brush; } }

		#endregion

		private readonly System.ConsoleColor brush;

		private BackgroundViewModel(System.ConsoleColor brush)
		{
			this.brush = brush;
		}

		#region List

		public static IDictionary<CellBackground, BackgroundViewModel> All = new Dictionary<CellBackground, BackgroundViewModel>
		{
			{ CellBackground.Grass, new BackgroundViewModel(System.ConsoleColor.Green) },
			{ CellBackground.Snow, new BackgroundViewModel(System.ConsoleColor.White) },
			{ CellBackground.Floor, new BackgroundViewModel(System.ConsoleColor.Gray) },
			{ CellBackground.Sand, new BackgroundViewModel(System.ConsoleColor.Yellow) },
			{ CellBackground.Rock, new BackgroundViewModel(System.ConsoleColor.Gray) },
			{ CellBackground.Swamp, new BackgroundViewModel(System.ConsoleColor.Cyan) },
			{ CellBackground.Water, new BackgroundViewModel(System.ConsoleColor.Blue) },
		};

		#endregion
	}
}
