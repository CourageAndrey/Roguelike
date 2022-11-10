using System;
using System.Collections.Generic;
using System.Linq;

using Roguelike.Core.Interfaces;
using Roguelike.Core.Localization;

namespace Roguelike.Console
{
	partial class ConsoleUi
	{
		private const string _emptySlot = "-";
		private const char _firstJewelryLetter = 'G';

		private class EquipmentSlot
		{
			#region Properties

			public char Letter
			{ get; }

			public WearSlot Slot
			{ get; }

			public string SlotName
			{ get; }

			public IWear Wear
			{ get; }

			private readonly LanguageItems _languageItems;

			private static readonly IDictionary<WearSlot, Func<LanguageManequin, string>> _slotNames = new Dictionary<WearSlot, Func<LanguageManequin, string>>
			{
				{ WearSlot.Head, l => l.HeadWear },
				{ WearSlot.UpperBody, l => l.UpperBodyWear },
				{ WearSlot.LowerBody, l => l.LowerBodyWear },
				{ WearSlot.Cover, l => l.CoverWear },
				{ WearSlot.Hands, l => l.HandsWear },
				{ WearSlot.Foots, l => l.FootsWear },
				{ WearSlot.Jewelry, l => l.Jewelry },
			};

			#endregion

			private EquipmentSlot(char letter, WearSlot slot, IWear wear, Language language)
			{
				Slot = slot;
				SlotName = _slotNames[slot](language.Character.Manequin);
				Letter = letter;
				Wear = wear;
				_languageItems = language.Items;
			}

			public void Display(IAlive forWhom)
			{
				if (Wear == null)
				{
					DisplayAddJewelry();
				}
				else if (Slot == WearSlot.Jewelry)
				{
					DisplayJewelry(forWhom);
				}
				else
				{
					DisplayRegular(forWhom);
				}
			}

			private void DisplayRegular(IAlive forWhom)
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

			private void DisplayJewelry(IAlive forWhom)
			{
				System.Console.Write(" * ");

				System.Console.ForegroundColor = ConsoleColor.Yellow;
				System.Console.Write($"[{Letter}]");

				System.Console.ForegroundColor = ConsoleColor.Cyan;
				System.Console.WriteLine($" {Wear.GetDescription(_languageItems, forWhom)}");
				System.Console.ForegroundColor = ConsoleColor.White;
			}

			public void DisplayAddJewelry()
			{
				System.Console.Write(" + ");

				System.Console.ForegroundColor = ConsoleColor.Yellow;
				System.Console.Write($"[{Letter}]");
			}

			public static Dictionary<char, EquipmentSlot> Display(Language language, IAlive forWhom, IManequin manequin)
			{
				var menuLanguage = language.Character.Manequin;

				var result = new Dictionary<char, EquipmentSlot>
				{
					{ 'A', new EquipmentSlot('A', WearSlot.Head, manequin.HeadWear, language) },
					{ 'B', new EquipmentSlot('B', WearSlot.UpperBody, manequin.UpperBodyWear, language) },
					{ 'C', new EquipmentSlot('C', WearSlot.LowerBody, manequin.LowerBodyWear, language) },
					{ 'D', new EquipmentSlot('D', WearSlot.Cover, manequin.CoverWear, language) },
					{ 'E', new EquipmentSlot('E', WearSlot.Hands, manequin.HandsWear, language) },
					{ 'F', new EquipmentSlot('F', WearSlot.Foots, manequin.FootsWear, language) },
				};
				foreach (var slot in result.Values)
				{
					slot.Display(forWhom);
				}

				System.Console.WriteLine();
				System.Console.ForegroundColor = ConsoleColor.White;
				System.Console.WriteLine($" {menuLanguage.Jewelry} :");

				char letter = _firstJewelryLetter;
				if (manequin.Jewelry.Count > 0)
				{
					foreach (var jewelry in manequin.Jewelry)
					{
						var jewelrySlot = new EquipmentSlot(letter++, WearSlot.Jewelry, jewelry, language);
						result[jewelrySlot.Letter] = jewelrySlot;
						jewelrySlot.Display(forWhom);
					}
					System.Console.ForegroundColor = ConsoleColor.White;
				}

				var addJewelry = new EquipmentSlot(letter, WearSlot.Jewelry, null, language);
				result[addJewelry.Letter] = addJewelry;
				addJewelry.Display(forWhom);

				return result;
			}

			public IEnumerable<IItem> FilterSuitableItems(IEnumerable<IItem> items)
			{
				return items.OfType<IWear>().Where(i => i.SuitableSlot == Slot);
			}
		}
	}
}