using System.Windows.Forms;

using Roguelike.Worlds;

namespace Roguelike.GUI
{
  /// <summary>
  /// Форма создания героя.
  /// </summary>
  public partial class CreateHeroDialog : Form
  {
    /// <summary>
    /// ctor.
    /// </summary>
    public CreateHeroDialog()
    {
      InitializeComponent();

      Settings = new StartSettings();
      startSettingsBindingSource.DataSource = Settings;
      startSettingsBindingSource.ResetBindings(false);

      startBonusBindingSource.DataSource = StartBonus.All;
      startBonusBindingSource.ResetBindings(false);
    }

    /// <summary>
    /// Настройки.
    /// </summary>
    public StartSettings Settings
    { get; private set; }

    /// <summary>
    /// Выбранное прозвище.
    /// </summary>
    public string HeroNickname
    { get { return textBoxNickname.Text; } }

    /// <summary>
    /// Выбранный пол.
    /// </summary>
    public bool HeroSex
    { get { return checkBoxSex.Checked; } }

    // не закрывать форму без введённых имени и прозвища
    private void CreateHeroDialog_FormClosing(object sender, FormClosingEventArgs e)
    {
      string error;
      if ((DialogResult == DialogResult.OK) && !Settings.CheckValid(out error))
      {
        DialogResult = DialogResult.None;
        e.Cancel = true;
        MessageBox.Show(this, error, "Предупреждение", MessageBoxButtons.OK, MessageBoxIcon.Warning);
      }
    }
  }
}
