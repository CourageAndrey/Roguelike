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

			_dialogArea.NavigateToString("<html><head /><body><p/></body></html>"); // because first call is always skipped
		}

		private void topicMouseDown(object sender, MouseButtonEventArgs e)
		{
			var hero = Game.Hero;

			var contextControl = getContextControl<Label>((FrameworkElement) e.OriginalSource);
			var listItem = (ListItem<Topic>) (contextControl != null
				? contextControl.DataContext
				: ((ListBox) e.Source).SelectedItem);
			var topic = listItem.Value;

			var text = Interlocutor.Discuss(hero, topic, Game.Language);

			_dialogArea.NavigateToString("<html><head /><body><p>" + text.PlainString + "</p></body></html>");
		}

		private static ControlT getContextControl<ControlT>(FrameworkElement control)
			where ControlT : FrameworkElement
		{
			do
			{
				if (control is ControlT)
				{
					return control as ControlT;
				}
				else
				{
					control = control.TemplatedParent as FrameworkElement;
				}
			} while (control != null);

			return null;
		}
	}
}
