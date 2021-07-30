using System.Windows.Forms;

namespace Roguelike.GUI
{
  /// <summary>
  /// Диалог для вывода большого текста.
  /// </summary>
  public partial class BigTextForm : Form
  {
    private BigTextForm()
    {
      InitializeComponent();
    }

    /// <summary>
    /// Вывод диалога.
    /// </summary>
    /// <param name="text">текст</param>
    /// <param name="caption">заголовок</param>
    public static void ShowDialog(string text, string caption)
    {
      using (var dialog = new BigTextForm())
      {
        dialog.Text = caption;
        dialog.textBoxText.Text = text;
        dialog.ShowDialog();
      }
    }
  }
}
