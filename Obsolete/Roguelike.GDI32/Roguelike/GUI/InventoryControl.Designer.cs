namespace Roguelike.GUI
{
	partial class InventoryControl
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

		#region Component Designer generated code

		/// <summary> 
		/// Required method for Designer support - do not modify 
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			this.components = new System.ComponentModel.Container();
			this.labelWeightTotal = new System.Windows.Forms.Label();
			this.dataGridViewItems = new System.Windows.Forms.DataGridView();
			this.nameDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.classNameDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.weightDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.itemBindingSource = new System.Windows.Forms.BindingSource(this.components);
			this.label4 = new System.Windows.Forms.Label();
			((System.ComponentModel.ISupportInitialize)(this.dataGridViewItems)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.itemBindingSource)).BeginInit();
			this.SuspendLayout();
			// 
			// labelWeightTotal
			// 
			this.labelWeightTotal.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.labelWeightTotal.AutoSize = true;
			this.labelWeightTotal.Location = new System.Drawing.Point(296, 148);
			this.labelWeightTotal.Name = "labelWeightTotal";
			this.labelWeightTotal.Size = new System.Drawing.Size(13, 13);
			this.labelWeightTotal.TabIndex = 15;
			this.labelWeightTotal.Text = "0";
			// 
			// dataGridViewItems
			// 
			this.dataGridViewItems.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.dataGridViewItems.AutoGenerateColumns = false;
			this.dataGridViewItems.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
			this.dataGridViewItems.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.nameDataGridViewTextBoxColumn,
            this.classNameDataGridViewTextBoxColumn,
            this.weightDataGridViewTextBoxColumn});
			this.dataGridViewItems.DataSource = this.itemBindingSource;
			this.dataGridViewItems.Location = new System.Drawing.Point(3, 3);
			this.dataGridViewItems.Name = "dataGridViewItems";
			this.dataGridViewItems.RowHeadersVisible = false;
			this.dataGridViewItems.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
			this.dataGridViewItems.Size = new System.Drawing.Size(337, 142);
			this.dataGridViewItems.TabIndex = 13;
			// 
			// nameDataGridViewTextBoxColumn
			// 
			this.nameDataGridViewTextBoxColumn.DataPropertyName = "Name";
			this.nameDataGridViewTextBoxColumn.HeaderText = "Предмет";
			this.nameDataGridViewTextBoxColumn.Name = "nameDataGridViewTextBoxColumn";
			this.nameDataGridViewTextBoxColumn.ReadOnly = true;
			this.nameDataGridViewTextBoxColumn.Width = 150;
			// 
			// classNameDataGridViewTextBoxColumn
			// 
			this.classNameDataGridViewTextBoxColumn.DataPropertyName = "ClassName";
			this.classNameDataGridViewTextBoxColumn.HeaderText = "Класс";
			this.classNameDataGridViewTextBoxColumn.Name = "classNameDataGridViewTextBoxColumn";
			this.classNameDataGridViewTextBoxColumn.ReadOnly = true;
			// 
			// weightDataGridViewTextBoxColumn
			// 
			this.weightDataGridViewTextBoxColumn.DataPropertyName = "Weight";
			this.weightDataGridViewTextBoxColumn.HeaderText = "Вес, кг";
			this.weightDataGridViewTextBoxColumn.Name = "weightDataGridViewTextBoxColumn";
			this.weightDataGridViewTextBoxColumn.ReadOnly = true;
			this.weightDataGridViewTextBoxColumn.Width = 75;
			// 
			// itemBindingSource
			// 
			this.itemBindingSource.DataSource = typeof(Roguelike.Items.Item);
			// 
			// label4
			// 
			this.label4.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.label4.AutoSize = true;
			this.label4.Location = new System.Drawing.Point(-3, 148);
			this.label4.Name = "label4";
			this.label4.Size = new System.Drawing.Size(142, 13);
			this.label4.TabIndex = 16;
			this.label4.Text = "Общий вес всех вещей, кг";
			// 
			// InventoryControl
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.Controls.Add(this.labelWeightTotal);
			this.Controls.Add(this.dataGridViewItems);
			this.Controls.Add(this.label4);
			this.Name = "InventoryControl";
			this.Size = new System.Drawing.Size(343, 161);
			((System.ComponentModel.ISupportInitialize)(this.dataGridViewItems)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.itemBindingSource)).EndInit();
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.Label labelWeightTotal;
		private System.Windows.Forms.DataGridView dataGridViewItems;
		private System.Windows.Forms.DataGridViewTextBoxColumn nameDataGridViewTextBoxColumn;
		private System.Windows.Forms.DataGridViewTextBoxColumn classNameDataGridViewTextBoxColumn;
		private System.Windows.Forms.DataGridViewTextBoxColumn weightDataGridViewTextBoxColumn;
		private System.Windows.Forms.BindingSource itemBindingSource;
		private System.Windows.Forms.Label label4;
	}
}
