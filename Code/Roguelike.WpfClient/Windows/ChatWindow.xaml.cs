using System.Linq;
using System.Windows;

using Roguelike.Core;
using Roguelike.Core.ActiveObjects;

namespace Roguelike.WpfClient.Windows
{
	public partial class ChatWindow
	{
		public ChatWindow()
		{
			InitializeComponent();
		}

		#region Properties

		public Game Game
		{ get; set; }

		public Humanoid Interlocutor
		{ get; set; }

		public ActionResult Result
		{ get; private set; }

		#endregion

		private void loaded(object sender, RoutedEventArgs e)
		{
			var hero = Game.Hero;

			_textName.Text = Interlocutor.GetName(hero);
			_textSocialGroup.Text = Interlocutor.SocialGroup.Name;
			_textAttitude.Text = Interlocutor.GetAttitude(hero).ToString();
			_listTopics.ItemsSource = Interlocutor.GetTopics(hero).Select(topic => topic.Ask(Game.Language));
		}
	}
}
