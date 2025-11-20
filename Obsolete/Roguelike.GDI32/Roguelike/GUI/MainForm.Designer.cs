using System.Drawing;
using System.Windows.Forms;

namespace Roguelike.GUI
{
  partial class MainForm
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
			this.statusStrip = new System.Windows.Forms.StatusStrip();
			this.toolStripStatusLabelHint = new System.Windows.Forms.ToolStripStatusLabel();
			this.consoleScreen = new Roguelike.GUI.ConsoleScreen();
			this.statusStrip.SuspendLayout();
			this.SuspendLayout();
			// 
			// statusStrip
			// 
			this.statusStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripStatusLabelHint});
			this.statusStrip.Location = new System.Drawing.Point(0, 540);
			this.statusStrip.Name = "statusStrip";
			this.statusStrip.Size = new System.Drawing.Size(784, 22);
			this.statusStrip.TabIndex = 1;
			this.statusStrip.Text = "statusStrip1";
			// 
			// toolStripStatusLabelHint
			// 
			this.toolStripStatusLabelHint.Name = "toolStripStatusLabelHint";
			this.toolStripStatusLabelHint.Size = new System.Drawing.Size(0, 17);
			// 
			// consoleScreen
			// 
			this.consoleScreen.BackColor = System.Drawing.Color.Black;
			this.consoleScreen.Dock = System.Windows.Forms.DockStyle.Fill;
			this.consoleScreen.Location = new System.Drawing.Point(0, 0);
			this.consoleScreen.Name = "consoleScreen";
			this.consoleScreen.Size = new System.Drawing.Size(784, 562);
			this.consoleScreen.TabIndex = 0;
			this.consoleScreen.HintChanged += new System.EventHandler(this.screenControl_HintChanged);
			this.consoleScreen.KeyDown += new System.Windows.Forms.KeyEventHandler(this.screenControl_KeyDown);
			// 
			// MainForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(784, 562);
			this.Controls.Add(this.statusStrip);
			this.Controls.Add(this.consoleScreen);
			this.Name = "MainForm";
			this.Text = "Рогалик";
			this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
			this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.MainForm_FormClosing);
			this.Load += new System.EventHandler(this.MainForm_Load);
			this.SizeChanged += new System.EventHandler(this.MainForm_SizeChanged);
			this.statusStrip.ResumeLayout(false);
			this.statusStrip.PerformLayout();
			this.ResumeLayout(false);
			this.PerformLayout();

    }

    #endregion

    private StatusStrip statusStrip;
    private ToolStripStatusLabel toolStripStatusLabelHint;
		private ConsoleScreen consoleScreen;


  }
}

