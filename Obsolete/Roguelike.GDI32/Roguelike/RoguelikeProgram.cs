using System;
using System.Drawing;
using System.IO;
using System.Windows.Forms;

using Roguelike.GUI;
using Roguelike.Worlds;

namespace Roguelike
{
  /// <summary>
  /// Класс программы.
  /// </summary>
  internal class RoguelikeProgram
  {
    [STAThread]
    static void Main()
    {
			// для корректного отображения предупреждений
			var screenSize = Screen.PrimaryScreen.Bounds.Size;
			Balance.DialogSize = new Size(screenSize.Width / 2, screenSize.Height / 2);

			// вывод главного меню
			if (new MainMenuDialog().ShowDialog() == DialogResult.OK)
			{
				// запуск главного окна игры
				Application.Run(new MainForm());
			}
    }

    /// <summary>
    /// Получение полного пути к файлу ресурсов по короткому имени.
    /// </summary>
    /// <param name="fileName">имя файла</param>
    /// <returns>полный путь</returns>
    public static string GetResourceFile(string fileName)
    {
      return Path.Combine(Application.StartupPath, "Data\\" + fileName);
    }
  }
}
