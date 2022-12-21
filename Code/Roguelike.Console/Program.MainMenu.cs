using System;
using System.Linq;
using System.Text;

using Roguelike.Core;
using Roguelike.Core.Localization;
using Roguelike.Core.Objects;

namespace Roguelike.Console
{
	partial class Program
	{
		private static Game ShowMainMenu(ConsoleUi ui, Language language)
		{
			MenuPoint helpMenuPoint;
			var menuPoints = new[]
			{
				new MenuPoint("N", language.Ui.MainScreen.NewGame, () => CreateNewGame(ui, language)),
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

		private static Game CreateNewGame(ConsoleUi ui, Language language)
		{
			var heroStartSettings = new HeroStartSettings
			{
				Race = SelectRace(ui, language),
				SexIsMale = InputSex(ui, language),
				Age = InputAge(ui, language),
				Name = InputName(ui, language),
				Profession = SelectProfession(ui, language),
			};
			return new Game(ui, language, heroStartSettings);
		}

		private static Race SelectRace(ConsoleUi ui, Language language)
		{
			var items = Race.All.Select(r => new ListItem<Race>(r, r.GetName(language.Character.Races)));
			ListItem selected;
			if (!ui.TrySelectItem(language.Ui.CreateHero.SelectRace, items, out selected))
			{
				selected = items.First();
			}

			return ((ListItem<Race>) selected).Value;
		}

		private static bool InputSex(ConsoleUi ui, Language language)
		{
			ui.Clear(true);
			System.Console.WriteLine(language.Ui.CreateHero.InputSex);

			var key = System.Console.ReadKey().KeyChar;
			return key != 'f' && key != 'F';
		}

		private static uint InputAge(ConsoleUi ui, Language language)
		{
			ui.Clear(true);
			System.Console.WriteLine(language.Ui.CreateHero.InputAge);

			return uint.Parse(System.Console.ReadLine());
		}

		private static string InputName(ConsoleUi ui, Language language)
		{
			ui.Clear(true);
			System.Console.WriteLine(language.Ui.CreateHero.InputName);

			string name = System.Console.ReadLine();
			return !string.IsNullOrEmpty(name)
				? name
				: "Andor Drakon";
		}

		private static Profession SelectProfession(ConsoleUi ui, Language language)
		{
			var items = Profession.All.Select(p => new ListItem<Profession>(p, p.GetName(language.Character.Professions)));
			ListItem selected;
			if (!ui.TrySelectItem(language.Ui.CreateHero.SelectProfession, items, out selected))
			{
				selected = items.First();
			}

			return ((ListItem<Profession>) selected).Value;
		}
	}
}
