using System.Drawing;
using System.Windows.Forms;

namespace Roguelike.GUI
{
	/// <summary>
	/// Диалог состояния тела.
	/// </summary>
	public partial class BodyDialog : Form
	{
		/// <summary>
		/// ctor.
		/// </summary>
		/// <param name="body">тело</param>
		public BodyDialog(Body.Body body)
		{
			InitializeComponent();

			bodyBindingSource.DataSource = this.body = body;
			partsBindingSource.DataSource = body.Parts;
			resetBinding();
		}

		private readonly Body.Body body;

		private void listBoxParts_DrawItem(object sender, DrawItemEventArgs e)
		{
			var part = body.Parts[e.Index];
			using (var brush = new SolidBrush(part.StateColor))
				e.Graphics.DrawString(part.ToString(), Font, brush, e.Bounds);
		}

		private void resetBinding()
		{
			bodyBindingSource.ResetBindings(false);
			partsBindingSource.ResetBindings(false);
		}
	}
}
