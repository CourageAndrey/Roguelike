using System.Windows.Forms;

namespace Roguelike.GUI
{
  /// <summary>
  /// Форма ввода консольных команд.
  /// </summary>
  public partial class ConsoleForm : Form
  {
    private ConsoleForm()
    {
      InitializeComponent();
    }

    /// <summary>
    /// Вывод на экран.
    /// </summary>
    /// <param name="parent">родительская форма</param>
    public static void ShowNew(IWin32Window parent)
    {
      using (var dialog = new ConsoleForm())
        dialog.ShowDialog(parent);
    }

    private void textBoxInput_KeyDown(object sender, KeyEventArgs e)
    {
      if ((e.KeyCode == Keys.Enter) && (sender == textBoxInput))
      {
        var result = Game.Current.ConsoleRun(textBoxInput.Text);
        result.AppendLine(); // пустая строка между коммандами
        textBoxOutput.Text += result.ToString();
        textBoxInput.Text = string.Empty;
      }
    }
  }
}
