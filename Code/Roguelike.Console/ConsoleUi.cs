using System;
using System.Collections.Generic;
using System.Text;

using Roguelike.Core;
using Roguelike.Core.ActiveObjects;
using Roguelike.Core.Interfaces;

namespace Roguelike.Console
{
	public class ConsoleUi : IUserInterface
	{
		public ConsoleUi()
		{
			_screenWidth = System.Console.WindowWidth = System.Console.LargestWindowWidth;
			_screenHeight = System.Console.WindowHeight = System.Console.LargestWindowHeight;
			_halfScreenWidth = _screenWidth / 2;
			_halfScreenHeight = _screenHeight / 2;

			_cellViewModels = new CellViewModel[_screenWidth, _screenHeight];
			for (int r = 0; r < _screenHeight; r++)
			{
				for (int c = 0; c < _screenWidth; c++)
				{
					_cellViewModels[c, r] = new CellViewModel(c, r);
				}
			}

			System.Console.Clear();
			System.Console.CursorVisible = false;
		}

		#region DrawAPI

		private readonly int _screenWidth, _screenHeight, _halfScreenWidth, _halfScreenHeight;
		private int _worldXOfScreenLeft, _worldXOfScreenRight, _worldYOfScreenTop, _worldYOfScreenDown;
		private int _screenXOfCameraLeft, _screenXOfCameraRight, _screenYOfCameraTop, _screenYOfCameraDown;
		private readonly CellViewModel[,] _cellViewModels;
		private ICollection<Cell> _visibleCellsCache = new HashSet<Cell>();
		private ICamera _camera;

		public ICamera Camera
		{
			get { return _camera; }
			set
			{
				if (_camera != null)
				{
					_camera.Changed -= cameraChanged;
				}

				_camera = value;
				RedrawAll();

				if (_camera != null)
				{
					_camera.Changed += cameraChanged;
				}
			}
		}

		public void RedrawAll()
		{
			System.Console.Clear();
			if (_camera == null)
			{
				return;
			}

			updateWorldScreenBounds();
			updateCameraScreenBounds(_camera);

			foreach (var cellViewModel in _cellViewModels)
			{
				cellViewModel.Cell = null;
			}

			var cells = _camera.SelectRegionCells(_screenWidth, _screenHeight);
			_visibleCellsCache = _camera.SelectVisibleCells();
			Redraw(cells, 0, _screenHeight, 0, _screenWidth);
		}

		public void Redraw(Cell[][] cells, int minRow, int maxRow, int minColumn, int maxColumn)
		{
			var text = new StringBuilder();
			System.Console.ForegroundColor = ObjectViewModel.DefaultForeground;
			System.Console.BackgroundColor = ObjectViewModel.DefaultBackground;
			for (int r = minRow; r < maxRow; r++)
			{
				for (int c = minColumn; c < maxColumn; c++)
				{
					Cell currentCell;
					_cellViewModels[c, r].Cell = currentCell = cells[r][c];

					var cellObjectModel = currentCell != null && _camera.MapMemory.Contains(currentCell) ? ConsoleUiHelper.GetModel(currentCell) : ObjectViewModel.Empty;

					var currentForeground = cellObjectModel.Foreground;
					var currentBackground = cellObjectModel.Background;
					if (!_visibleCellsCache.Contains(currentCell))
					{
						currentForeground = CellViewModel.ToGrayScale(currentForeground);
						currentBackground = CellViewModel.ToGrayScale(currentBackground);
					}

					if (System.Console.ForegroundColor != currentForeground || System.Console.BackgroundColor != currentBackground)
					{
						if (text.Length > 0)
						{
							System.Console.Write(text);
							text.Clear();
						}
						System.Console.ForegroundColor = currentForeground;
						System.Console.BackgroundColor = currentBackground;
					}
					text.Append(cellObjectModel.Text);
				}
			}

			if (text.Length > 0)
			{
				System.Console.Write(text);
			}
		}

		private void cameraChanged(ICamera senderCamera)
		{
			if (isCameraNearScreenBounds(senderCamera))
			{
				RedrawAll();
			}
			else
			{
				var newVisibleCells = senderCamera.SelectVisibleCells();

				for (int x = _screenXOfCameraLeft; x <= _screenXOfCameraRight; x++)
				{
					for (int y = _screenYOfCameraTop; y <= _screenYOfCameraDown; y++)
					{
						var cellViewModel = _cellViewModels[x, y];
						if (!cellViewModel.IsVisible && newVisibleCells.Contains(cellViewModel.Cell))
						{
							cellViewModel.IsVisible = true;
							cellViewModel.Update();
						}
						else if (cellViewModel.IsVisible && !newVisibleCells.Contains(cellViewModel.Cell))
						{
							cellViewModel.IsVisible = false;
							cellViewModel.Update();
						}
					}
				}

				_visibleCellsCache = newVisibleCells;
			}
		}

		private bool isCameraNearScreenBounds(ICamera camera)
		{
			updateCameraScreenBounds(camera);

			return	_screenXOfCameraLeft <= 1 ||
					_screenXOfCameraRight >= _screenWidth - 2 ||
					_screenYOfCameraTop <= 1 ||
					_screenYOfCameraDown >= _screenHeight - 2;
		}

		private void updateWorldScreenBounds()
		{
			var cameraPosition = _camera.Cell.Position;

			_worldXOfScreenLeft = cameraPosition.X - _halfScreenWidth;
			_worldXOfScreenRight = cameraPosition.X + _halfScreenWidth;
			_worldYOfScreenTop = cameraPosition.Y + _halfScreenHeight;
			_worldYOfScreenDown = cameraPosition.Y - _halfScreenHeight;
		}

		private void updateCameraScreenBounds(ICamera camera)
		{
			int cameraDistance = (int) Math.Ceiling(_camera.Distance);
			var cameraPosition = camera.Cell.Position;

			_screenXOfCameraLeft = Math.Max(0, cameraPosition.X - _worldXOfScreenLeft - cameraDistance);
			_screenXOfCameraRight = Math.Min(_screenWidth - 1, cameraPosition.X - _worldXOfScreenLeft + cameraDistance);
			_screenYOfCameraTop = Math.Max(0, _worldYOfScreenTop - cameraPosition.Y - cameraDistance);
			_screenYOfCameraDown = Math.Min(_screenHeight - 1, _worldYOfScreenTop - cameraPosition.Y + cameraDistance);
		}

		#endregion

		#region Implementation of IUserInterface

		public void ShowMessage(string title, StringBuilder text)
		{
			throw new NotImplementedException();
		}

		public bool TrySelectItem(Game game, string question, IEnumerable<ListItem> items, out ListItem selectedItem)
		{
			throw new NotImplementedException();
		}

		public bool TrySelectItems(Game game, string question, IEnumerable<ListItem> items, out IList<ListItem> selectedItems)
		{
			throw new NotImplementedException();
		}

		public void ShowCharacter(Game game, Humanoid humanoid)
		{
			throw new NotImplementedException();
		}

		public ActionResult BeginChat(Game game, Humanoid humanoid)
		{
			throw new NotImplementedException();
		}

		public ActionResult BeginTrade(Game game, Humanoid humanoid)
		{
			throw new NotImplementedException();
		}

		public ActionResult BeginPickpocket(Game game, Humanoid humanoid)
		{
			throw new NotImplementedException();
		}

		#endregion
	}
}