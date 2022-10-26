using Roguelike.Core;
using Roguelike.Core.ActiveObjects;
using Roguelike.Core.Localization;

namespace Roguelike.Console
{
	partial class Program
	{
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
	}
}
