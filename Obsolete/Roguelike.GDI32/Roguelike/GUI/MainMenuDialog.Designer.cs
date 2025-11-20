namespace Roguelike.GUI
{
	partial class MainMenuDialog
	{
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.IContainer components = null;

		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
		/// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
		protected override void Dispose(bool disposing)
		{
			if (disposing && (components != null))
			{
				components.Dispose();
			}
			base.Dispose(disposing);
		}

		#region Windows Form Designer generated code

		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			this.buttonNewGame = new System.Windows.Forms.Button();
			this.buttonLoadGame = new System.Windows.Forms.Button();
			this.buttonHelp = new System.Windows.Forms.Button();
			this.buttonExit = new System.Windows.Forms.Button();
			this.openFileDialog = new System.Windows.Forms.OpenFileDialog();
			this.SuspendLayout();
			// 
			// buttonNewGame
			// 
			this.buttonNewGame.Location = new System.Drawing.Point(41, 12);
			this.buttonNewGame.Name = "buttonNewGame";
			this.buttonNewGame.Size = new System.Drawing.Size(145, 23);
			this.buttonNewGame.TabIndex = 0;
			this.buttonNewGame.Text = "Новая игра";
			this.buttonNewGame.UseVisualStyleBackColor = true;
			this.buttonNewGame.Click += new System.EventHandler(this.buttonNewGame_Click);
			// 
			// buttonLoadGame
			// 
			this.buttonLoadGame.Location = new System.Drawing.Point(41, 41);
			this.buttonLoadGame.Name = "buttonLoadGame";
			this.buttonLoadGame.Size = new System.Drawing.Size(145, 23);
			this.buttonLoadGame.TabIndex = 1;
			this.buttonLoadGame.Text = "Загрузить сохранение";
			this.buttonLoadGame.UseVisualStyleBackColor = true;
			this.buttonLoadGame.Click += new System.EventHandler(this.buttonLoadGame_Click);
			// 
			// buttonHelp
			// 
			this.buttonHelp.Location = new System.Drawing.Point(41, 70);
			this.buttonHelp.Name = "buttonHelp";
			this.buttonHelp.Size = new System.Drawing.Size(145, 23);
			this.buttonHelp.TabIndex = 2;
			this.buttonHelp.Text = "Справка";
			this.buttonHelp.UseVisualStyleBackColor = true;
			this.buttonHelp.Click += new System.EventHandler(this.buttonHelp_Click);
			// 
			// buttonExit
			// 
			this.buttonExit.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.buttonExit.Location = new System.Drawing.Point(41, 99);
			this.buttonExit.Name = "buttonExit";
			this.buttonExit.Size = new System.Drawing.Size(145, 23);
			this.buttonExit.TabIndex = 3;
			this.buttonExit.Text = "Выход";
			this.buttonExit.UseVisualStyleBackColor = true;
			// 
			// openFileDialog
			// 
			this.openFileDialog.DefaultExt = "*.xml";
			this.openFileDialog.Filter = "Файлы сохранений|*.xml";
			// 
			// MainMenuDialog
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.CancelButton = this.buttonExit;
			this.ClientSize = new System.Drawing.Size(224, 137);
			this.Controls.Add(this.buttonExit);
			this.Controls.Add(this.buttonHelp);
			this.Controls.Add(this.buttonLoadGame);
			this.Controls.Add(this.buttonNewGame);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "MainMenuDialog";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "рогалик - стартовое меню";
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.Button buttonNewGame;
		private System.Windows.Forms.Button buttonLoadGame;
		private System.Windows.Forms.Button buttonHelp;
		private System.Windows.Forms.Button buttonExit;
		private System.Windows.Forms.OpenFileDialog openFileDialog;
	}
}