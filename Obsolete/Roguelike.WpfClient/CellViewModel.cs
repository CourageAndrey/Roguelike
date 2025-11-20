using System;
using System.Windows.Forms;

using Roguelike.Core;
using Roguelike.Core.Interfaces;

namespace Roguelike.WpfClient
{
	internal class CellViewModel : IDisposable
	{
		#region Properties

		public Cell Cell
		{
			get { return cell; }
			set
			{
				if (cell != null)
				{
					cell.ViewChanged -= cellViewChanged;
				}
				cell = value;
				if (cell != null)
				{
					cell.ViewChanged += cellViewChanged;
				}
			}
		}

		public RichTextBox RichTextBox
		{ get; }

		public int CharIndex
		{ get; }

		public bool IsVisible
		{ get; internal set; }

		public ObjectViewModel CurrentObjectView
		{ get; internal set; }

		private Cell cell;

		#endregion

		public CellViewModel(RichTextBox richTextBox, int charIndex)
		{
			if (richTextBox == null) throw new ArgumentNullException(nameof(richTextBox));
			if (charIndex < 0) throw new ArgumentException(nameof(charIndex));

			RichTextBox = richTextBox;
			CharIndex = charIndex;
		}

		private void cellViewChanged(Cell sender)
		{
			CurrentObjectView = null;
			var hero = sender?.Region.World.Hero;
			if (hero != null)
			{
				hero.SelectVisibleCells();
				hero.RefreshCamera();
				if (hero.MapMemory.Contains(cell))
				{
					Update(false);
				}
			}
		}

		public void Update(bool forced)
		{
			if (CurrentObjectView == null)
			{
				CurrentObjectView = cell != null ? ConsoleUiHelper.GetModel(cell) : ObjectViewModel.Empty;
			}
			if (IsVisible || forced)
			{
				RichTextBox.SelectionStart = CharIndex;
				RichTextBox.SelectionLength = 1;
				RichTextBox.SelectionColor = IsVisible ? CurrentObjectView.Foreground : ToGrayScale(CurrentObjectView.Foreground);
				RichTextBox.SelectionBackColor = IsVisible ? CurrentObjectView.Background : ToGrayScale(CurrentObjectView.Background);
				RichTextBox.SelectionFont = CurrentObjectView.Font;
				RichTextBox.SelectedText = CurrentObjectView.Text;
				RichTextBox.SelectionLength = 0;
			}
		}

		public static System.Drawing.Color ToGrayScale(System.Drawing.Color color)
		{
			int grayScale = (int)((color.R * 0.3) + (color.G * 0.59) + (color.B * 0.11));
			return System.Drawing.Color.FromArgb(color.A, grayScale, grayScale, grayScale);
		}

		public void Dispose()
		{
			Cell = null;
		}
	}
}
