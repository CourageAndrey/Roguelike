namespace Roguelike.GUI
{
	partial class InventoryDialog
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
			this.labelWeightMax = new System.Windows.Forms.Label();
			this.label2 = new System.Windows.Forms.Label();
			this.labelWeightTotal = new System.Windows.Forms.Label();
			this.label4 = new System.Windows.Forms.Label();
			this.tabControl = new System.Windows.Forms.TabControl();
			this.SuspendLayout();
			// 
			// labelWeightMax
			// 
			this.labelWeightMax.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.labelWeightMax.AutoSize = true;
			this.labelWeightMax.Location = new System.Drawing.Point(308, 198);
			this.labelWeightMax.Name = "labelWeightMax";
			this.labelWeightMax.Size = new System.Drawing.Size(13, 13);
			this.labelWeightMax.TabIndex = 12;
			this.labelWeightMax.Text = "0";
			// 
			// label2
			// 
			this.label2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.label2.AutoSize = true;
			this.label2.Location = new System.Drawing.Point(9, 198);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(282, 13);
			this.label2.TabIndex = 9;
			this.label2.Text = "Максимальный вес, переносимый без неудобства, кг";
			// 
			// labelWeightTotal
			// 
			this.labelWeightTotal.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.labelWeightTotal.AutoSize = true;
			this.labelWeightTotal.Location = new System.Drawing.Point(308, 185);
			this.labelWeightTotal.Name = "labelWeightTotal";
			this.labelWeightTotal.Size = new System.Drawing.Size(13, 13);
			this.labelWeightTotal.TabIndex = 10;
			this.labelWeightTotal.Text = "0";
			// 
			// label4
			// 
			this.label4.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.label4.AutoSize = true;
			this.label4.Location = new System.Drawing.Point(9, 185);
			this.label4.Name = "label4";
			this.label4.Size = new System.Drawing.Size(142, 13);
			this.label4.TabIndex = 11;
			this.label4.Text = "Общий вес всех вещей, кг";
			// 
			// tabControl
			// 
			this.tabControl.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.tabControl.Location = new System.Drawing.Point(12, 12);
			this.tabControl.Name = "tabControl";
			this.tabControl.SelectedIndex = 0;
			this.tabControl.Size = new System.Drawing.Size(341, 170);
			this.tabControl.TabIndex = 13;
			// 
			// InventoryDialog
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(365, 218);
			this.Controls.Add(this.tabControl);
			this.Controls.Add(this.labelWeightMax);
			this.Controls.Add(this.labelWeightTotal);
			this.Controls.Add(this.label2);
			this.Controls.Add(this.label4);
			this.Name = "InventoryDialog";
			this.Text = "Вещи в рюкзаке:";
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.Label labelWeightMax;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.Label labelWeightTotal;
		private System.Windows.Forms.Label label4;
		private System.Windows.Forms.TabControl tabControl;
	}
}