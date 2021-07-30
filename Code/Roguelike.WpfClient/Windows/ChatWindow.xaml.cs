using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

using Roguelike.Core;
using Roguelike.Core.ActiveObjects;
using Roguelike.Core.Chat;

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
			_listTopics.ItemsSource = Interlocutor.GetTopics(hero).Select(topic => new ListItem<Topic>(topic, topic.Ask(Game.Language)));
		}

		private void topicMouseDown(object sender, MouseButtonEventArgs e)
		{
			var hero = Game.Hero;

			var parentControl = ((FrameworkElement) e.OriginalSource).TemplatedParent;
			var contextControl = (parentControl as Label) ?? (((ContentPresenter) parentControl).TemplatedParent as Label);
			var listItem = (ListItem<Topic>) contextControl.DataContext;
			var topic = listItem.Value;

			var text = Interlocutor.Discuss(hero, topic, Game.Language);

#warning Works from time to time, nned to load correct HTML.
			_dialogArea.NavigateToString(text.PlainString);
		}
	}
}
