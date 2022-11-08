using System;

using Roguelike.Core;
using Roguelike.Core.Interfaces;

namespace Roguelike.Console
{
	partial class ConsoleUi
	{
		private const string _emptySlot = "-";

		private void DisplayItemLine(char letter, string slotName, IWear wear, Game game)
		{
			var itemsLanguage = game.Language.Items;

			System.Console.ForegroundColor = ConsoleColor.Yellow;
			System.Console.Write($"[{letter}]");

			System.Console.ForegroundColor = ConsoleColor.White;
			System.Console.Write($" {slotName} : ");

			if (wear is Core.Items.Naked)
			{
				System.Console.WriteLine(_emptySlot);
			}
			else
			{
				System.Console.ForegroundColor = wear.Material.Color.ToConsole();
				System.Console.WriteLine($" {wear.GetDescription(itemsLanguage, game.Hero)}");
				System.Console.ForegroundColor = ConsoleColor.White;
			}
		}
	}
}