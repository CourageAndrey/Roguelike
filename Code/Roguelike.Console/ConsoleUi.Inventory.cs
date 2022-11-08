using System;

using Roguelike.Core.Interfaces;
using Roguelike.Core.Localization;

namespace Roguelike.Console
{
	partial class ConsoleUi
	{
		private const string _emptySlot = "-";

		private class EquipmentSlot
		{
			public char Letter
			{ get; }

			public string SlotName
			{ get; }

			public IWear Wear
			{ get; }

			private readonly LanguageItems _languageItems;

			public EquipmentSlot(char letter, Func<LanguageManequin, string> getSlotName, IWear wear, Language language)
			{
				_languageItems = language.Items;

				Letter = letter;
				SlotName = getSlotName(language.Character.Manequin);
				Wear = wear;
			}

			public void Display(IAlive forWhom)
			{
				System.Console.ForegroundColor = ConsoleColor.Yellow;
				System.Console.Write($"[{Letter}]");

				System.Console.ForegroundColor = ConsoleColor.White;
				System.Console.Write($" {SlotName} : ");

				if (Wear is Core.Items.Naked)
				{
					System.Console.WriteLine(_emptySlot);
				}
				else
				{
					System.Console.ForegroundColor = Wear.Material.Color.ToConsole();
					System.Console.WriteLine($" {Wear.GetDescription(_languageItems, forWhom)}");
					System.Console.ForegroundColor = ConsoleColor.White;
				}
			}
		}
	}
}