using System.Windows.Input;

namespace Roguelike.WpfClient
{
	public partial class MessageWindow
	{
		public MessageWindow()
		{
			InitializeComponent();

			text.Focus();
		}

		public string Text
		{
			get { return text.Text; }
			set { text.Text = value; }
		}

		private void exitKeyDown(object sender, KeyEventArgs e)
		{
			if (e.Key == Key.Escape)
			{
				DialogResult = false;
			}
		}
	}
}
