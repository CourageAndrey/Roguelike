using System.Collections.Generic;
using System.Globalization;
using System.Linq;

using Roguelike.Core;
using Roguelike.Core.ActiveObjects;
using Roguelike.Core.Interfaces;
using Roguelike.Core.Items;
using Roguelike.Core.Localization;

namespace Roguelike.Console
{
	partial class Program
	{
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
									cell.Key.GetName(game.Language.Directions)),
								interaction.IsAvailable));
						}
					}
				}

				ListItem selectedInteractionItem;
				if (ui.TrySelectItem(game, language.Promts.SelectInteraction, items, out selectedInteractionItem))
				{
					return ((Interaction) selectedInteractionItem.ValueObject).Perform(hero);
				}
			}

			return null;
		}

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
			if (ui.TrySelectItem(game, language.Promts.SelectItemToDrop, itemsToDrop, out selectedItemToDrop))
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
			if (ui.TrySelectItem(game, language.Promts.SelectWeapon, weapons.Select(w => new ListItem<IWeapon>(w, w.ToString())), out selectedWeaponItem))
			{
				selectedWeapon = ((ListItem<IWeapon>) selectedWeaponItem).Value;
			}
			else
			{
				selectedWeapon = hero.WeaponToFight;
			}

			return hero.ChangeWeapon(selectedWeapon);
		}
	}
}
