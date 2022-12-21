using System;
using System.Linq;
using System.Text;

using Roguelike.Core;
using Roguelike.Core.Localization;

namespace Roguelike.Console
{
	partial class Program
	{
		private static Game ShowMainMenu(ConsoleUi ui, Language language)
		{
			MenuPoint helpMenuPoint;
			var menuPoints = new[]
			{
				new MenuPoint("N", language.Ui.MainScreen.NewGame, () => new Game(ui, language)),
				new MenuPoint("L", language.Ui.MainScreen.LoadGame, () => { throw new NotImplementedException(); }),
				helpMenuPoint = new MenuPoint(language.Ui.AnyOtherKey, language.Ui.MainScreen.Help, null),
				new MenuPoint("E", language.Ui.MainScreen.Exit, () => null),
			}.ToDictionary(mp => mp.Letter, mp => mp);

			do
			{
				ui.Clear(true);

				foreach (var menuPoint in menuPoints.Values)
				{
					menuPoint.Draw();
				}
				var key = System.Console.ReadKey(true);
				var keyChar = key.KeyChar.ToString().ToUpperInvariant()[0];

				MenuPoint selectedMenuPoint;
				if (menuPoints.TryGetValue(keyChar, out selectedMenuPoint))
				{
					return selectedMenuPoint.GetGame();
				}
				else
				{
					ui.ShowMessage(language.HelpTitle, new StringBuilder(language.HelpText));
				}
			}
			while (true);
		}

		private class MenuPoint
		{
			#region Properties

			public char Letter
			{ get; }

			public string LetterString
			{ get; }

			public string Description
			{ get; }

			private readonly Func<Game> _getGame;

			#endregion

			public MenuPoint(string letter, string description, Func<Game> getGame)
			{
				Letter = letter.Length == 1 ? letter[0] : '\0';
				LetterString = letter;
				Description = description;
				_getGame = getGame;
			}

			public void Draw()
			{
				System.Console.ForegroundColor = ConsoleColor.Yellow;
				System.Console.Write($"[{LetterString}] ");

				System.Console.ForegroundColor = ConsoleColor.White;
				System.Console.WriteLine(Description);
			}

			public Game GetGame()
			{
				return _getGame();
			}
		}
	}
}
