using System.Text;

using Roguelike.Core;
using Roguelike.Core.ActiveObjects;
using Roguelike.Core.Localization;

namespace Roguelike.Console
{
	partial class Program
	{
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

		private static ActionResult HandleShowEquipment(
			Language language,
			ConsoleUi ui,
			Game game,
			World world,
			Hero hero)
		{
			return ui.ShowEquipment(game, hero.Manequin);
		}

		private static ActionResult HandleShowInventory(
			Language language,
			ConsoleUi ui,
			Game game,
			World world,
			Hero hero)
		{
			ui.ShowInventory(game, hero);
			return null;
		}

		private static ActionResult HandleShowLog(
			Language language,
			ConsoleUi ui,
			Game game,
			World world,
			Hero hero)
		{
			ui.ShowMessage(string.Empty, new StringBuilder(game.Log));
			return null;
		}
	}
}
