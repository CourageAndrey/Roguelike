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
	}
}
