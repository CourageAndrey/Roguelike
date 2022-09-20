using System;

using Roguelike.Core;
using Roguelike.Core.Interfaces;

namespace Roguelike.Console
{
	internal class CellViewModel
	{
		#region Properties

		public Cell Cell
		{
			get { return _cell; }
			set
			{
				if (_cell != null)
				{
					_cell.ViewChanged -= cellViewChanged;
				}
				_cell = value;
				CurrentObjectView = null;
				if (_cell != null)
				{
					_cell.ViewChanged += cellViewChanged;
				}
			}
		}

		public int X
		{ get; }

		public int Y
		{ get; }

		public bool IsVisible
		{ get; internal set; }

		public ObjectViewModel CurrentObjectView
		{ get; internal set; }

		private Cell _cell;

		#endregion

		public CellViewModel(int x, int y)
		{
			X = x;
			Y = y;
		}

		private void cellViewChanged(Cell sender)
		{
			CurrentObjectView = null;
			var hero = sender?.Region.World.Hero;
			if (hero != null)
			{
				hero.SelectVisibleCells();
				hero.RefreshCamera();
				if (hero.MapMemory.Contains(_cell) && IsVisible)
				{
					Update();
				}
			}
		}

		public void Update()
		{
			if (CurrentObjectView == null)
			{
				CurrentObjectView = _cell != null ? ConsoleUiHelper.GetModel(_cell) : ObjectViewModel.Empty;
			}

			System.Console.CursorLeft = X;
			System.Console.CursorTop = Y;
			System.Console.ForegroundColor = IsVisible ? CurrentObjectView.Foreground : ToGrayScale(CurrentObjectView.Foreground);
			System.Console.BackgroundColor = IsVisible ? CurrentObjectView.Background : ToGrayScale(CurrentObjectView.Background);
			System.Console.Write(CurrentObjectView.Text);
		}

		public static ConsoleColor ToGrayScale(ConsoleColor color)
		{
			switch (color)
			{
				case ConsoleColor.DarkBlue:
				case ConsoleColor.DarkGreen:
				case ConsoleColor.DarkCyan:
				case ConsoleColor.DarkRed:
				case ConsoleColor.DarkMagenta:
				case ConsoleColor.DarkYellow:
					return ConsoleColor.DarkGray;
				case ConsoleColor.Blue:
				case ConsoleColor.Green:
				case ConsoleColor.Cyan:
				case ConsoleColor.Red:
				case ConsoleColor.Magenta:
					return ConsoleColor.Gray;
				case ConsoleColor.Yellow:
					return ConsoleColor.White;
				case ConsoleColor.Black:
				case ConsoleColor.White:
				case ConsoleColor.Gray:
				case ConsoleColor.DarkGray:
				default:
					return color;
			}
		}
	}
}
