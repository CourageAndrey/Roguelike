using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading;

using Roguelike.Core;
using Roguelike.Core.Aspects;
using Roguelike.Core.Interfaces;
using Roguelike.Core.Items;
using Roguelike.Core.Objects;

namespace Roguelike.Console
{
	public partial class ConsoleUi : IUserInterface
	{
		public ConsoleUi()
		{
			System.Console.OutputEncoding = Encoding.UTF8;

			_screenWidth = System.Console.WindowWidth;
			_screenHeight = System.Console.WindowHeight;
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

			LockConsoleSize(_screenWidth, _screenHeight);
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
				// Apply word wrapping to the text
				var rawLines = text.ToString().Split(new[] { "\r\n", "\r", "\n" }, StringSplitOptions.None);
				var wrappedLines = new List<string>();
				int maxWidth = _screenWidth - 2; // Leave margin

				foreach (var line in rawLines)
				{
					if (string.IsNullOrEmpty(line))
					{
						wrappedLines.Add(line);
					}
					else
					{
						wrappedLines.AddRange(WrapLine(line, maxWidth));
					}
				}

				int maxDisplayLines = _screenHeight - 5; // Reserve space for title and controls

				if (wrappedLines.Count <= maxDisplayLines)
				{
					// Short message - display without scrolling
					if (!string.IsNullOrEmpty(title))
					{
						System.Console.WriteLine($"=== {title} ===");
						System.Console.WriteLine();
					}

					foreach (var line in wrappedLines)
					{
						System.Console.WriteLine(line);
					}
					System.Console.ReadKey(true);
				}
				else
				{
					// Long message - use scrolling
					DisplayScrollableText(title, wrappedLines.ToArray(), maxDisplayLines);
				}
			});
		}

		private IEnumerable<string> WrapLine(string line, int maxWidth)
		{
			if (line.Length <= maxWidth)
			{
				yield return line;
				yield break;
			}

			int currentPos = 0;
			while (currentPos < line.Length)
			{
				int remainingLength = line.Length - currentPos;
				int segmentLength = Math.Min(maxWidth, remainingLength);

				// If this is not the last segment, try to break at a word boundary
				if (segmentLength == maxWidth && currentPos + segmentLength < line.Length)
				{
					// Look for the last space in this segment
					int lastSpace = line.LastIndexOf(' ', currentPos + segmentLength - 1, segmentLength);

					if (lastSpace > currentPos)
					{
						// Break at the last space
						segmentLength = lastSpace - currentPos;
						yield return line.Substring(currentPos, segmentLength);
						currentPos = lastSpace + 1; // Skip the space
					}
					else
					{
						// No space found, must break in the middle of a word
						yield return line.Substring(currentPos, segmentLength);
						currentPos += segmentLength;
					}
				}
				else
				{
					// Last segment or fits entirely
					yield return line.Substring(currentPos, segmentLength);
					currentPos += segmentLength;
				}
			}
		}

		public bool TrySelectItem(string question, IEnumerable<ListItem> items, out ListItem selectedItem)
		{
			var itemsList = items.ToList();
			bool result = DisplayScrollableList(
				question,
				itemsList,
				item => item.IsAvailable,
				item => item.Text,
				item => item.IsAvailable ? ConsoleColor.White : ConsoleColor.Gray,
				out ListItem selected,
				out int _);

			selectedItem = selected;
			return result;
		}

		private bool DisplayScrollableMultiSelectList<T>(
			string question,
			List<T> items,
			Func<T, bool> isAvailable,
			Func<T, string> getText,
			Func<T, ConsoleColor> getColor,
			out IList<T> selectedItems)
		{
			var selected = new HashSet<int>();
			bool result = false;

			startDialog(() =>
			{
				int maxDisplayLines = _screenHeight - 7; // Reserve space for header, controls, and input
				int scrollPosition = 0;
				int currentIndex = 0;
				bool done = false;

				while (!done)
				{
					System.Console.Clear();
					System.Console.WriteLine(question);
					System.Console.WriteLine();

					// Display items in the current scroll window
					int displayCount = Math.Min(maxDisplayLines, items.Count - scrollPosition);
					for (int i = 0; i < displayCount; i++)
					{
						int itemIndex = scrollPosition + i;
						var item = items[itemIndex];
						bool isSelected = selected.Contains(itemIndex);
						bool isCurrent = itemIndex == currentIndex;
						bool available = isAvailable(item);

						// Set background for current item
						if (isCurrent)
						{
							System.Console.BackgroundColor = ConsoleColor.DarkGray;
						}

						// Show checkbox and item
						System.Console.ForegroundColor = getColor(item);
						string checkbox = isSelected ? "[X]" : "[ ]";
						System.Console.WriteLine($"{itemIndex + 1}. {checkbox} {getText(item)}");

						// Reset background
						if (isCurrent)
						{
							System.Console.BackgroundColor = ConsoleColor.Black;
						}
					}

					System.Console.WriteLine();

					// Show scroll indicators and controls
					System.Console.ForegroundColor = ConsoleColor.DarkGray;
					if (scrollPosition > 0)
					{
						System.Console.Write("↑ ");
					}
					if (scrollPosition + maxDisplayLines < items.Count)
					{
						System.Console.Write("↓ ");
					}
					System.Console.Write("ARROWS navigate | SPACE toggle | ENTER confirm | ESC cancel | or type numbers");
					System.Console.ForegroundColor = ConsoleColor.White;
					System.Console.WriteLine();

					var key = System.Console.ReadKey(true);
					switch (key.Key)
					{
						case ConsoleKey.UpArrow:
							if (currentIndex > 0)
							{
								currentIndex--;
								// Auto-scroll if needed
								if (currentIndex < scrollPosition)
								{
									scrollPosition = currentIndex;
								}
							}
							break;

						case ConsoleKey.DownArrow:
							if (currentIndex < items.Count - 1)
							{
								currentIndex++;
								// Auto-scroll if needed
								if (currentIndex >= scrollPosition + maxDisplayLines)
								{
									scrollPosition = currentIndex - maxDisplayLines + 1;
								}
							}
							break;

						case ConsoleKey.PageUp:
							currentIndex = Math.Max(0, currentIndex - maxDisplayLines);
							scrollPosition = Math.Max(0, scrollPosition - maxDisplayLines);
							break;

						case ConsoleKey.PageDown:
							currentIndex = Math.Min(items.Count - 1, currentIndex + maxDisplayLines);
							scrollPosition = Math.Min(items.Count - maxDisplayLines, scrollPosition + maxDisplayLines);
							if (scrollPosition < 0) scrollPosition = 0;
							break;

						case ConsoleKey.Home:
							currentIndex = 0;
							scrollPosition = 0;
							break;

						case ConsoleKey.End:
							currentIndex = items.Count - 1;
							scrollPosition = Math.Max(0, items.Count - maxDisplayLines);
							break;

						case ConsoleKey.Spacebar:
							// Toggle selection of current item
							if (isAvailable(items[currentIndex]))
							{
								if (selected.Contains(currentIndex))
								{
									selected.Remove(currentIndex);
								}
								else
								{
									selected.Add(currentIndex);
								}
							}
							break;

						case ConsoleKey.Enter:
							done = true;
							result = selected.Count > 0;
							break;

						case ConsoleKey.Escape:
							done = true;
							result = false;
							selected.Clear();
							break;

						default:
							// Try number input
							if (char.IsDigit(key.KeyChar))
							{
								System.Console.Write(key.KeyChar);
								string numberInput = key.KeyChar + System.Console.ReadLine();

								foreach (string input in numberInput.Split(new[] { ' ', ',' }, StringSplitOptions.RemoveEmptyEntries))
								{
									if (int.TryParse(input, out int index) &&
										index >= 1 &&
										index <= items.Count &&
										isAvailable(items[index - 1]))
									{
										int idx = index - 1;
										if (selected.Contains(idx))
										{
											selected.Remove(idx);
										}
										else
										{
											selected.Add(idx);
										}
									}
								}
							}
							break;
					}
				}
			});

			selectedItems = selected.OrderBy(i => i).Select(i => items[i]).ToList();
			return result;
		}

		public bool TrySelectItems(string question, IEnumerable<ListItem> items, out IList<ListItem> selectedItems)
		{
			var itemsList = items.ToList();
			return DisplayScrollableMultiSelectList(
				question,
				itemsList,
				item => item.IsAvailable,
				item => item.Text,
				item => item.IsAvailable ? ConsoleColor.White : ConsoleColor.Gray,
				out selectedItems);
		}

		public void ShowCharacter(Game game, IHumanoid humanoid)
		{
			var language = game.Language;
			var languageUi = language.Ui.CharacterScreen;
			int propetyLength = language.Character.Properties.GetAll().Max(p => p.Length);

			var lines = new List<string>();

			// Header
			lines.Add($"=== {humanoid.Name} ===");
			string sex = humanoid.SexIsMale ? language.Character.SexIsMale : language.Character.SexIsFemale;
			lines.Add($"{sex} {humanoid.Race.GetName(language.Character.Races)}, {humanoid.GetAge(humanoid.GetWorld().Time)} {language.Character.AgeYears}");
			lines.Add(string.Format(
				humanoid.Appearance.Haircut != Haircut.Bald ? languageUi.AppearanceFormat : languageUi.AppearanceFormatBald,
				humanoid.SkinColor.Name.ToLowerInvariant(),
				humanoid.Appearance.Haircut.GetName(language.Character.Haircuts),
				humanoid.Appearance.HairColor.Name.ToLowerInvariant()));
			lines.Add("");

			// State
			lines.Add($"=== {languageUi.State.ToUpperInvariant()} ===");
			lines.Add($"{humanoid.State.GetDescription(language, game.Hero)}");
			lines.Add("");

			// Body
			lines.Add($"=== {languageUi.Body.ToUpperInvariant()} ===");
			lines.Add($"... under construction ...");
			lines.Add("");

			// Stats
			lines.Add($"=== {languageUi.Stats.ToUpperInvariant()} ===");
			lines.Add($"{language.Character.Properties.Strength.PadLeft(propetyLength)} : {humanoid.Properties.Strength}");
			lines.Add($"{language.Character.Properties.Endurance.PadLeft(propetyLength)} : {humanoid.Properties.Endurance}");
			lines.Add($"{language.Character.Properties.Reaction.PadLeft(propetyLength)} : {humanoid.Properties.Reaction}");
			lines.Add($"{language.Character.Properties.Perception.PadLeft(propetyLength)} : {humanoid.Properties.Perception}");
			lines.Add($"{language.Character.Properties.Intelligence.PadLeft(propetyLength)} : {humanoid.Properties.Intelligence}");
			lines.Add($"{language.Character.Properties.Willpower.PadLeft(propetyLength)} : {humanoid.Properties.Willpower}");
			lines.Add("");

			// Skills
			lines.Add($"=== {languageUi.Skills.ToUpperInvariant()} ===");
			lines.Add($"... under construction ...");

			int maxDisplayLines = _screenHeight - 5;
			if (lines.Count <= maxDisplayLines)
			{
				startDialog(() =>
				{
					foreach (var line in lines)
					{
						System.Console.WriteLine(line);
					}
					System.Console.ReadKey(true);
				});
			}
			else
			{
				DisplayScrollableText(humanoid.Name, lines.ToArray(), maxDisplayLines);
			}
		}

		public void ShowInventory(Game game, IHumanoid humanoid)
		{
			var language = game.Language;
			var lines = new List<string>();

			foreach (var itemTypeGroup in humanoid.Inventory.Items.GroupBy(item => item.Type))
			{
				lines.Add($"=== {itemTypeGroup.Key.GetName(language.Items.ItemTypes)}: ===");

				foreach (var item in itemTypeGroup)
				{
					lines.Add(item.GetDescription(language, humanoid));
				}

				lines.Add("");
			}

			int maxDisplayLines = _screenHeight - 5;
			if (lines.Count <= maxDisplayLines)
			{
				startDialog(() =>
				{
					foreach (var line in lines)
					{
						System.Console.WriteLine(line);
					}
					System.Console.ReadKey(true);
				});
			}
			else
			{
				DisplayScrollableText($"{humanoid.Name} - Inventory", lines.ToArray(), maxDisplayLines);
			}
		}

		private void DisplayScrollableText(string title, string[] lines, int maxDisplayLines)
		{
			int scrollPosition = 0;
			int totalLines = lines.Length;

			while (true)
			{
				Clear(true);

				if (!string.IsNullOrEmpty(title))
				{
					System.Console.WriteLine($"=== {title} ===");
					System.Console.WriteLine();
				}

				int displayableLines = Math.Min(maxDisplayLines, totalLines - scrollPosition);
				for (int i = 0; i < displayableLines; i++)
				{
					System.Console.WriteLine(lines[scrollPosition + i]);
				}

				// Show scroll indicators
				System.Console.WriteLine();
				if (scrollPosition > 0)
				{
					System.Console.Write("↑ UP ");
				}
				if (scrollPosition + maxDisplayLines < totalLines)
				{
					System.Console.Write("↓ DOWN ");
				}
				System.Console.Write("ESC to close");

				var key = System.Console.ReadKey(true);
				switch (key.Key)
				{
					case ConsoleKey.UpArrow:
					case ConsoleKey.PageUp:
						scrollPosition = Math.Max(0, scrollPosition - 1);
						break;
					case ConsoleKey.DownArrow:
					case ConsoleKey.PageDown:
						scrollPosition = Math.Min(totalLines - maxDisplayLines, scrollPosition + 1);
						break;
					case ConsoleKey.Home:
						scrollPosition = 0;
						break;
					case ConsoleKey.End:
						scrollPosition = Math.Max(0, totalLines - maxDisplayLines);
						break;
					case ConsoleKey.Escape:
					case ConsoleKey.Enter:
					case ConsoleKey.Spacebar:
						return;
				}
			}
		}

		private bool DisplayScrollableList<T>(string question, List<T> items, Func<T, bool> isAvailable, Func<T, string> getText, Func<T, ConsoleColor> getColor, out T selectedItem, out int selectedIndex)
		{
			selectedItem = default(T);
			selectedIndex = -1;

			if (items.Count == 0)
			{
				return false;
			}

			int scrollPosition = 0;
			int selectedPosition = 0;
			int maxDisplayLines = _screenHeight - 6; // Reserve space for title and controls

			while (true)
			{
				Clear(true);
				System.Console.WriteLine(question);
				System.Console.WriteLine();

				// Adjust scroll position to keep selected item visible
				if (selectedPosition < scrollPosition)
				{
					scrollPosition = selectedPosition;
				}
				if (selectedPosition >= scrollPosition + maxDisplayLines)
				{
					scrollPosition = selectedPosition - maxDisplayLines + 1;
				}

				int displayableLines = Math.Min(maxDisplayLines, items.Count - scrollPosition);
				for (int i = 0; i < displayableLines; i++)
				{
					int itemIndex = scrollPosition + i;
					var item = items[itemIndex];
					bool available = isAvailable(item);
					bool isSelected = itemIndex == selectedPosition;

					if (isSelected)
					{
						System.Console.BackgroundColor = ConsoleColor.DarkGray;
					}

					System.Console.ForegroundColor = available ? getColor(item) : ConsoleColor.DarkGray;
					System.Console.WriteLine($"{itemIndex + 1}. {getText(item)}");

					if (isSelected)
					{
						System.Console.BackgroundColor = ConsoleColor.Black;
					}
				}

				System.Console.WriteLine();
				System.Console.ForegroundColor = ConsoleColor.Gray;
				if (scrollPosition > 0)
				{
					System.Console.Write("↑ ");
				}
				if (scrollPosition + maxDisplayLines < items.Count)
				{
					System.Console.Write("↓ ");
				}
				System.Console.Write("ARROWS to navigate | ENTER to select | ESC to cancel");
				System.Console.ForegroundColor = ConsoleColor.White;

				var key = System.Console.ReadKey(true);
				switch (key.Key)
				{
					case ConsoleKey.UpArrow:
						selectedPosition = Math.Max(0, selectedPosition - 1);
						break;
					case ConsoleKey.DownArrow:
						selectedPosition = Math.Min(items.Count - 1, selectedPosition + 1);
						break;
					case ConsoleKey.PageUp:
						selectedPosition = Math.Max(0, selectedPosition - maxDisplayLines);
						break;
					case ConsoleKey.PageDown:
						selectedPosition = Math.Min(items.Count - 1, selectedPosition + maxDisplayLines);
						break;
					case ConsoleKey.Home:
						selectedPosition = 0;
						break;
					case ConsoleKey.End:
						selectedPosition = items.Count - 1;
						break;
					case ConsoleKey.Enter:
						if (isAvailable(items[selectedPosition]))
						{
							selectedItem = items[selectedPosition];
							selectedIndex = selectedPosition;
							return true;
						}
						break;
					case ConsoleKey.Escape:
						return false;
					default:
						// Also support number input for backward compatibility
						if (char.IsDigit(key.KeyChar))
						{
							System.Console.Write(key.KeyChar);
							string input = key.KeyChar + System.Console.ReadLine();
							if (int.TryParse(input, out int index) &&
								index >= 1 &&
								index <= items.Count &&
								isAvailable(items[index - 1]))
							{
								selectedItem = items[index - 1];
								selectedIndex = index - 1;
								return true;
							}
						}
						break;
				}
			}
		}

		public ActionResult ShowEquipment(Game game, Mannequin mannequin)
		{
			var operations = new List<KeyValuePair<IItem, bool>>();

			startDialog(() =>
			{
				var language = game.Language;

				do
				{
					var itemSlots = EquipmentSlot.Display(language, game.Hero, game.Hero.Mannequin);
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
									mannequin.Dress(itemToDress);
									operations.Add(new KeyValuePair<IItem, bool>(itemToDress, true));
								}
							}
							else
							{ // undress
								mannequin.Undress(itemSlot.Wear);
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
			var language = game.Language;
			var hero = game.Hero;
			var interlocutor = humanoid.Interlocutor;

			startDialog(() =>
			{
				System.Console.WriteLine(interlocutor.GetName(hero));
				System.Console.WriteLine(interlocutor.SocialGroup.GetName(language));
				System.Console.WriteLine(interlocutor.GetAttitude(hero).ToString());
				System.Console.WriteLine();

				var topics = interlocutor.GetTopics(hero).ToList();
				for (int t = 0; t < topics.Count; t++)
				{
					System.Console.WriteLine($"{t+1}. {topics[t].Ask(language.Talk.Questions)}");
				}
				System.Console.WriteLine();

				var topic = topics[int.Parse(System.Console.ReadLine()) - 1];
				var answer = interlocutor.Discuss(hero, topic, language);
				System.Console.WriteLine(answer.PlainString);
				System.Console.WriteLine();

				System.Console.ReadKey(true);
			});

			return null;
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