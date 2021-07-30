namespace Roguelike.GUI
{
	partial class BodyDialog
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
			this.components = new System.ComponentModel.Container();
			this.bodyBindingSource = new System.Windows.Forms.BindingSource(this.components);
			this.partsBindingSource = new System.Windows.Forms.BindingSource(this.components);
			this.labelTotalState = new System.Windows.Forms.Label();
			this.label1 = new System.Windows.Forms.Label();
			this.groupBoxEffects = new System.Windows.Forms.GroupBox();
			this.labelBleeding = new System.Windows.Forms.Label();
			this.labelBloated = new System.Windows.Forms.Label();
			this.labelChilly = new System.Windows.Forms.Label();
			this.labelHot = new System.Windows.Forms.Label();
			this.labelAsphyxia = new System.Windows.Forms.Label();
			this.labelTired = new System.Windows.Forms.Label();
			this.labelStunt = new System.Windows.Forms.Label();
			this.labelPoisoned = new System.Windows.Forms.Label();
			this.labelHunger = new System.Windows.Forms.Label();
			this.labelSick = new System.Windows.Forms.Label();
			this.groupBoxParts = new System.Windows.Forms.GroupBox();
			this.listBoxParts = new System.Windows.Forms.ListBox();
			((System.ComponentModel.ISupportInitialize)(this.bodyBindingSource)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.partsBindingSource)).BeginInit();
			this.groupBoxEffects.SuspendLayout();
			this.groupBoxParts.SuspendLayout();
			this.SuspendLayout();
			// 
			// bodyBindingSource
			// 
			this.bodyBindingSource.DataSource = typeof(Roguelike.Body.Body);
			// 
			// partsBindingSource
			// 
			this.partsBindingSource.DataSource = typeof(Roguelike.Body.BodyPart);
			// 
			// labelTotalState
			// 
			this.labelTotalState.AutoSize = true;
			this.labelTotalState.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.bodyBindingSource, "TotalState", true));
			this.labelTotalState.DataBindings.Add(new System.Windows.Forms.Binding("ForeColor", this.bodyBindingSource, "TotalStateColor", true));
			this.labelTotalState.Location = new System.Drawing.Point(116, 7);
			this.labelTotalState.Name = "labelTotalState";
			this.labelTotalState.Size = new System.Drawing.Size(10, 13);
			this.labelTotalState.TabIndex = 4;
			this.labelTotalState.Text = "-";
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Location = new System.Drawing.Point(9, 7);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(101, 13);
			this.label1.TabIndex = 3;
			this.label1.Text = "Общее состояние:";
			// 
			// groupBoxEffects
			// 
			this.groupBoxEffects.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.groupBoxEffects.Controls.Add(this.labelBleeding);
			this.groupBoxEffects.Controls.Add(this.labelBloated);
			this.groupBoxEffects.Controls.Add(this.labelChilly);
			this.groupBoxEffects.Controls.Add(this.labelHot);
			this.groupBoxEffects.Controls.Add(this.labelAsphyxia);
			this.groupBoxEffects.Controls.Add(this.labelTired);
			this.groupBoxEffects.Controls.Add(this.labelStunt);
			this.groupBoxEffects.Controls.Add(this.labelPoisoned);
			this.groupBoxEffects.Controls.Add(this.labelHunger);
			this.groupBoxEffects.Controls.Add(this.labelSick);
			this.groupBoxEffects.Location = new System.Drawing.Point(266, 20);
			this.groupBoxEffects.Name = "groupBoxEffects";
			this.groupBoxEffects.Size = new System.Drawing.Size(87, 300);
			this.groupBoxEffects.TabIndex = 5;
			this.groupBoxEffects.TabStop = false;
			this.groupBoxEffects.Text = "Эффекты:";
			// 
			// labelBleeding
			// 
			this.labelBleeding.AutoSize = true;
			this.labelBleeding.DataBindings.Add(new System.Windows.Forms.Binding("Visible", this.bodyBindingSource, "IsBleeding", true));
			this.labelBleeding.Location = new System.Drawing.Point(6, 133);
			this.labelBleeding.Name = "labelBleeding";
			this.labelBleeding.Size = new System.Drawing.Size(77, 13);
			this.labelBleeding.TabIndex = 9;
			this.labelBleeding.Text = "кровотечение";
			// 
			// labelBloated
			// 
			this.labelBloated.AutoSize = true;
			this.labelBloated.DataBindings.Add(new System.Windows.Forms.Binding("Visible", this.bodyBindingSource, "IsBloated", true));
			this.labelBloated.Location = new System.Drawing.Point(6, 29);
			this.labelBloated.Name = "labelBloated";
			this.labelBloated.Size = new System.Drawing.Size(67, 13);
			this.labelBloated.TabIndex = 1;
			this.labelBloated.Text = "переедание";
			// 
			// labelChilly
			// 
			this.labelChilly.AutoSize = true;
			this.labelChilly.DataBindings.Add(new System.Windows.Forms.Binding("Visible", this.bodyBindingSource, "IsChilly", true));
			this.labelChilly.Location = new System.Drawing.Point(6, 81);
			this.labelChilly.Name = "labelChilly";
			this.labelChilly.Size = new System.Drawing.Size(36, 13);
			this.labelChilly.TabIndex = 5;
			this.labelChilly.Text = "холод";
			// 
			// labelHot
			// 
			this.labelHot.AutoSize = true;
			this.labelHot.DataBindings.Add(new System.Windows.Forms.Binding("Visible", this.bodyBindingSource, "IsHot", true));
			this.labelHot.Location = new System.Drawing.Point(6, 94);
			this.labelHot.Name = "labelHot";
			this.labelHot.Size = new System.Drawing.Size(33, 13);
			this.labelHot.TabIndex = 6;
			this.labelHot.Text = "жара";
			// 
			// labelAsphyxia
			// 
			this.labelAsphyxia.AutoSize = true;
			this.labelAsphyxia.DataBindings.Add(new System.Windows.Forms.Binding("Visible", this.bodyBindingSource, "IsAsphyxia", true));
			this.labelAsphyxia.Location = new System.Drawing.Point(6, 107);
			this.labelAsphyxia.Name = "labelAsphyxia";
			this.labelAsphyxia.Size = new System.Drawing.Size(67, 13);
			this.labelAsphyxia.TabIndex = 7;
			this.labelAsphyxia.Text = "нет воздуха";
			// 
			// labelTired
			// 
			this.labelTired.AutoSize = true;
			this.labelTired.DataBindings.Add(new System.Windows.Forms.Binding("Visible", this.bodyBindingSource, "IsTired", true));
			this.labelTired.Location = new System.Drawing.Point(6, 42);
			this.labelTired.Name = "labelTired";
			this.labelTired.Size = new System.Drawing.Size(58, 13);
			this.labelTired.TabIndex = 2;
			this.labelTired.Text = "усталость";
			// 
			// labelStunt
			// 
			this.labelStunt.AutoSize = true;
			this.labelStunt.DataBindings.Add(new System.Windows.Forms.Binding("Visible", this.bodyBindingSource, "IsStunned", true));
			this.labelStunt.Location = new System.Drawing.Point(6, 55);
			this.labelStunt.Name = "labelStunt";
			this.labelStunt.Size = new System.Drawing.Size(61, 13);
			this.labelStunt.TabIndex = 3;
			this.labelStunt.Text = "оглушение";
			// 
			// labelPoisoned
			// 
			this.labelPoisoned.AutoSize = true;
			this.labelPoisoned.DataBindings.Add(new System.Windows.Forms.Binding("Visible", this.bodyBindingSource, "IsPoisoned", true));
			this.labelPoisoned.Location = new System.Drawing.Point(6, 68);
			this.labelPoisoned.Name = "labelPoisoned";
			this.labelPoisoned.Size = new System.Drawing.Size(66, 13);
			this.labelPoisoned.TabIndex = 4;
			this.labelPoisoned.Text = "отравление";
			// 
			// labelHunger
			// 
			this.labelHunger.AutoSize = true;
			this.labelHunger.DataBindings.Add(new System.Windows.Forms.Binding("Visible", this.bodyBindingSource, "IsHungred", true));
			this.labelHunger.Location = new System.Drawing.Point(6, 16);
			this.labelHunger.Name = "labelHunger";
			this.labelHunger.Size = new System.Drawing.Size(36, 13);
			this.labelHunger.TabIndex = 0;
			this.labelHunger.Text = "голод";
			// 
			// labelSick
			// 
			this.labelSick.AutoSize = true;
			this.labelSick.DataBindings.Add(new System.Windows.Forms.Binding("Visible", this.bodyBindingSource, "IsSick", true));
			this.labelSick.Location = new System.Drawing.Point(6, 120);
			this.labelSick.Name = "labelSick";
			this.labelSick.Size = new System.Drawing.Size(49, 13);
			this.labelSick.TabIndex = 8;
			this.labelSick.Text = "болезнь";
			// 
			// groupBoxParts
			// 
			this.groupBoxParts.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.groupBoxParts.Controls.Add(this.listBoxParts);
			this.groupBoxParts.Location = new System.Drawing.Point(12, 23);
			this.groupBoxParts.Name = "groupBoxParts";
			this.groupBoxParts.Size = new System.Drawing.Size(248, 300);
			this.groupBoxParts.TabIndex = 6;
			this.groupBoxParts.TabStop = false;
			this.groupBoxParts.Text = "Органы:";
			// 
			// listBoxParts
			// 
			this.listBoxParts.BackColor = System.Drawing.Color.Gray;
			this.listBoxParts.DataSource = this.partsBindingSource;
			this.listBoxParts.Dock = System.Windows.Forms.DockStyle.Fill;
			this.listBoxParts.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
			this.listBoxParts.FormattingEnabled = true;
			this.listBoxParts.Location = new System.Drawing.Point(3, 16);
			this.listBoxParts.Name = "listBoxParts";
			this.listBoxParts.Size = new System.Drawing.Size(242, 281);
			this.listBoxParts.TabIndex = 0;
			// 
			// BodyDialog
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(365, 335);
			this.Controls.Add(this.labelTotalState);
			this.Controls.Add(this.label1);
			this.Controls.Add(this.groupBoxEffects);
			this.Controls.Add(this.groupBoxParts);
			this.Name = "BodyDialog";
			this.Text = "Состояние:";
			((System.ComponentModel.ISupportInitialize)(this.bodyBindingSource)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.partsBindingSource)).EndInit();
			this.groupBoxEffects.ResumeLayout(false);
			this.groupBoxEffects.PerformLayout();
			this.groupBoxParts.ResumeLayout(false);
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.BindingSource bodyBindingSource;
		private System.Windows.Forms.BindingSource partsBindingSource;
		private System.Windows.Forms.Label labelTotalState;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.GroupBox groupBoxEffects;
		private System.Windows.Forms.Label labelBleeding;
		private System.Windows.Forms.Label labelBloated;
		private System.Windows.Forms.Label labelChilly;
		private System.Windows.Forms.Label labelHot;
		private System.Windows.Forms.Label labelAsphyxia;
		private System.Windows.Forms.Label labelTired;
		private System.Windows.Forms.Label labelStunt;
		private System.Windows.Forms.Label labelPoisoned;
		private System.Windows.Forms.Label labelHunger;
		private System.Windows.Forms.Label labelSick;
		private System.Windows.Forms.GroupBox groupBoxParts;
		private System.Windows.Forms.ListBox listBoxParts;

	}
}