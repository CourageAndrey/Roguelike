using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

using Roguelike.Worlds;

namespace Roguelike.GUI
{
  /// <summary>
  /// Диалог выбора.
  /// </summary>
  public partial class SelectionDialog : Form
  {
    /// <summary>
    /// ctor.
    /// </summary>
    private SelectionDialog()
    {
      InitializeComponent();

      Size = Balance.DialogSize;
    }

    /// <summary>
    /// Вывод на экран.
    /// </summary>
    /// <param name="question">вопрос</param>
    /// <param name="choises">варианты выбора</param>
    /// <returns>выбранный вариант</returns>
    public static ChoiseObject Show(string question, List<ChoiseObject> choises)
    {
      var dialog = new SelectionDialog();
      dialog.groupBoxBorder.Text = question;

      int y = 19;
      RadioButton last = null;
      foreach (var choise in choises)
      {
        var radio = new RadioButton
        {
	        AutoSize = true,
					Text = choise.Display,
					Tag = choise,
					Enabled = choise.IsPossible,
        };
	      dialog.groupBoxBorder.Controls.Add(radio);
        radio.Location = new Point(6, y);
        radio.KeyDown += dialog.closeKeyDown;
        y += (1 + radio.Height);
        last = radio;
      }

      if (last == null)
        throw new ApplicationException("Нет выбора!");
      else
        last.Checked = true;

      if (dialog.ShowDialog() == DialogResult.OK)
        foreach (RadioButton radio in dialog.groupBoxBorder.Controls)
          if (radio.Checked)
            return (ChoiseObject) radio.Tag;
      return CancelChoise.Value;
    }

    // корректное закрытие
    private void closeKeyDown(object sender, KeyEventArgs e)
    {
      if (e.KeyCode == Keys.Enter)
        Close();
    }

    /// <summary>
    /// Determines whether the specified key is a regular input key or a special key that requires preprocessing.
    /// </summary>
    /// <returns>
    /// true if the specified key is a regular input key; otherwise, false.
    /// </returns>
    /// <param name="keyData">One of the <see cref="T:System.Windows.Forms.Keys"/> values. </param>
    protected override bool IsInputKey(Keys keyData)
    {
      // return base.IsInputKey(keyData);
      return true;
    }
  }

  #region Классы выбора

  /// <summary>
  /// Пункт выбора.
  /// </summary>
  public abstract class ChoiseObject
  {
    #region Свойства

    /// <summary>
    /// Описание.
    /// </summary>
    public string Display
    { get; private set; }

    /// <summary>
    /// Значение.
    /// </summary>
    public abstract object ValueObject
		{ get; }

		/// <summary>
		/// Можно ли выбрать.
		/// </summary>
		public bool IsPossible
		{ get; private set; }

    #endregion

    /// <summary>
    /// ctor.
    /// </summary>
		/// <param name="display">описание</param>
		/// <param name="isPossible">можно ли выбрать</param>
		protected ChoiseObject(string display, bool isPossible)
    {
			Display = display;
			IsPossible = isPossible;
    }
  }

  /// <summary>
  /// Пункт выбора (типизированный).
  /// </summary>
  public class Choise<T> : ChoiseObject
  {
    #region Свойства

    /// <summary>
    /// Значение (типизированное).
    /// </summary>
    public T Value
    { get; private set; }

    /// <summary>
    /// Значение.
    /// </summary>
    public override object ValueObject
    { get { return Value; } }

    #endregion

    /// <summary>
    /// ctor.
    /// </summary>
    /// <param name="display">описание</param>
		/// <param name="value">значение</param>
		/// <param name="isPossible">можно ли выбрать</param>
    public Choise(string display, T value, bool isPossible = true)
      : base(display, isPossible)
    {
      Value = value;
    }
  }

  /// <summary>
  /// Объект отмены действия.
  /// </summary>
  public class CancelChoise : ChoiseObject
  {
    private CancelChoise() : base("Отмена", true)
    { }

    /// <summary>
    /// Значение.
    /// </summary>
    public override object ValueObject
    { get { return null; } }

    /// <summary>
    /// Единственный экземпляр отмены.
    /// </summary>
    public static CancelChoise Value = new CancelChoise();
  }

  #endregion
}
