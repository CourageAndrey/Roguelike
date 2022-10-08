using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;

using Roguelike.Core;
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

				ActionResult performedAction = null;
				switch (key.Key)
				{
					#region Functional keys

					case ConsoleKey.F1:
						ui.ShowMessage(language.HelpTitle, new StringBuilder(language.HelpText));
						break;
					case ConsoleKey.F2:
						ui.ShowCharacter(game, hero);
						break;

					#endregion

					#region Move keys

					case ConsoleKey.LeftArrow:
					case ConsoleKey.NumPad4:
						performedAction = hero.TryMove(Direction.Left);
						break;
					case ConsoleKey.RightArrow:
					case ConsoleKey.NumPad6:
						performedAction = hero.TryMove(Direction.Right);
						break;
					case ConsoleKey.UpArrow:
					case ConsoleKey.NumPad8:
						performedAction = hero.TryMove(Direction.Up);
						break;
					case ConsoleKey.DownArrow:
					case ConsoleKey.NumPad2:
						performedAction = hero.TryMove(Direction.Down);
						break;
					case ConsoleKey.NumPad1:
						performedAction = hero.TryMove(Direction.DownLeft);
						break;
					case ConsoleKey.NumPad3:
						performedAction = hero.TryMove(Direction.DownRight);
						break;
					case ConsoleKey.NumPad7:
						performedAction = hero.TryMove(Direction.UpLeft);
						break;
					case ConsoleKey.NumPad9:
						performedAction = hero.TryMove(Direction.UpRight);
						break;
					case ConsoleKey.NumPad5:
						performedAction = hero.TryMove(Direction.None);
						break;

					#endregion

					#region Interaction

					case ConsoleKey.H:
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
								performedAction = ((Interaction) selectedInteractionItem.ValueObject).Perform(hero);
							}
						}
						break;

					#endregion

					#region Other

					case ConsoleKey.F:
						performedAction = hero.ChangeAggressive(!hero.IsAgressive);
						break;

					case ConsoleKey.D:
						var itemsToDrop = hero.Inventory.Select(i => new ListItem<IItem>(i, i.ToString()));

						ListItem selectedItemToDrop;
						if (ui.TrySelectItem(game, language.SelectItemToDropPromt, itemsToDrop, out selectedItemToDrop))
						{
							performedAction = hero.DropItem((IItem) selectedItemToDrop.ValueObject);
						}
						break;

					case ConsoleKey.W:
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

						performedAction = hero.ChangeWeapon(selectedWeapon);
						break;

						#endregion
				}

				if (performedAction != null)
				{
					world.ApplyAction(hero, new ActionResult(performedAction.Longevity.Scale(hero.Speed), performedAction.LogMessages));
					world.DoOneStep();
				}
			} while (true);
		}
	}
}
