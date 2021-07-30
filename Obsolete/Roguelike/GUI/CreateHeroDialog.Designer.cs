namespace Roguelike.GUI
{
  partial class CreateHeroDialog
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
        this.label1 = new System.Windows.Forms.Label();
        this.label2 = new System.Windows.Forms.Label();
        this.textBoxName = new System.Windows.Forms.TextBox();
        this.startSettingsBindingSource = new System.Windows.Forms.BindingSource(this.components);
        this.textBoxNickname = new System.Windows.Forms.TextBox();
        this.buttonNext = new System.Windows.Forms.Button();
        this.buttonCancel = new System.Windows.Forms.Button();
        this.checkBoxSex = new System.Windows.Forms.CheckBox();
        this.comboBoxBonus = new System.Windows.Forms.ComboBox();
        this.startBonusBindingSource = new System.Windows.Forms.BindingSource(this.components);
        this.label3 = new System.Windows.Forms.Label();
        ((System.ComponentModel.ISupportInitialize)(this.startSettingsBindingSource)).BeginInit();
        ((System.ComponentModel.ISupportInitialize)(this.startBonusBindingSource)).BeginInit();
        this.SuspendLayout();
        // 
        // label1
        // 
        this.label1.AutoSize = true;
        this.label1.Location = new System.Drawing.Point(10, 14);
        this.label1.Name = "label1";
        this.label1.Size = new System.Drawing.Size(32, 13);
        this.label1.TabIndex = 2;
        this.label1.Text = "Имя:";
        // 
        // label2
        // 
        this.label2.AutoSize = true;
        this.label2.Location = new System.Drawing.Point(10, 38);
        this.label2.Name = "label2";
        this.label2.Size = new System.Drawing.Size(63, 13);
        this.label2.TabIndex = 4;
        this.label2.Text = "Прозвище:";
        // 
        // textBoxName
        // 
        this.textBoxName.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                    | System.Windows.Forms.AnchorStyles.Right)));
        this.textBoxName.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.startSettingsBindingSource, "Name", true));
        this.textBoxName.Location = new System.Drawing.Point(74, 14);
        this.textBoxName.Name = "textBoxName";
        this.textBoxName.Size = new System.Drawing.Size(246, 20);
        this.textBoxName.TabIndex = 3;
        // 
        // startSettingsBindingSource
        // 
        this.startSettingsBindingSource.DataSource = typeof(Roguelike.Worlds.StartSettings);
        // 
        // textBoxNickname
        // 
        this.textBoxNickname.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                    | System.Windows.Forms.AnchorStyles.Right)));
        this.textBoxNickname.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.startSettingsBindingSource, "Nickname", true));
        this.textBoxNickname.Location = new System.Drawing.Point(75, 38);
        this.textBoxNickname.Name = "textBoxNickname";
        this.textBoxNickname.Size = new System.Drawing.Size(246, 20);
        this.textBoxNickname.TabIndex = 5;
        // 
        // buttonNext
        // 
        this.buttonNext.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
        this.buttonNext.DialogResult = System.Windows.Forms.DialogResult.OK;
        this.buttonNext.Location = new System.Drawing.Point(215, 129);
        this.buttonNext.Name = "buttonNext";
        this.buttonNext.Size = new System.Drawing.Size(105, 21);
        this.buttonNext.TabIndex = 0;
        this.buttonNext.Text = "Продолжить >>>";
        this.buttonNext.UseVisualStyleBackColor = true;
        // 
        // buttonCancel
        // 
        this.buttonCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
        this.buttonCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
        this.buttonCancel.Location = new System.Drawing.Point(10, 129);
        this.buttonCancel.Name = "buttonCancel";
        this.buttonCancel.Size = new System.Drawing.Size(60, 21);
        this.buttonCancel.TabIndex = 1;
        this.buttonCancel.Text = "Отмена";
        // 
        // checkBoxSex
        // 
        this.checkBoxSex.AutoSize = true;
        this.checkBoxSex.Checked = true;
        this.checkBoxSex.CheckState = System.Windows.Forms.CheckState.Checked;
        this.checkBoxSex.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.startSettingsBindingSource, "SexString", true));
        this.checkBoxSex.DataBindings.Add(new System.Windows.Forms.Binding("Checked", this.startSettingsBindingSource, "SexMale", true));
        this.checkBoxSex.Location = new System.Drawing.Point(13, 59);
        this.checkBoxSex.Name = "checkBoxSex";
        this.checkBoxSex.Size = new System.Drawing.Size(268, 17);
        this.checkBoxSex.TabIndex = 6;
        this.checkBoxSex.Text = "Пол М / Ж   (установленная галочка - мужской)";
        // 
        // comboBoxBonus
        // 
        this.comboBoxBonus.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                    | System.Windows.Forms.AnchorStyles.Right)));
        this.comboBoxBonus.DataBindings.Add(new System.Windows.Forms.Binding("SelectedValue", this.startSettingsBindingSource, "Bonus", true));
        this.comboBoxBonus.DataSource = this.startBonusBindingSource;
        this.comboBoxBonus.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
        this.comboBoxBonus.FormattingEnabled = true;
        this.comboBoxBonus.Location = new System.Drawing.Point(75, 82);
        this.comboBoxBonus.Name = "comboBoxBonus";
        this.comboBoxBonus.Size = new System.Drawing.Size(246, 21);
        this.comboBoxBonus.TabIndex = 8;
        // 
        // startBonusBindingSource
        // 
        this.startBonusBindingSource.DataSource = typeof(Roguelike.Worlds.StartBonus);
        // 
        // label3
        // 
        this.label3.AutoSize = true;
        this.label3.Location = new System.Drawing.Point(10, 84);
        this.label3.Name = "label3";
        this.label3.Size = new System.Drawing.Size(40, 13);
        this.label3.TabIndex = 7;
        this.label3.Text = "Бонус:";
        // 
        // CreateHeroDialog
        // 
        this.AcceptButton = this.buttonNext;
        this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
        this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
        this.CancelButton = this.buttonCancel;
        this.ClientSize = new System.Drawing.Size(331, 162);
        this.Controls.Add(this.label3);
        this.Controls.Add(this.comboBoxBonus);
        this.Controls.Add(this.checkBoxSex);
        this.Controls.Add(this.buttonCancel);
        this.Controls.Add(this.buttonNext);
        this.Controls.Add(this.textBoxNickname);
        this.Controls.Add(this.textBoxName);
        this.Controls.Add(this.label2);
        this.Controls.Add(this.label1);
        this.Name = "CreateHeroDialog";
        this.ShowIcon = false;
        this.ShowInTaskbar = false;
        this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
        this.Text = "Создание героя";
        this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.CreateHeroDialog_FormClosing);
        ((System.ComponentModel.ISupportInitialize)(this.startSettingsBindingSource)).EndInit();
        ((System.ComponentModel.ISupportInitialize)(this.startBonusBindingSource)).EndInit();
        this.ResumeLayout(false);
        this.PerformLayout();

    }

    #endregion

    private System.Windows.Forms.Label label1;
    private System.Windows.Forms.Label label2;
    private System.Windows.Forms.TextBox textBoxName;
    private System.Windows.Forms.TextBox textBoxNickname;
    private System.Windows.Forms.Button buttonNext;
    private System.Windows.Forms.Button buttonCancel;
    private System.Windows.Forms.CheckBox checkBoxSex;
    private System.Windows.Forms.BindingSource startSettingsBindingSource;
    private System.Windows.Forms.ComboBox comboBoxBonus;
    private System.Windows.Forms.BindingSource startBonusBindingSource;
    private System.Windows.Forms.Label label3;
  }
}