﻿using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading;

using Roguelike.Core;
using Roguelike.Core.Aspects;
using Roguelike.Core.Interfaces;
using Roguelike.Core.Items;

namespace Roguelike.Console
{
	public partial class ConsoleUi : IUserInterface
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
		private readonly IDictionary<Cell, CellViewModel> _cellsViewsCache = new Dictionary<Cell, CellViewModel>();
		private Camera _camera;

		public Camera Camera
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
			_cellsViewsCache.Clear();
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
					if (currentCell != null)
					{
						_cellsViewsCache[currentCell] = _cellViewModels[c, r];
					}

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

		private void cameraCellsVisibilityChanged(Camera senderCamera, IDictionary<Cell, bool> delta)
		{
			if (isCameraNearScreenBounds(senderCamera))
			{
				RedrawAll();
			}
			else
			{
				foreach (var cellModel in _cellsViewsCache.Values)
				{
					if (cellModel.X >= _screenXOfCameraLeft &&
						cellModel.X <= _screenXOfCameraRight &&
						cellModel.Y >= _screenYOfCameraTop &&
						cellModel.Y <= _screenYOfCameraDown &&
						cellModel.Cell != null &&
						delta.ContainsKey(cellModel.Cell))
					{
						cellModel.Invalidate();
						cellModel.Update(senderCamera);
					}
				}
			}
		}

		private bool isCameraNearScreenBounds(Camera camera)
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

		private void updateCameraScreenBounds(Camera camera)
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
			Clear(true);

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

		public bool TrySelectItem(string question, IEnumerable<ListItem> items, out ListItem selectedItem)
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

