using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;

using Roguelike.Core;
using Roguelike.Core.ActiveObjects;
using Roguelike.Core.Interfaces;
using Roguelike.Core.Items;
using Roguelike.Core.Localization;

namespace Roguelike.Console
{
	class Program
	{
		static void Main(string[] args)
		{
			var language = Language.CreateDefault();
			var ui = new ConsoleUi();
			var game = new Game(ui, language);
			var world = game.World;
			var hero = world.Hero;
			game.StateChanged += (g, state) =>
			{
				switch (state)
				{
					case GameState.Win:
						ui.ShowMessage(null, new StringBuilder(language.GameWin));
						Environment.Exit(0);
						break;
					case GameState.Defeat:
						ui.ShowMessage(null, new StringBuilder(language.GameDefeat));
						Environment.Exit(0);
						break;
				}
			};
			game.Start();

			ui.Camera = hero.Camera;

			do
			{
				var key = System.Console.ReadKey(true);

				KeyHandlerDelegate keyHandler;
				var performedAction = _keyHandlers.TryGetValue(key.Key, out keyHandler)
					? keyHandler(language, ui, game, world, hero)
					: null;

				if (performedAction != null)
				{
					world.ApplyAction(hero, new ActionResult(performedAction.Longevity.Scale(hero.Speed), performedAction.LogMessages));
					world.DoOneStep();
				}
			} while (true);
		}

		#region Keyboard handlers

		private delegate ActionResult KeyHandlerDelegate(
			Language language,
			ConsoleUi ui,
			Game game,
			World world,
			Hero hero);

		private static readonly IDictionary<ConsoleKey, KeyHandlerDelegate> _keyHandlers = new Dictionary<ConsoleKey, KeyHandlerDelegate>
		{
			{ ConsoleKey.F1, HandleShowHelp },
			{ ConsoleKey.F2, HandleShowCharacter },
			{ ConsoleKey.LeftArrow, HandleMoveLeft },
			{ ConsoleKey.NumPad4, HandleMoveLeft },
			{ ConsoleKey.RightArrow, HandleMoveRight },
			{ ConsoleKey.NumPad6, HandleMoveRight },
			{ ConsoleKey.UpArrow, HandleMoveUp },
			{ ConsoleKey.NumPad8, HandleMoveUp },
			{ ConsoleKey.DownArrow, HandleMoveDown },
			{ ConsoleKey.NumPad2, HandleMoveDown },
			{ ConsoleKey.NumPad1, HandleMoveDownLeft },
			{ ConsoleKey.NumPad3, HandleMoveDownRight },
			{ ConsoleKey.NumPad7, HandleMoveUpLeft },
			{ ConsoleKey.NumPad9, HandleMoveUpRight },
			{ ConsoleKey.NumPad5, HandleMoveNone },
			{ ConsoleKey.H, HandleInteract },
			{ ConsoleKey.F, HandleChangeAggressive },
			{ ConsoleKey.D, HandleDropItem },
			{ ConsoleKey.W, HandleSelectWeapon },
		};

		#region Functional keys

		private static ActionResult HandleShowHelp(
			Language language,
			ConsoleUi ui,
			Game game,
			World world,
			Hero hero)
		{
			ui.ShowMessage(language.HelpTitle, new StringBuilder(language.HelpText));
			return null;
		}

		private static ActionResult HandleShowCharacter(
			Language language,
			ConsoleUi ui,
			Game game,
			World world,
			Hero hero)
		{
			ui.ShowCharacter(game, hero);
			return null;
		}

		#endregion

		#region Move keys

		private static ActionResult HandleMoveLeft(
			Language language,
			ConsoleUi ui,
			Game game,
			World world,
			Hero hero)
		{
			return hero.TryMove(Direction.Left);
		}

		private static ActionResult HandleMoveRight(
			Language language,
			ConsoleUi ui,
			Game game,
			World world,
			Hero hero)
		{
			return hero.TryMove(Direction.Right);
		}

