using System;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Xml;
using System.Xml.Serialization;

using Roguelike.Core;
using Roguelike.Core.Localization;
using Roguelike.Core.Objects;

namespace Roguelike.Console
{
	partial class Program
	{
		private static XmlSerializer _serializer;

		private static Game ShowMainMenu(ConsoleUi ui, Language language)
		{
			MenuPoint helpMenuPoint;
			var menuPoints = new[]
			{
				new MenuPoint("N", language.Ui.MainScreen.NewGame, () => CreateNewGame(ui, language)),
				new MenuPoint("L", language.Ui.MainScreen.LoadGame, () => LoadGame(ui, language)),
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
				System.Console.ForegroundColor = ConsoleUi.HighlightForegroundColor;
				System.Console.Write($"[{LetterString}] ");

				System.Console.ForegroundColor = ConsoleUi.DefaultForegroundColor;
				System.Console.WriteLine(Description);
			}

			public Game GetGame()
			{
				return _getGame();
			}
		}

		private static Game CreateNewGame(ConsoleUi ui, Language language)
		{
			Race race;
			var heroStartSettings = new HeroStartSettings
			{
				Race = race = SelectRace(ui, language),
				SexIsMale = InputSex(ui, language),
				Age = ui.ReadNumber(language.Ui.CreateHero.InputAge, 25),
				Name = ui.ReadString(language.Ui.CreateHero.InputName, "Andor Drakon"),
				Profession = SelectProfession(ui, language),
				HairColor = SelectHairColor(race, ui, language),
				Haircut = SelectHaircut(race, ui, language),
			};
			return new Game(ui, language, heroStartSettings);
		}

		private static Game LoadGame(ConsoleUi ui, Language language)
		{
			string appPath = AppDomain.CurrentDomain.BaseDirectory;
			var savesFolder = new DirectoryInfo(Path.Combine(appPath, "Saves"));
			if (!savesFolder.Exists)
			{
				savesFolder.Create();
			}

			ui.Clear(true);
			var saveFiles = savesFolder.GetFiles("*.sav");
			if (saveFiles.Length > 0)
			{
				System.Console.WriteLine();
				for (int f = 0; f < saveFiles.Length; f++)
				{
					System.Console.WriteLine($"{f+1}. {saveFiles[f].Name}");
				}
				System.Console.WriteLine();
				uint saveNumber = ui.ReadNumber(language.Ui.LoadSave.SelectPromt, 0, false);

				if (_serializer == null)
				{
					_serializer = new XmlSerializer(typeof(Save));
				}

				Save save;
				using (var xmlFile = new XmlTextReader(saveFiles[saveNumber].FullName))
				{
					save = (Save) _serializer.Deserialize(xmlFile);
				}

				return Game.Load(save, ui, language);
			}
			else
			{
				System.Console.WriteLine(language.Ui.LoadSave.NoSaves);
				Thread.Sleep(500);
				return CreateNewGame(ui, language);
			}
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

		private static Color SelectHairColor(Race race, ConsoleUi ui, Language language)
		{
			var items = race.HairColors.Select(hc => new ListItem<Color>(hc, hc.Name));
			ListItem selected;
			if (!ui.TrySelectItem(language.Ui.CreateHero.SelectHairColor, items, out selected))
			{
				selected = items.First();
			}

			return ((ListItem<Color>) selected).Value;
		}

		private static Haircut SelectHaircut(Race race, ConsoleUi ui, Language language)
		{
			var items = Haircut.All.Select(hc => new ListItem<Haircut>(hc, hc.GetName(language.Character.Haircuts)));
			ListItem selected;
			if (!ui.TrySelectItem(language.Ui.CreateHero.SelectHaircut, items, out selected))
			{
				selected = items.First();
			}

			return ((ListItem<Haircut>) selected).Value;
		}
	}
}
