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

				KeyHandlerDelegate keyHandlerDelegate;
				KeyHandler keyHandler;
				var performedAction = _keyHandlers.TryGetValue(key.Key, out keyHandler) && (keyHandlerDelegate = keyHandler.GetHandler(key)) != null
					? keyHandlerDelegate(language, ui, game, world, hero)
					: null;

				if (performedAction != null)
				{
					var longevity = performedAction.Longevity.Scale(hero.Speed);
					world.ApplyAction(hero, new ActionResult(longevity, performedAction.LogMessages));
					world.DoOneStep();
					hero.State.PassTime(longevity, language.DeathReasons);
				}
			} while (true);
		}

		private static readonly IDictionary<ConsoleKey, KeyHandler> _keyHandlers = new Dictionary<ConsoleKey, KeyHandler>
		{
			{ ConsoleKey.F1, new KeyHandler(HandleShowHelp) },
			{ ConsoleKey.F2, new KeyHandler(HandleShowCharacter) },
			{ ConsoleKey.F3, new KeyHandler(HandleShowEquipment) },
			{ ConsoleKey.F4, new KeyHandler(HandleShowInventory) },

			{ ConsoleKey.LeftArrow, new KeyHandler(HandleMoveLeft) },
			{ ConsoleKey.NumPad4, new KeyHandler(HandleMoveLeft) },
			{ ConsoleKey.RightArrow, new KeyHandler(HandleMoveRight) },
			{ ConsoleKey.NumPad6, new KeyHandler(HandleMoveRight) },
			{ ConsoleKey.UpArrow, new KeyHandler(HandleMoveUp) },
			{ ConsoleKey.NumPad8, new KeyHandler(HandleMoveUp) },
			{ ConsoleKey.DownArrow, new KeyHandler(HandleMoveDown) },
			{ ConsoleKey.NumPad2, new KeyHandler(HandleMoveDown) },
			{ ConsoleKey.NumPad1, new KeyHandler(HandleMoveDownLeft) },
			{ ConsoleKey.NumPad3, new KeyHandler(HandleMoveDownRight) },
			{ ConsoleKey.NumPad7, new KeyHandler(HandleMoveUpLeft) },
			{ ConsoleKey.NumPad9, new KeyHandler(HandleMoveUpRight) },
			{ ConsoleKey.NumPad5, new KeyHandler(HandleMoveNone) },
			{ ConsoleKey.Spacebar, new KeyHandler(HandleMoveNone) },

			{ ConsoleKey.H, new KeyHandler(HandleInteract) },
			{ ConsoleKey.E, new KeyHandler(HandleEat) },
			{ ConsoleKey.F, new KeyHandler(HandleChangeAggressive) },
			{ ConsoleKey.D, KeyHandler.Shift(HandleDropItem, HandleDrink) },
			{ ConsoleKey.W, new KeyHandler(HandleSelectWeapon) },
			{ ConsoleKey.C, new KeyHandler(HandleChat) },
			{ ConsoleKey.T, KeyHandler.Shift(HandleShoot, HandleTrade) },
			{ ConsoleKey.P, new KeyHandler(HandlePickpocket) },
			{ ConsoleKey.B, new KeyHandler(HandleBackstab) },
			{ ConsoleKey.R, new KeyHandler(HandleReadBook) },
			{ ConsoleKey.OemComma, new KeyHandler(HandlePick) },
			{ ConsoleKey.O, new KeyHandler(HandleOpenClose) },
			{ ConsoleKey.S, new KeyHandler(HandleRide) },
		};
	}
}
