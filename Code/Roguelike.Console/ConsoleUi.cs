using System;
using System.Collections.Generic;
using System.Text;

using Roguelike.Core;
using Roguelike.Core.Interfaces;

namespace Roguelike.Console
{
	public class ConsoleUi : IUserInterface
	{
		public ConsoleUi()
		{
			System.Console.OutputEncoding = Encoding.UTF8;

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
		private ICamera _camera;

		public ICamera Camera
		{
			get { return _camera; }
			set
			{
				if (_camera != null)
				{
					_camera.CellsVisibilityChanged -= cameraCellsVisibilityChanged;
				}

				_camera = value;
				_camera.RefreshVisibleCells();
				RedrawAll();

				if (_camera != null)
				{
					_camera.CellsVisibilityChanged += cameraCellsVisibilityChanged;
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
					ObjectViewModel cellObjectModel;
					ConsoleColor currentForeground;
					ConsoleColor currentBackground;

					_cellViewModels[c, r].Cell = currentCell = cells[r][c];

					if (currentCell != null && _camera.MapMemory.Contains(currentCell))
					{
						cellObjectModel = currentCell.GetModel();

						bool isVisible;
						if (_camera.VisibleCells.TryGetValue(currentCell, out isVisible) && isVisible)
						{
							currentForeground = cellObjectModel.Foreground;
							currentBackground = cellObjectModel.Background;
						}
						else
						{
							currentForeground = cellObjectModel.Foreground.ToGrayScale();
							currentBackground = cellObjectModel.Background.ToGrayScale();
						}
					}
					else
					{
						cellObjectModel = ObjectViewModel.Empty;
						currentForeground = cellObjectModel.Foreground;
						currentBackground = cellObjectModel.Background;
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

		private void cameraCellsVisibilityChanged(ICamera senderCamera, IDictionary<Cell, bool> delta)
		{
			if (isCameraNearScreenBounds(senderCamera))
			{
				RedrawAll();
			}
			else
			{
				for (int x = _screenXOfCameraLeft; x <= _screenXOfCameraRight; x++)
				{
					for (int y = _screenYOfCameraTop; y <= _screenYOfCameraDown; y++)
					{
						var cellModel = _cellViewModels[x, y];
						if (cellModel.Cell != null && delta.ContainsKey(cellModel.Cell))
						{
							cellModel.Invalidate();
							cellModel.Update(senderCamera);
						}
					}
				}
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

			_screenXOfCameraLeft = Math.Max(0, cameraPosition.X - _worldXOfScreenLeft - cameraDistance - 1);
			_screenXOfCameraRight = Math.Min(_screenWidth - 1, cameraPosition.X - _worldXOfScreenLeft + cameraDistance + 1);
			_screenYOfCameraTop = Math.Max(0, _worldYOfScreenTop - cameraPosition.Y - cameraDistance - 1);
			_screenYOfCameraDown = Math.Min(_screenHeight - 1, _worldYOfScreenTop - cameraPosition.Y + cameraDistance + 1);
		}

		#endregion

		#region Implementation of IUserInterface

		private void startDialog(Action code)
		{
			System.Console.Clear();
			System.Console.CursorVisible = true;
			System.Console.CursorTop = 0;
			System.Console.CursorLeft = 0;
			System.Console.ForegroundColor = ConsoleColor.White;
			System.Console.BackgroundColor = ConsoleColor.Black;

			code();

			System.Console.CursorTop = 0;
			System.Console.CursorLeft = 0;
			System.Console.CursorVisible = false;
			RedrawAll();
		}

		public void ShowMessage(string title, StringBuilder text)
		{
			startDialog(() =>
			{
				if (!string.IsNullOrEmpty(title))
				{
					System.Console.WriteLine($"=== {title} ===");
					System.Console.WriteLine();
				}

				System.Console.Write(text);
				System.Console.ReadKey(true);
			});
		}

		public bool TrySelectItem(Game game, string question, IEnumerable<ListItem> items, out ListItem selectedItem)
		{
			bool result = false;
			ListItem selected = null;

			startDialog(() =>
			{
				System.Console.WriteLine(question);
				System.Console.WriteLine();

				var itemsList = new List<ListItem>(items);
				int index = 1;
				foreach (var item in itemsList)
				{
					System.Console.ForegroundColor = item.IsAvailable ? ConsoleColor.White : ConsoleColor.Gray;
					System.Console.WriteLine($"{index++}. {item.Text}");
				}

				System.Console.WriteLine();

				string input = System.Console.ReadLine();
				if (int.TryParse(input, out index) &&
					index >= 1 &&
					index <= itemsList.Count &&
					itemsList[index - 1].IsAvailable)
				{
					result = true;
					selected = itemsList[index - 1];
				}
			});

			selectedItem = selected;
			return result;
		}

		public bool TrySelectItems(Game game, string question, IEnumerable<ListItem> items, out IList<ListItem> selectedItems)
		{
			var selected = new List<ListItem>();

			startDialog(() =>
			{
				System.Console.WriteLine(question);
				System.Console.WriteLine();

				var itemsList = new List<ListItem>(items);
				int index = 1;
				foreach (var item in itemsList)
				{
					System.Console.ForegroundColor = item.IsAvailable ? ConsoleColor.White : ConsoleColor.Gray;
					System.Console.WriteLine($"{index++}. {item.Text}");
				}

				System.Console.WriteLine();

				string inputs = System.Console.ReadLine();
				foreach (string input in (inputs ?? string.Empty).Split(" "))
				{
					if (int.TryParse(input, out index) &&
						index >= 1 &&
						index <= itemsList.Count &&
						itemsList[index - 1].IsAvailable)
					{
						selected.Add(itemsList[index - 1]);
					}
				}
			});

			selectedItems = selected;
			return selectedItems.Count > 0;
		}

		public void ShowCharacter(Game game, IHumanoid humanoid)
		{
			startDialog(() =>
			{
#warning Localize
				System.Console.ForegroundColor = ConsoleColor.Cyan;
				System.Console.WriteLine($"=== {humanoid.Name} ===");
				System.Console.ForegroundColor = ConsoleColor.White;
				string sex = humanoid.SexIsMale ? "Male" : "Female";
				System.Console.WriteLine($"{sex}, {humanoid.Age} years old");
				System.Console.WriteLine();

				System.Console.ForegroundColor = ConsoleColor.DarkYellow;
				System.Console.WriteLine($"=== STATE ===");
				System.Console.ForegroundColor = ConsoleColor.White;
				System.Console.WriteLine($"{humanoid.State}");
				System.Console.WriteLine();

				System.Console.ForegroundColor = ConsoleColor.DarkYellow;
				System.Console.WriteLine($"=== BODY ===");
				System.Console.ForegroundColor = ConsoleColor.White;
				System.Console.WriteLine($"... under construction ...");
				System.Console.WriteLine();

				System.Console.ForegroundColor = ConsoleColor.DarkYellow;
				System.Console.WriteLine($"=== PROPERTIES ===");
				System.Console.ForegroundColor = ConsoleColor.White;
				System.Console.WriteLine($"    Strength : {humanoid.Properties.Strength}");
				System.Console.WriteLine($"   Endurance : {humanoid.Properties.Endurance}");
				System.Console.WriteLine($"    Reaction : {humanoid.Properties.Reaction}");
				System.Console.WriteLine($"  Perception : {humanoid.Properties.Perception}");
				System.Console.WriteLine($"Intelligence : {humanoid.Properties.Intelligence}");
				System.Console.WriteLine($"   Willpower : {humanoid.Properties.Willpower}");
				System.Console.WriteLine();

				System.Console.ForegroundColor = ConsoleColor.DarkYellow;
				System.Console.WriteLine($"=== SKILLS ===");
				System.Console.ForegroundColor = ConsoleColor.White;
				System.Console.WriteLine($"... under construction ...");

				System.Console.ReadKey(true);
			});
		}

		public ActionResult BeginChat(Game game, IHumanoid humanoid)
		{
			throw new NotImplementedException();
		}

		public ActionResult BeginTrade(Game game, IHumanoid humanoid)
		{
			throw new NotImplementedException();
		}

		public ActionResult BeginPickpocket(Game game, IHumanoid humanoid)
		{
			throw new NotImplementedException();
		}

		#endregion
	}
}