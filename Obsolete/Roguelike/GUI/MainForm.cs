using System;
using System.Drawing;
using System.IO;
using System.Windows.Forms;

using Roguelike.Worlds;

namespace Roguelike.GUI
{
  /// <summary>
  /// Главная форма.
  /// </summary>
  public partial class MainForm : Form
  {
    /// <summary>
    /// ctor.
    /// </summary>
    public MainForm()
    {
      // инициализация GUI по умолчанию
      InitializeComponent();

      // настройка дочерних элементов управления
      foreach (Control control in Controls)
        if (control != consoleScreen)
          control.TabStop = false;
      consoleScreen.Focus();
    }

    // реакция на изменение подсказки
    private void screenControl_HintChanged(object sender, EventArgs e)
    {
      toolStripStatusLabelHint.Text = consoleScreen.Hint;
    }

    // реакция на нашжатие клавиши
    private void screenControl_KeyDown(object sender, KeyEventArgs e)
    {
      // подготовка
      var world = Game.Current.World;
      var hero = Game.Current.Hero;

      // предварительная обработка нажатий функциональных клавиш
      if (processKey(e))
        return;

      // проверка на финиш
      if (world.GameOver)
        return;

      // обработка нажатия клавиши
      var logMessages = hero.ProcessKey(e.KeyCode);
      foreach (var message in logMessages)
        Game.Current.WriteLog(message);
      updateControls();

      // цикл других агентов
      do
      {
        world.DoOneStep();
      } while (world.LastActor != hero);
     updateControls();
    }

    private bool processKey(KeyEventArgs eventArgs)
    {
      switch (eventArgs.KeyCode)
      {
        case Keys.F1:
          // вывод справки
          BigTextForm.ShowDialog(File.ReadAllText(RoguelikeProgram.GetResourceFile("help.txt")), "Справка");
          return true;
				case Keys.F2:
					// вывод журнала
					BigTextForm.ShowDialog(Game.Current.Log, "Журнал событий");
					return true;
				case Keys.F3:
					// вывод инвентори
		      new InventoryDialog(Game.Current.Hero).ShowDialog();
					return true;
				case Keys.F4:
					// вывод тела
					new BodyDialog(Game.Current.Hero.Body).ShowDialog();
					return true;
				case Keys.Escape:
          // выход из игры
          Close();
          return true;
        case Keys.F12:
          // вызов окна консоли
          ConsoleForm.ShowNew(this);
          return true;
      }

      return false;
    }

    // пересчёт дистанции обработки и прорисовки при изменении размеров формы
    private void MainForm_SizeChanged(object sender, EventArgs e)
    {
      Balance.RecalculateWorkDistance(Size);
      Balance.DialogSize = new Size(ClientSize.Width / 4 * 3, ClientSize.Height / 4 * 3);
    }

    // вопрос о выходе
    private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
    {
      if (World.Instance.Hero.IsDead)
        return;

      if (MessageBox.Show(this, "Завершить игру, убив персонажа и разрушив мир?", "Закрытие", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
        e.Cancel = true;
    }

    #region Дочерние элементы управления

    private void updateControls(bool redraw = true)
    {
	    if (redraw)
	    {
		    consoleScreen.Redraw();
	    }
    }

    #endregion

    private void MainForm_Load(object sender, EventArgs e)
    {
			// привязка всех дочерних элементов управления формы
			updateControls(false);

			// выполнение первого шага
			Game.Current.World.StartLife();

      // первая прорисовка
      consoleScreen.Redraw();
    }
  }
}
