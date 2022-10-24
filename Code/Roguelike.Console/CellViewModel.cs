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
				Invalidate();
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
		private ConsoleColor? _lastForeColor;
		private ConsoleColor? _lastBackColor;
		private string _lastText;

		#endregion

		public CellViewModel(int x, int y)
		{
			X = x;
			Y = y;
		}

		private void Invalidate()
		{
			CurrentObjectView = null;
			_lastForeColor = _lastBackColor = null;
			_lastText = null;
		}

		private void cellViewChanged(Cell sender, bool transparencyChanged)
		{
			Invalidate();
			var camera = sender?.Region.World.Hero.Camera as Core.ActiveObjects.HeroCamera;
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

			ConsoleColor newForeColor;
			ConsoleColor newBackColor;
			if (IsVisible)
			{
				newForeColor = CurrentObjectView.Foreground;
				newBackColor = CurrentObjectView.Background;
			}
			else
			{
				newForeColor = CurrentObjectView.Foreground.ToGrayScale();
				newBackColor = CurrentObjectView.Background.ToGrayScale();
			}

			if (newForeColor != _lastForeColor || newBackColor != _lastBackColor || CurrentObjectView.Text != _lastText)
			{
				System.Console.CursorLeft = X;
				System.Console.CursorTop = Y;
				_lastForeColor = System.Console.ForegroundColor = newForeColor;
				_lastBackColor = System.Console.BackgroundColor = newBackColor;
				System.Console.Write(_lastText = CurrentObjectView.Text);
			}
		}
	}
}
