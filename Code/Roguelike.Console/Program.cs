using System;
using System.Collections.Generic;
using System.Text;

using Roguelike.Core;
using Roguelike.Core.ActiveObjects;
using Roguelike.Core.Localization;

namespace Roguelike.Console
{
	internal partial class Program
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
			{ ConsoleKey.Spacebar, HandleMoveNone },

			{ ConsoleKey.H, HandleInteract },
			{ ConsoleKey.F, HandleChangeAggressive },
			{ ConsoleKey.D, HandleDropItem },
			{ ConsoleKey.W, HandleSelectWeapon },
			{ ConsoleKey.C, HandleChat },
			{ ConsoleKey.T, HandleTrade },
			{ ConsoleKey.P, HandlePickpocket },
			{ ConsoleKey.B, HandleBackstab },
			{ ConsoleKey.R, HandleReadBook },
			{ ConsoleKey.OemComma, HandlePick },
			{ ConsoleKey.O, HandleOpenClose },
		};

		#endregion
	}
}
