using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;

using Roguelike.Core;
using Roguelike.Core.Aspects;
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
			IHero hero)
		{
			var interactive = hero.CurrentCell.Objects.OfType<IInteractive>().FirstOrDefault();
			if (interactive != null)
			{
				var items = interactive
					.GetAvailableInteractions(hero)
					.Select(i => new ListItem<Interaction>(i, i.Name, i.IsAvailable))
					.ToList();

				ListItem selectedInteractionItem;
				if (ui.TrySelectItem(game, language.Promts.SelectInteraction, items, out selectedInteractionItem))
				{
					return ((Interaction) selectedInteractionItem.ValueObject).Perform(hero);
				}
			}

			return null;
		}

		internal static TargetT SelectTarget<TargetT>(
			ConsoleUi ui,
			Game game,
			IHero hero,
			string promt,
			Func<TargetT, bool> filter = null)
			where TargetT : class
		{
			if (filter == null)
			{
				filter = i => true;
			}

			var heroPosition = hero.GetPosition();
			var cells = hero.GetRegion().GetCellsAroundPoint(heroPosition);
			cells.Remove(Direction.None);
			var items = new List<ListItem>();
			foreach (var cell in cells)
			{
				var cellTarget = cell.Value.Objects.OfType<TargetT>().FirstOrDefault(filter);
				if (cellTarget != null)
				{
					items.Add(new ListItem<TargetT>(cellTarget, cell.Key.GetName(game.Language.Directions)));
				}
			}

			if (items.Count > 1)
			{
				ListItem selectedItem;
				if (ui.TrySelectItem(game, promt, items, out selectedItem))
				{
					return ((ListItem<TargetT>) selectedItem).Value;
				}
			}
			else if (items.Count == 1)
			{
				return ((ListItem<TargetT>) items[0]).Value;
			}

			return null;
		}

		private static ActionResult HandleChangeAggressive(
			Language language,
			ConsoleUi ui,
			Game game,
			World world,
			IHero hero)
		{
			return hero.Fighter.ChangeAggressive(!hero.Fighter.IsAgressive);
		}

		private static ActionResult HandleDropItem(
			Language language,
			ConsoleUi ui,
			Game game,
			World world,
			IHero hero)
		{
			var itemsToDrop = hero.Inventory.Items.Select(i => new ListItem<IItem>(i, i.GetDescription(language, hero)));

			ListItem selectedItemToDrop;
			if (ui.TrySelectItem(game, language.Promts.SelectItemToDrop, itemsToDrop, out selectedItemToDrop))
			{
				return hero.DropItem((IItem) selectedItemToDrop.ValueObject);
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
			IHero hero)
		{
			var weapons = new List<IItem>(hero.Inventory.Items.Where(i => i.Is<Weapon>())).ToList();
			if (hero.Fighter.WeaponToFight.GetType() != typeof(Unarmed))
			{
				weapons.Remove(hero.Fighter.WeaponToFight);
				weapons.Add(new Unarmed(hero));
			}

			IItem selectedWeapon;
			ListItem selectedWeaponItem;
			if (ui.TrySelectItem(game, language.Promts.SelectWeapon, weapons.Select(w => new ListItem<IItem>(w, w.GetDescription(language, hero))), out selectedWeaponItem))
			{
				selectedWeapon = ((ListItem<IItem>) selectedWeaponItem).Value;
			}
			else
			{
				selectedWeapon = hero.Fighter.WeaponToFight;
			}

			return hero.Fighter.ChangeWeapon(selectedWeapon);
		}

		private static ActionResult HandleChat(
			Language language,
			ConsoleUi ui,
			Game game,
			World world,
			IHero hero)
		{
			var humanoid = SelectTarget<IHumanoid>(ui, game, hero, language.SelectDirectionsPromt);
			if (humanoid != null)
			{
				return game.UserInterface.BeginChat(game, humanoid);
			}
			else
			{
				return null;
			}
		}

		private static ActionResult HandleTrade(
			Language language,
			ConsoleUi ui,
			Game game,
			World world,
			IHero hero)
		{
			var humanoid = SelectTarget<IHumanoid>(ui, game, hero, language.SelectDirectionsPromt);
			if (humanoid != null)
			{
				return game.UserInterface.BeginTrade(game, humanoid);
			}
			else
			{
				return null;
			}
		}

		private static ActionResult HandlePickpocket(
			Language language,
			ConsoleUi ui,
			Game game,
			World world,
			IHero hero)
		{
			var humanoid = SelectTarget<IHumanoid>(ui, game, hero, language.SelectDirectionsPromt);
			if (humanoid != null)
			{
				return game.UserInterface.BeginPickpocket(game, humanoid);
			}
			else
			{
				return null;
			}
		}

		private static ActionResult HandleBackstab(
			Language language,
			ConsoleUi ui,
			Game game,
			World world,
			IHero hero)
		{
			var alive = SelectTarget<IHumanoid>(ui, game, hero, language.SelectDirectionsPromt);
			if (alive != null)
			{
				return alive.Fighter.Backstab(hero);
			}
			else
			{
				return null;
			}
		}

		private static ActionResult HandlePick(
			Language language,
			ConsoleUi ui,
			Game game,
			World world,
			IHero hero)
		{
			IItem itemToPick = null;

			var itemsContainer = hero.CurrentCell.Objects.OfType<IItemsContainer>().FirstOrDefault();
			if (itemsContainer != null)
			{
				if (itemsContainer.Items.Count > 1)
				{
					var items = new List<ListItem>();
					foreach (var item in itemsContainer.Items)
					{
						items.Add(new ListItem<IItem>(item, item.ToString()));
					}

					ListItem selectedItem;
					if (ui.TrySelectItem(game, language.Promts.SelectItemToPick, items, out selectedItem))
					{
						itemToPick = ((ListItem<IItem>) selectedItem).Value;
					}
				}
				else //if (itemsContainer.Items.Count == 1)
				{
					itemToPick = itemsContainer.Items.First();
				}
			}

			if (itemToPick != null)
			{
				itemsContainer.PickItem(itemToPick, hero.Inventory.Items);
				return new ActionResult(
					Time.FromTicks(game.Balance.Time, game.Balance.ActionLongevity.PickItem),
					string.Format(CultureInfo.InvariantCulture, language.LogActionFormats.PickItem, hero.GetDescription(language, hero), itemToPick));

			}
			else
			{
				return null;
			}
		}

		private static ActionResult HandleOpenClose(
			Language language,
			ConsoleUi ui,
			Game game,
			World world,
			IHero hero)
		{
			var door = SelectTarget<IDoor>(ui, game, hero, language.SelectDirectionsPromt);
			if (door != null)
			{
				if (door.IsOpened)
				{
					door.Close();
				}
				else
				{
					door.Open();
				}
				return new ActionResult(
					Time.FromTicks(game.Balance.Time, game.Balance.ActionLongevity.OpenCloseDoor),
					string.Format(CultureInfo.InvariantCulture, language.LogActionFormats.OpenDoor, hero.GetDescription(language, hero), door.GetPosition()));
			}
			else
			{
				return null;
			}
		}

		private static ActionResult HandleReadBook(
			Language language,
			ConsoleUi ui,
			Game game,
			World world,
			IHero hero)
		{
			Paper selectedBook = null;

			var booksToRead = hero.Inventory.Items.OfType<Paper>().Select(i => new ListItem<Paper>(i, i.GetTitle(language.Books))).ToList();
			if (booksToRead.Count > 0)
			{
				ListItem selectedBookToRead;
				if (ui.TrySelectItem(game, language.Promts.SelectItemToRead, booksToRead, out selectedBookToRead))
				{
					selectedBook = ((ListItem<Paper>) selectedBookToRead).Value;
				}
			}

			if (selectedBook != null)
			{
				ui.ShowMessage(selectedBook.GetTitle(language.Books), new StringBuilder(selectedBook.GetText(language.Books)));

				return new ActionResult(
					Time.FromTicks(game.Balance.Time, game.Balance.ActionLongevity.ReadBook),
					string.Format(CultureInfo.InvariantCulture, language.LogActionFormats.ReadBook, hero.GetDescription(language, hero)));
			}
			else
			{
				return null;
			}
		}

		private static ActionResult HandleRide(
			Language language,
			ConsoleUi ui,
			Game game,
			World world,
			IHero hero)
		{
			if (hero.Rider.Transport == null)
			{
				var transport = SelectTarget<IObject>(ui, game, hero, language.SelectDirectionsPromt, h => h.Is<Transport>() && h.GetAspect<Transport>().Rider == null);
				return transport != null ? hero.Ride(transport) : null;
			}
			else
			{
				return hero.Unride();
			}

			return null;
		}

		private static ActionResult HandleShoot(
			Language language,
			ConsoleUi ui,
			Game game,
			World world,
			IHero hero)
		{
			Cell target;
			return	hero.Fighter.IsAgressive &&
					hero.Fighter.WeaponToFight.GetAspect<Weapon>().IsRange &&
					hero.Inventory.Items.Select<IItem, Missile>().Any() &&
					(target = ui.SelectShootingTarget(game)) != null
				? hero.Fighter.Shoot(target)
				: null;
		}

		private static ActionResult HandleEat(
			Language language,
			ConsoleUi ui,
			Game game,
			World world,
			IHero hero)
		{
			var itemsToEat = hero.Inventory.Items.Where(i => i.Type == ItemType.Food).Select(i => new ListItem<IItem>(i, i.GetDescription(language, hero)));

			ListItem selectedItemToEat;
			if (ui.TrySelectItem(game, language.Promts.SelectItemToEat, itemsToEat, out selectedItemToEat))
			{
				return hero.Eat(((ListItem<IItem>) selectedItemToEat).Value);
			}
			else
			{
				return null;
			}
		}

		private static ActionResult HandleDrink(
			Language language,
			ConsoleUi ui,
			Game game,
			World world,
			IHero hero)
		{
			var source = hero.CurrentCell.Objects.FirstOrDefault(o => o.Is<WaterSource>())?.GetAspect<WaterSource>();
			if (source != null && ui.AskForYesNoCancel(language.Promts.DrinkFromSource, game) == true)
			{
				return source.Drink(hero);
			}

			var itemsToDrink = hero.Inventory.Items.Where(i => i.Type == ItemType.Potion).Select(i => new ListItem<IItem>(i, i.GetDescription(language, hero)));
			ListItem selectedItemToDrink;
			if (ui.TrySelectItem(game, language.Promts.SelectItemToDrink, itemsToDrink, out selectedItemToDrink))
			{
				return hero.Drink(((ListItem<IItem>) selectedItemToDrink).Value);
			}

			return null;
		}
	}
}
