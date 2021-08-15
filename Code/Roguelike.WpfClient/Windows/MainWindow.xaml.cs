using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Input;

using Roguelike.Core;
using Roguelike.Core.Interfaces;

namespace Roguelike.WpfClient.Windows
{
	public partial class MainWindow
	{
		public MainWindow()
		{
			InitializeComponent();
		}

		public Game Game
		{ get; internal set; }

		private void windowLoaded(object sender, RoutedEventArgs e)
		{
			Game.StateChanged += (g, state) =>
			{
				var language = g.Language;
				switch (state)
				{
					case GameState.Win:
						new RoguelikeWindow { Title = Title, Content = language.GameWin }.ShowDialog();
						consoleControl.IsEnabled = false;
						break;
					case GameState.Defeat:
						new RoguelikeWindow { Title = Title, Content = language.GameDefeat }.ShowDialog();
						consoleControl.IsEnabled = false;
						break;
				}
			};
			Game.Start();
			consoleControl.Camera = Game.Hero;
			consoleControl.Focus();
		}

		private void keyPress(object sender, KeyEventArgs e)
		{
			var language = Game.Language;
			var hero = Game.Hero;
			ActionResult performedAction = null;
			switch (e.Key)
			{
				#region Functional keys

				case Key.F1:
					Game.UserInterface.ShowMessage(language.HelpTitle, new StringBuilder(language.HelpText));
					break;
				case Key.F2:
					Game.UserInterface.ShowCharacter(Game, hero);
					break;

				#endregion

				#region Move keys

				case Key.Left:
				case Key.NumPad4:
					performedAction = hero.TryMove(Direction.Left);
					break;
				case Key.Right:
				case Key.NumPad6:
					performedAction = hero.TryMove(Direction.Right);
					break;
				case Key.Up:
				case Key.NumPad8:
					performedAction = hero.TryMove(Direction.Up);
					break;
				case Key.Down:
				case Key.NumPad2:
					performedAction = hero.TryMove(Direction.Down);
					break;
				case Key.NumPad1:
					performedAction = hero.TryMove(Direction.DownLeft);
					break;
				case Key.NumPad3:
					performedAction = hero.TryMove(Direction.DownRight);
					break;
				case Key.NumPad7:
					performedAction = hero.TryMove(Direction.UpLeft);
					break;
				case Key.NumPad9:
					performedAction = hero.TryMove(Direction.UpRight);
					break;
				case Key.NumPad5:
					performedAction = hero.TryMove(Direction.None);
					break;

				#endregion

				#region Interaction

				case Key.H:
					var heroPosition = hero.CurrentCell.Position;
					var cells = hero.CurrentCell.Region.GetCellsAroundPoint(heroPosition.X, heroPosition.Y, heroPosition.Z);
					var interactives = cells
						.Select(c => new KeyValuePair<Direction, List<IInteractive>>(c.Key, c.Value.Objects.OfType<IInteractive>().ToList()))
						.Where(c => c.Value.Count > 0)
						.ToDictionary(c => c.Key, c => c.Value);
					if (interactives.Count > 0)
					{
						var items = new List<ListItem<Interaction>>();
						foreach (var cell in interactives)
						{
							foreach (var interactive in cell.Value)
							{
								foreach (var interaction in interactive.GetAvailableInteractions(hero))
								{
									items.Add(new ListItem<Interaction>(
										interaction,
										string.Format(
											CultureInfo.InvariantCulture,
											language.InteractionFormat,
											interaction.Name,
											cell.Key.GetName(language)),
										interaction.IsAvailable));
								}
							}
						}

						ListItem selectedItem;
						if (Game.UserInterface.TrySelectItem(Game, language.SelectInteractionPromt, items, out selectedItem))
						{
							performedAction = ((Interaction) selectedItem.ValueObject).Perform(hero);
						}
					}
					break;

				#endregion

				#region Other

				case Key.F:
					performedAction = hero.ChangeAggressive(!hero.IsAgressive);
					break;

				#endregion
			}

			if (performedAction != null)
			{
				var world = Game.World;
				world.ApplyAction(hero, performedAction);
				world.DoOneStep();
			}
		}
	}
}
