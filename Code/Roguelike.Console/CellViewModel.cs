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

		public void Invalidate()
		{
			CurrentObjectView = null;
			_lastForeColor = _lastBackColor = null;
			_lastText = null;
		}

		private void cellViewChanged(Cell sender, bool transparencyChanged)
		{
			var camera = _cell.Region.World.Hero.Camera;
			bool isVisible;
			if (camera != null && camera.VisibleCells.TryGetValue(_cell, out isVisible) && isVisible)
			{
				Invalidate();

				if (transparencyChanged)
				{
					camera.RefreshVisibleCells();
				}

				Update(camera);
			}
		}

		public void Update(ICamera camera)
		{
			if (_cell == null || !camera.MapMemory.Contains(_cell)) return;

			if (CurrentObjectView == null)
			{
				CurrentObjectView = _cell.GetModel();
			}

			bool isVisible;
			if (Draw(CurrentObjectView, camera.VisibleCells.TryGetValue(_cell, out isVisible) && isVisible))
			{
				_lastForeColor = System.Console.ForegroundColor;
				_lastBackColor = System.Console.BackgroundColor;
				_lastText = CurrentObjectView.Text;
			}
		}

		private bool Draw(ObjectViewModel objectViewModel, bool isVisible)
		{
			ConsoleColor newForeColor;
			ConsoleColor newBackColor;
			if (isVisible)
			{ 
				newForeColor = objectViewModel.Foreground;
				newBackColor = objectViewModel.Background;
			}
			else
			{
				newForeColor = objectViewModel.Foreground.ToGrayScale();
				newBackColor = objectViewModel.Background.ToGrayScale();
			}

			if (newForeColor != _lastForeColor || newBackColor != _lastBackColor || objectViewModel.Text != _lastText)
			{
				System.Console.CursorLeft = X;
				System.Console.CursorTop = Y;
				System.Console.ForegroundColor = newForeColor;
				System.Console.BackgroundColor = newBackColor;
				System.Console.Write(objectViewModel.Text);
				return true;
			}
			else
			{
				return false;
			}
		}

		public void SetOverlay(OverlayViewModel overlay)
		{
			Draw(overlay, true);
		}

		public void ResetOverlay(ICamera camera)
		{
			Invalidate();
			Update(camera);
		}
	}

	internal static class CellViewModelExtensions
	{
		public static void SetOverlay(this CellViewModel cellViewModel, ConsoleColor color, string text)
		{
			cellViewModel.SetOverlay(new OverlayViewModel(
				text,
				color,
				cellViewModel.CurrentObjectView != null ? cellViewModel.CurrentObjectView.Background : ObjectViewModel.DefaultBackground));
		}
	}
}
