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
			var camera = sender?.Region.World.Hero.Camera;
			if (camera != null)
			{
				camera.SelectVisibleCells();
				camera.Refresh();
				if (camera.MapMemory.Contains(_cell) && IsVisible)
				{
					Update();
				}
			}
		}

		public void Update()
		{
			if (CurrentObjectView == null)
			{
				CurrentObjectView = _cell != null ? _cell.GetModel() : ObjectViewModel.Empty;
			}

			System.Console.CursorLeft = X;
			System.Console.CursorTop = Y;
			System.Console.ForegroundColor = IsVisible ? CurrentObjectView.Foreground : CurrentObjectView.Foreground.ToGrayScale();
			System.Console.BackgroundColor = IsVisible ? CurrentObjectView.Background : CurrentObjectView.Background.ToGrayScale();
			System.Console.Write(CurrentObjectView.Text);
		}
	}
}
