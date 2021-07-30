namespace Roguelike.GUI
{
  partial class ConsoleForm
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
      this.label1 = new System.Windows.Forms.Label();
      this.textBoxOutput = new System.Windows.Forms.TextBox();
      this.textBoxInput = new System.Windows.Forms.TextBox();
      this.SuspendLayout();
      // 
      // label1
      // 
      this.label1.AutoSize = true;
      this.label1.Location = new System.Drawing.Point(10, 14);
      this.label1.Name = "label1";
      this.label1.Size = new System.Drawing.Size(98, 13);
      this.label1.TabIndex = 1;
      this.label1.Text = "Введите команду:";
      // 
      // textBoxOutput
      // 
      this.textBoxOutput.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
      this.textBoxOutput.Location = new System.Drawing.Point(10, 35);
      this.textBoxOutput.Multiline = true;
      this.textBoxOutput.Name = "textBoxOutput";
      this.textBoxOutput.ReadOnly = true;
      this.textBoxOutput.ScrollBars = System.Windows.Forms.ScrollBars.Both;
      this.textBoxOutput.Size = new System.Drawing.Size(237, 232);
      this.textBoxOutput.TabIndex = 3;
      // 
      // textBoxInput
      // 
      this.textBoxInput.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
      this.textBoxInput.Location = new System.Drawing.Point(117, 15);
      this.textBoxInput.Name = "textBoxInput";
      this.textBoxInput.Size = new System.Drawing.Size(130, 20);
      this.textBoxInput.TabIndex = 2;
      this.textBoxInput.KeyDown += new System.Windows.Forms.KeyEventHandler(this.textBoxInput_KeyDown);
      // 
      // ConsoleForm
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.ClientSize = new System.Drawing.Size(257, 279);
      this.Controls.Add(this.label1);
      this.Controls.Add(this.textBoxOutput);
      this.Controls.Add(this.textBoxInput);
      this.Name = "ConsoleForm";
      this.ShowIcon = false;
      this.ShowInTaskbar = false;
      this.Text = "Системная консоль";
      this.ResumeLayout(false);
      this.PerformLayout();

    }

    #endregion

    private System.Windows.Forms.Label label1;
    private System.Windows.Forms.TextBox textBoxOutput;
    private System.Windows.Forms.TextBox textBoxInput;
  }
}