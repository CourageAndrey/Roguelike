using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

using Roguelike.Core;
using Roguelike.Core.Interfaces;

namespace Roguelike.WpfClient.Controls
{
	public partial class ConsoleControl
	{
		public ConsoleControl()
		{
			InitializeComponent();

			richTextBox = (RichTextBox) ((System.Windows.Forms.Integration.WindowsFormsHost) Content).Child;
			richTextBox.Font = ObjectViewModel.DefaultFont;
		}

		public ICamera Camera
		{
			get { return camera; }
			set
			{
				if (camera != null)
				{
					camera.Changed -= cameraChanged;
				}
				camera = value;
				loadCells();
				if (camera != null)
				{
					camera.Changed += cameraChanged;
				}
			}
		}

		private int screenWidth, screenHeight;
		private CellViewModel[,] cellViewModels = new CellViewModel[0, 0];
		private readonly RichTextBox richTextBox;
		private ICamera camera;
		private List<Cell> visibleCellsCache = new List<Cell>();

		private void sizeChanged(object sender, EventArgs e)
		{
			// initialize screenWidth and screenHeight
			richTextBox.WordWrap = true;
			const int length = 1000;
			richTextBox.Text = new string('W', length);
			screenWidth = richTextBox.GetFirstCharIndexFromLine(1);
			screenHeight = richTextBox.Height / richTextBox.PreferredSize.Height;
			richTextBox.WordWrap = false;

			// get the price of new line
			var text = new StringBuilder();
			const string testChar = "a";
			text.AppendLine(testChar);
			text.Append(testChar);
			richTextBox.Text = text.ToString();
			int newLineLength = richTextBox.Text.Length - 2*testChar.Length;

			// (re)create cellViewModels
			foreach (var cellViewModel in cellViewModels)
			{
				cellViewModel.Dispose();
			}
			cellViewModels = new CellViewModel[screenWidth, screenHeight];
			for (int r = 0; r < screenHeight; r++)
			{
				for (int c = 0; c < screenWidth; c++)
				{
					cellViewModels[c, r] = new CellViewModel(richTextBox, r * (screenWidth + newLineLength) + c);
				}
			}

			// load initial void string
			var line = new string(' ', screenWidth);
			text = new StringBuilder();
			for (int r = 0; r < screenHeight; r++)
			{
				text.AppendLine(line);
			}
			richTextBox.Text = text.ToString();

			// update screen
			loadCells();
		}

		private void cameraChanged(ICamera senderCamera)
		{
			var newVisibleCells = senderCamera.SelectVisibleCells();
			var cellsBecomeVisible = new HashSet<Cell>(newVisibleCells.Except(visibleCellsCache));
			bool needToShiftScreen = false;
			for (int x = 0; x < screenWidth; x++)
			{
				if (cellsBecomeVisible.Contains(cellViewModels[x, 0].Cell) || cellsBecomeVisible.Contains(cellViewModels[x, screenHeight - 1].Cell))
				{
					needToShiftScreen = true;
					break;
				}
			}
			for (int y = 0; y < screenHeight; y++)
			{
				if (cellsBecomeVisible.Contains(cellViewModels[0, y].Cell) || cellsBecomeVisible.Contains(cellViewModels[screenWidth - 1, y].Cell))
				{
					needToShiftScreen = true;
					break;
				}
			}
			if (needToShiftScreen)
			{
				MassUpdate();
			}
			var cellsBecomeInvisible = new HashSet<Cell>(visibleCellsCache.Except(newVisibleCells));
			for (int x = 0; x < screenWidth; x++)
			{
				for (int y = 0; y < screenHeight; y++)
				{
					var cellViewModel = cellViewModels[x, y];
					if (cellsBecomeVisible.Contains(cellViewModel.Cell))
					{
						cellViewModel.IsVisible = true;
						if (cellViewModel.Cell != null && cellViewModel.CurrentObjectView == ObjectViewModel.Empty)
						{
							cellViewModel.CurrentObjectView = null;
						}
						cellViewModel.Update(true);
					}
					if (cellsBecomeInvisible.Contains(cellViewModel.Cell))
					{
						cellViewModel.IsVisible = false;
						cellViewModel.Update(true);
					}
				}
			}
			visibleCellsCache = newVisibleCells;
		}

