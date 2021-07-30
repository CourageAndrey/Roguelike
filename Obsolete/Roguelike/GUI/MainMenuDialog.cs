using System;
using System.IO;
using System.Windows.Forms;

namespace Roguelike.GUI
{
	/// <summary>
	/// Главная форма.
	/// </summary>
	public partial class MainMenuDialog : Form
	{
		/// <summary>
		/// ctor.
		/// </summary>
		public MainMenuDialog()
		{
			InitializeComponent();
		}

		private void buttonNewGame_Click(object sender, EventArgs e)
		{
			var dialog = new CreateHeroDialog();
			if (dialog.ShowDialog() == DialogResult.OK)
			{
				Game.CreateNew(dialog.Settings);
				DialogResult = DialogResult.OK;
			}
		}

		private void buttonLoadGame_Click(object sender, EventArgs e)
		{
			if (openFileDialog.ShowDialog(this) == DialogResult.OK)
			{
				Game.Load(openFileDialog.FileName);
				DialogResult = DialogResult.OK;
			}
		}

		private void buttonHelp_Click(object sender, EventArgs e)
		{
			BigTextForm.ShowDialog(File.ReadAllText(RoguelikeProgram.GetResourceFile("help.txt")), "Справка");
		}
	}
}