		public bool TrySelectItems(string question, IEnumerable<ListItem> items, out IList<ListItem> selectedItems)
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
			var language = game.Language;
			var languageUi = language.Ui.CharacterScreen;
			int propetyLength = language.Character.Properties.GetAll().Max(p => p.Length);
			startDialog(() =>
			{
				System.Console.ForegroundColor = ConsoleColor.Cyan;
				System.Console.WriteLine($"=== {humanoid.Name} ===");
				System.Console.ForegroundColor = ConsoleColor.White;
				string sex = humanoid.SexIsMale ? language.Character.SexIsMale : language.Character.SexIsFemale;
				System.Console.WriteLine($"{sex} {humanoid.Race.GetName(language.Character.Races)}, {humanoid.GetAge(humanoid.GetWorld().Time)} {language.Character.AgeYears}");
				System.Console.WriteLine();

				System.Console.ForegroundColor = ConsoleColor.DarkYellow;
				System.Console.WriteLine($"=== {languageUi.State.ToUpperInvariant()} ===");
				System.Console.ForegroundColor = ConsoleColor.White;
				System.Console.WriteLine($"{humanoid.State.GetDescription(language, game.Hero)}");
				System.Console.WriteLine();

				System.Console.ForegroundColor = ConsoleColor.DarkYellow;
				System.Console.WriteLine($"=== {languageUi.Body.ToUpperInvariant()} ===");
				System.Console.ForegroundColor = ConsoleColor.White;
				System.Console.WriteLine($"... under construction ...");
				System.Console.WriteLine();

				System.Console.ForegroundColor = ConsoleColor.DarkYellow;
				System.Console.WriteLine($"=== {languageUi.Stats.ToUpperInvariant()} ===");
				System.Console.ForegroundColor = ConsoleColor.White;
				System.Console.WriteLine($"{language.Character.Properties.Strength.PadLeft(propetyLength)} : {humanoid.Properties.Strength}");
				System.Console.WriteLine($"{language.Character.Properties.Endurance.PadLeft(propetyLength)} : {humanoid.Properties.Endurance}");
				System.Console.WriteLine($"{language.Character.Properties.Reaction.PadLeft(propetyLength)} : {humanoid.Properties.Reaction}");
				System.Console.WriteLine($"{language.Character.Properties.Perception.PadLeft(propetyLength)} : {humanoid.Properties.Perception}");
				System.Console.WriteLine($"{language.Character.Properties.Intelligence.PadLeft(propetyLength)} : {humanoid.Properties.Intelligence}");
				System.Console.WriteLine($"{language.Character.Properties.Willpower.PadLeft(propetyLength)} : {humanoid.Properties.Willpower}");
				System.Console.WriteLine();

				System.Console.ForegroundColor = ConsoleColor.DarkYellow;
				System.Console.WriteLine($"=== {languageUi.Skills.ToUpperInvariant()} ===");
				System.Console.ForegroundColor = ConsoleColor.White;
				System.Console.WriteLine($"... under construction ...");

				System.Console.ReadKey(true);
			});
		}

		public void ShowInventory(Game game, IHumanoid humanoid)
		{
			startDialog(() =>
			{
				var language = game.Language;

				foreach (var itemTypeGroup in humanoid.Inventory.Items.GroupBy(item => item.Type))
				{
					System.Console.ForegroundColor = ConsoleColor.Yellow;
					System.Console.WriteLine($"=== {itemTypeGroup.Key.GetName(language.Items.ItemTypes)}: ===");
					System.Console.ForegroundColor = ConsoleColor.White;

					foreach (var item in itemTypeGroup)
					{
						System.Console.ForegroundColor = item.Material.Color.ToConsole();
						System.Console.WriteLine(item.GetDescription(language, humanoid));
					}

					System.Console.WriteLine();
				}

				System.Console.ReadKey(true);
			});
		}

		public ActionResult ShowEquipment(Game game, Manequin manequin)
		{
			var operations = new List<KeyValuePair<IItem, bool>>();

			startDialog(() =>
			{
				var language = game.Language;

				do
				{
					var itemSlots = EquipmentSlot.Display(language, game.Hero, game.Hero.Manequin);
					var key = System.Console.ReadKey(true);

					if (char.IsLetter(key.KeyChar))
					{
						char pressedChar = key.KeyChar.ToString().ToUpperInvariant()[0];
						EquipmentSlot itemSlot;
						if (itemSlots.TryGetValue(pressedChar, out itemSlot))
						{
							if (itemSlot.Wear is Naked || itemSlot.Wear == null)
							{ // dress
								var possibleItems = itemSlot
									.FilterSuitableItems(game.Hero.Inventory.Items)
									.Select(i => new ListItem<IItem>(i, i.GetDescription(language, game.Hero)))
								.ToList();

								ListItem selectedItemItem;
								if (TrySelectItem(language.Promts.SelectWear, possibleItems, out selectedItemItem))
								{
									var itemToDress = (IItem) selectedItemItem.ValueObject;
									manequin.Dress(itemToDress);
									operations.Add(new KeyValuePair<IItem, bool>(itemToDress, true));
								}
							}
							else
							{ // undress
								manequin.Undress(itemSlot.Wear);
								operations.Add(new KeyValuePair<IItem, bool>(itemSlot.Wear, false));
							}
						}

						Clear(false);
					}
					else if (key.Key == ConsoleKey.Escape)
					{
						break;
					}
				} while (true);
			});

			if (operations.Count > 0)
			{
				var longevity = new Time(game.World.Balance.Time);
				var messages = new List<string>();
				var languageLog = game.Language.LogActionFormats;

				foreach (var operation in operations)
				{
					longevity = longevity.AddTicks(operation.Key.GetDressTime(game.World.Balance.ActionLongevity.Dress));
					messages.Add(string.Format(
						CultureInfo.InvariantCulture,
						operation.Value ? languageLog.Dress : languageLog.Undress,
						game.Hero,
						operation.Key));
				}

				return new ActionResult(longevity, messages, Activity.Dresses);
			}
			else
			{
				return null;
			}
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

		public Cell SelectShootingTarget(Game game)
		{
			var shooter = game.Hero;
			var possibleTargets = shooter.Camera.VisibleCells
				.Where(cell => cell.Value && cell.Key.Objects.OfType<IAlive>().Any())
				.Select(cell => cell.Key)
				.ToList();
			possibleTargets.Remove(shooter.CurrentCell);

			var aim = possibleTargets.FirstOrDefault() ?? shooter.CurrentCell;
			var region = shooter.GetRegion();
			bool aimSelected = false;

			_cellsViewsCache[aim].SetOverlay(OverlayViewModel.Aim);
			while (!aimSelected)
			{
				var decision = System.Console.ReadKey(true);
				switch (decision.Key)
				{
					case ConsoleKey.LeftArrow:
					case ConsoleKey.NumPad4:
						aim = ShiftAim(aim, region.GetCell(aim.Position.X - 1, aim.Position.Y, aim.Position.Z));
						break;
					case ConsoleKey.RightArrow:
					case ConsoleKey.NumPad6:
						aim = ShiftAim(aim, region.GetCell(aim.Position.X + 1, aim.Position.Y, aim.Position.Z));
						break;
					case ConsoleKey.UpArrow:
					case ConsoleKey.NumPad8:
						aim = ShiftAim(aim, region.GetCell(aim.Position.X, aim.Position.Y + 1, aim.Position.Z));
						break;
					case ConsoleKey.DownArrow:
					case ConsoleKey.NumPad2:
						aim = ShiftAim(aim, region.GetCell(aim.Position.X, aim.Position.Y - 1, aim.Position.Z));
						break;
					case ConsoleKey.NumPad1:
						aim = ShiftAim(aim, region.GetCell(aim.Position.X - 1, aim.Position.Y - 1, aim.Position.Z));
						break;
					case ConsoleKey.NumPad3:
						aim = ShiftAim(aim, region.GetCell(aim.Position.X + 1, aim.Position.Y - 1, aim.Position.Z));
						break;
					case ConsoleKey.NumPad7:
						aim = ShiftAim(aim, region.GetCell(aim.Position.X - 1, aim.Position.Y + 1, aim.Position.Z));
						break;
					case ConsoleKey.NumPad9:
						aim = ShiftAim(aim, region.GetCell(aim.Position.X + 1, aim.Position.Y + 1, aim.Position.Z));
						break;
					case ConsoleKey.PageUp:
						int previousIndex = possibleTargets.IndexOf(aim);
						previousIndex = previousIndex >= 0
							? (possibleTargets.IndexOf(aim) - 1 + possibleTargets.Count) % possibleTargets.Count
							: 0;
						aim = ShiftAim(aim, possibleTargets[previousIndex]);
						break;
					case ConsoleKey.PageDown:
						int nextIndex = possibleTargets.IndexOf(aim);
						nextIndex = nextIndex >= 0
							? (possibleTargets.IndexOf(aim) + 1) % possibleTargets.Count
							: 0;
						aim = ShiftAim(aim, possibleTargets[nextIndex]);
						break;
					case ConsoleKey.Enter:
						_cellsViewsCache[aim].ResetOverlay(_camera);
						aimSelected = true;
						break;
					case ConsoleKey.Escape:
						_cellsViewsCache[aim].ResetOverlay(_camera);
						aim = null;
						aimSelected = true;
						break;
				}
			}

			return aim;
		}

		private Cell ShiftAim(Cell current, Cell next)
		{
			_cellsViewsCache[current].ResetOverlay(_camera);
			_cellsViewsCache[next].SetOverlay(OverlayViewModel.Aim);
			return next;
		}

		public void AnimateShoot(Direction direction, ICollection<Cell> path, IItem missile)
		{
			string missileChar = direction.GetMissile();
			if (missileChar != null)
			{
				var overlay = new OverlayViewModel(missileChar, ConsoleColor.White, ConsoleColor.Black);

				CellViewModel previous = null;
				foreach (var cell in path)
				{
					if (previous != null)
					{
						previous.ResetOverlay(_camera);
					}

					CellViewModel cellViewModel;
					if (_cellsViewsCache.TryGetValue(cell, out cellViewModel))
					{
						cellViewModel.SetOverlay(overlay);
						Thread.Sleep(50);
					}

					previous = cellViewModel;
				}

				if (previous != null)
				{
					previous.ResetOverlay(_camera);
				}
			}
		}

		#endregion

		public void Clear(bool resetColors)
		{
			System.Console.Clear();
			System.Console.CursorTop = 0;
			System.Console.CursorLeft = 0;
			if (resetColors)
			{
				System.Console.ForegroundColor = ConsoleColor.White;
				System.Console.BackgroundColor = ConsoleColor.Black;
			}
		}
	}
}