		private void loadCells()
		{
			if (camera != null)
			{
				MassUpdate();
			}
			else
			{
				foreach (var cell in cellViewModels)
				{
					cell.Cell = null;
				}
				richTextBox.Text = string.Empty;
			}
		}

		private void MassUpdate()
		{
			visibleCellsCache.Clear();
			var cells = camera.SelectRegionCells(screenWidth, screenHeight);
			var cellObjectModels = new ObjectViewModel[screenWidth, screenHeight];
			var text = new StringBuilder();
			for (int r = 0; r < screenHeight; r++)
			{
				for (int c = 0; c < screenWidth; c++)
				{
					var currentCell = cells[r][c];
					cellViewModels[c, r].Cell = currentCell;
					cellObjectModels[c, r] = currentCell != null && camera.MapMemory.Contains(currentCell) ? ConsoleUiHelper.GetModel(currentCell) : ObjectViewModel.Empty;
					text.Append(cellObjectModels[c, r].Text);
				}
				text.AppendLine();
			}
			richTextBox.Text = text.ToString();

			var currentForeground = ObjectViewModel.DefaultForeground;
			var currentBackground = ObjectViewModel.DefaultBackground;
			var currentFont = ObjectViewModel.DefaultFont;
			int indexForeground = 0, indexBackground = 0, indexFont = 0;
			var cameraVisibleCells = new HashSet<Cell>(camera.SelectVisibleCells());
			for (int r = 0; r < screenHeight; r++)
			{
				for (int c = 0; c < screenWidth; c++)
				{
					var cellViewModel = cellViewModels[c, r];
					var objectViewModel = cellViewModel.CurrentObjectView = cellObjectModels[c, r];
					int index = cellViewModel.CharIndex;
					Color newForeground, newBackground;
					Font newFont;
					if (cellViewModel.Cell != null)
					{
						if (cameraVisibleCells.Contains(cellViewModel.Cell))
						{
							visibleCellsCache.Add(cellViewModel.Cell);
							cellViewModel.IsVisible = true;
							newForeground = objectViewModel.Foreground;
							newBackground = objectViewModel.Background;
							newFont = objectViewModel.Font;
						}
						else
						{
							cellViewModel.IsVisible = false;
							newForeground = CellViewModel.ToGrayScale(objectViewModel.Foreground);
							newBackground = CellViewModel.ToGrayScale(objectViewModel.Background);
							newFont = objectViewModel.Font;
						}
					}
					else
					{
						newForeground = currentForeground;
						newBackground = currentBackground;
						newFont = currentFont;
					}
					if (currentForeground != newForeground)
					{
						applyForeground(richTextBox, indexForeground, index - indexForeground, currentForeground);
						currentForeground = newForeground;
						indexForeground = index;
					}
					if (currentBackground != newBackground)
					{
						applyBackground(richTextBox, indexBackground, index - indexBackground, currentBackground);
						currentBackground = newBackground;
						indexBackground = index;
					}
					if (!currentFont.Equals(newFont))
					{
						applyFont(richTextBox, indexFont, index - indexFont, currentFont);
						currentFont = newFont;
						indexFont = index;
					}
				}
			}
			applyForeground(richTextBox, indexForeground, richTextBox.Text.Length - indexForeground, currentForeground);
			applyBackground(richTextBox, indexBackground, richTextBox.Text.Length - indexBackground, currentBackground);
			applyFont(richTextBox, indexFont, richTextBox.Text.Length - indexFont, currentFont);
			richTextBox.SelectionLength = 0;
		}

		#region RichTextBox styles

		private static void applyForeground(RichTextBox richTextBox, int fromIndex, int count, System.Drawing.Color color)
		{
			richTextBox.SelectionStart = fromIndex;
			richTextBox.SelectionLength = count;
			richTextBox.SelectionColor = color;
		}

		private static void applyBackground(RichTextBox richTextBox, int fromIndex, int count, System.Drawing.Color color)
		{
			richTextBox.SelectionStart = fromIndex;
			richTextBox.SelectionLength = count;
			richTextBox.SelectionBackColor = color;
		}

		private static void applyFont(RichTextBox richTextBox, int fromIndex, int count, System.Drawing.Font font)
		{
			richTextBox.SelectionStart = fromIndex;
			richTextBox.SelectionLength = count;
			richTextBox.SelectionFont = font;
		}

		#endregion

		private void fixTextFieldFocus(object sender, EventArgs e)
		{
			((System.Windows.Window) Parent).Focus();
		}
	}
}