		private static ActionResult HandleMoveUp(
			Language language,
			ConsoleUi ui,
			Game game,
			World world,
			Hero hero)
		{
			return hero.TryMove(Direction.Up);
		}

		private static ActionResult HandleMoveDown(
			Language language,
			ConsoleUi ui,
			Game game,
			World world,
			Hero hero)
		{
			return hero.TryMove(Direction.Down);
		}

		private static ActionResult HandleMoveDownLeft(
			Language language,
			ConsoleUi ui,
			Game game,
			World world,
			Hero hero)
		{
			return hero.TryMove(Direction.DownLeft);
		}

		private static ActionResult HandleMoveDownRight(
			Language language,
			ConsoleUi ui,
			Game game,
			World world,
			Hero hero)
		{
			return hero.TryMove(Direction.DownRight);
		}

		private static ActionResult HandleMoveUpLeft(
			Language language,
			ConsoleUi ui,
			Game game,
			World world,
			Hero hero)
		{
			return hero.TryMove(Direction.UpLeft);
		}

		private static ActionResult HandleMoveUpRight(
			Language language,
			ConsoleUi ui,
			Game game,
			World world,
			Hero hero)
		{
			return hero.TryMove(Direction.UpRight);
		}

		private static ActionResult HandleMoveNone(
			Language language,
			ConsoleUi ui,
			Game game,
			World world,
			Hero hero)
		{
			return hero.TryMove(Direction.None);
		}

		#endregion

		#region Interaction

		private static ActionResult HandleInteract(
			Language language,
			ConsoleUi ui,
			Game game,
			World world,
			Hero hero)
		{
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
									cell.Key.GetName(game.Language)),
								interaction.IsAvailable));
						}
					}
				}

				ListItem selectedInteractionItem;
				if (ui.TrySelectItem(game, language.SelectInteractionPromt, items, out selectedInteractionItem))
				{
					return ((Interaction) selectedInteractionItem.ValueObject).Perform(hero);
				}
			}

			return null;
		}

		#endregion

		#region Other

		private static ActionResult HandleChangeAggressive(
			Language language,
			ConsoleUi ui,
			Game game,
			World world,
			Hero hero)
		{
			return hero.ChangeAggressive(!hero.IsAgressive);
		}

		private static ActionResult HandleDropItem(
			Language language,
			ConsoleUi ui,
			Game game,
			World world,
			Hero hero)
		{
			var itemsToDrop = hero.Inventory.Select(i => new ListItem<IItem>(i, i.ToString()));

			ListItem selectedItemToDrop;
			if (ui.TrySelectItem(game, language.SelectItemToDropPromt, itemsToDrop, out selectedItemToDrop))
			{
				return hero.DropItem((IItem)selectedItemToDrop.ValueObject);
			}
			else
			{
				return null;
			}
		}

		private static ActionResult HandleSelectWeapon(
			Language language,
			ConsoleUi ui,
			Game game,
			World world,
			Hero hero)
		{
			var weapons = new List<IWeapon>(hero.Inventory.OfType<IWeapon>()).ToList();
			if (hero.WeaponToFight.GetType() != typeof(Unarmed))
			{
				weapons.Remove(hero.WeaponToFight);
				weapons.Add(new Unarmed(hero));
			}

			IWeapon selectedWeapon;
			ListItem selectedWeaponItem;
			if (ui.TrySelectItem(game, language.SelectWeaponPromt, weapons.Select(w => new ListItem<IWeapon>(w, w.ToString())), out selectedWeaponItem))
			{
				selectedWeapon = ((ListItem<IWeapon>) selectedWeaponItem).Value;
			}
			else
			{
				selectedWeapon = hero.WeaponToFight;
			}

			return hero.ChangeWeapon(selectedWeapon);
		}

		#endregion

		#endregion
	}
}
