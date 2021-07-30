using System;
using System.Drawing;
using System.Linq;
using System.Threading;
using System.Windows.Forms;

using Roguelike.Worlds;

namespace Roguelike.GUI
{
  /// <summary>
  /// Элемент управления: консольный экран.
  /// </summary>
  public partial class ConsoleScreen : UserControl
  {
    /// <summary>
    /// ctor.
    /// </summary>
    public ConsoleScreen()
    {
      InitializeComponent();

			BackColor = Balance.DefaultBackground;
    }

    /// <summary>
    /// Determines whether the specified key is a regular input key or a special key that requires preprocessing.
    /// </summary>
    /// <returns>
    /// true if the specified key is a regular input key; otherwise, false.
    /// </returns>
    /// <param name="keyData">One of the <see cref="T:System.Windows.Forms.Keys"/> values. </param>
    protected override bool IsInputKey(Keys keyData)
    {
      return true;
    }

    #region Прорисовка

    // объект для синхронизации прорисовки
    private readonly object paintInProcess = new object();
    
    /// <summary>
    /// Перерисовка окна.
    /// </summary>
    public void Redraw()
    {
      if (!Monitor.TryEnter(paintInProcess))
        return;

      // инициализация
      var buffer = new Bitmap(Width, Height);
      var canvas = Graphics.FromImage(buffer);

      if (World.Instance != null)
      {
        var heroPosition = World.Instance.Hero.Position;
        var consoleSize = new Size(ClientRectangle.Width / Balance.CellSize.Width + 2,
                                   ClientRectangle.Height / Balance.CellSize.Height + 2);
        var startPoint = new Point((ClientRectangle.Width / 2) - heroPosition.X * Balance.CellSize.Width,
                                   (ClientRectangle.Height / 2) + heroPosition.Y * Balance.CellSize.Height);
        var screenBounds = new Rectangle(
          World.Instance.Hero.Position.X - (consoleSize.Width / 2),
          World.Instance.Hero.Position.Y - (consoleSize.Height / 2),
          consoleSize.Width,
          consoleSize.Height);
        for (int x = screenBounds.Left; x < screenBounds.Right; x++)
          for (int y = screenBounds.Top; y < screenBounds.Bottom; y++)
          {
            var position = new Point(x, y);
            var cellBounds = new Rectangle(
              startPoint.X + position.X * Balance.CellSize.Width,
              startPoint.Y - position.Y * Balance.CellSize.Height,
              Balance.CellSize.Width,
              Balance.CellSize.Height);
            canvas.FillRectangle(World.Instance.GetCell(position).Background.Brush, cellBounds);
            {
							foreach (var o in World.Instance.GetCell(position).Objects.OrderBy(o => o.HeightOrder))
                o.Draw(canvas, cellBounds);
            }
          }
      }

      // вывод на экран
      pictureBox.Image = buffer;

      Monitor.Exit(paintInProcess);
    }

    // установка резмеров ячейки экрана при загрузке формы
    private void ConsoleScreen_Load(object sender, EventArgs e)
    {
	    using (var tempScreen = Graphics.FromHwnd(Handle))
	    {
		    Balance.ResizeCell(tempScreen.MeasureString("W", Balance.DefaultFont).ToSize());
	    }
    }

    #endregion

    #region Подсказки мыши

    /// <summary>
    /// Событие: изменение подсказки.
    /// </summary>
    public event EventHandler HintChanged;

    /// <summary>
    /// Подсказка.
    /// </summary>
    public string Hint
    { get; private set; }

    private void raiseHint(string hint)
    {
      Hint = hint;

      var handler = HintChanged;
      if (handler != null)
        handler(this, EventArgs.Empty);
#warning Volatile.Read - и проверить весь проект на соблюдение этого правила!
    }

    // фокус мыши потерян
    private void ScreenControl_MouseLeave(object sender, EventArgs e)
    {
      raiseHint(string.Empty);
    }

    // движение мыши
    private void ScreenControl_MouseMove(object sender, MouseEventArgs e)
    {
      if (World.Instance == null)
        return;
      string hint = string.Empty;
      
      foreach (var obj in World.Instance.GetCell(getWorldCoordinates(ClientRectangle, e.Location)).Objects)
        if (!string.IsNullOrEmpty(obj.Description))
          hint += string.Format("{0}, ", obj.Description);
      if (!string.IsNullOrEmpty(hint))
        hint = hint.Substring(0, hint.Length - 2);

      raiseHint(hint);
    }

    private static Point getWorldCoordinates(Rectangle viewport, Point coords)
    {
      var camera = World.Instance.Hero.Position;
      return new Point(
        (+coords.X - (Balance.CellSize.Width / 2) - (viewport.Width / 2)) / Balance.CellSize.Width + camera.X,
        (-coords.Y + (Balance.CellSize.Height / 2) + (viewport.Height / 2)) / Balance.CellSize.Height + camera.Y);
    }

    // фокус мыши получен
    private void ScreenControl_MouseEnter(object sender, EventArgs e)
    {
      raiseHint(string.Empty);
    }

    #endregion
  }
}
