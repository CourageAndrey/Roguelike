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

		private abstract class EquipmentSlot
		{
			public char Letter { get; }

			public IWear Wear { get; }

			protected readonly LanguageItems LanguageItems;

			protected EquipmentSlot(char letter, IWear wear, Language language)
			{
				LanguageItems = language.Items;

				Letter = letter;
				Wear = wear;
			}

			public abstract void Display(IAlive forWhom);

			public static Dictionary<char, EquipmentSlot> Display(Language language, IAlive forWhom, IManequin manequin)
			{
				var menuLanguage = language.Character.Manequin;

				var result = new Dictionary<char, EquipmentSlot>
				{
					{ 'A', new RegularEquipmentSlot<IHeadWear>('A', l => l.HeadWear, manequin.HeadWear, language) },
					{ 'B', new RegularEquipmentSlot<IUpperBodyWear>('B', l => l.UpperBodyWear, manequin.UpperBodyWear, language) },
					{ 'C', new RegularEquipmentSlot<ILowerBodyWear>('C', l => l.LowerBodyWear, manequin.LowerBodyWear, language) },
					{ 'D', new RegularEquipmentSlot<ICoverWear>('D', l => l.CoverWear, manequin.CoverWear, language) },
					{ 'E', new RegularEquipmentSlot<IHandWear>('E', l => l.HandsWear, manequin.HandsWear, language) },
					{ 'F', new RegularEquipmentSlot<IFootWear>('F', l => l.FootsWear, manequin.FootsWear, language) },
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
						var jewelrySlot = new JewelryEquipmentSlot(letter++, jewelry, language);
						result[jewelrySlot.Letter] = jewelrySlot;
						jewelrySlot.Display(forWhom);
					}
					System.Console.ForegroundColor = ConsoleColor.White;
				}

				var addJewelry = new AddJewelryEquipmentSlot(letter, language);
				result[addJewelry.Letter] = addJewelry;
				addJewelry.Display(forWhom);

				return result;
			}

			public abstract IEnumerable<IItem> FilterSuitableItems(IEnumerable<IItem> items);
		}

		private class RegularEquipmentSlot<WearT> : EquipmentSlot
			where WearT : class, IWear
		{
			public string SlotName
			{ get; }

			public WearT ConcreteWear
			{ get { return Wear as WearT; } }

			public RegularEquipmentSlot(char letter, Func<LanguageManequin, string> getSlotName, WearT wear, Language language)
				: base(letter, wear, language)
			{
				SlotName = getSlotName(language.Character.Manequin);
			}

			public override void Display(IAlive forWhom)
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
					System.Console.WriteLine($" {Wear.GetDescription(LanguageItems, forWhom)}");
					System.Console.ForegroundColor = ConsoleColor.White;
				}
			}

			public override IEnumerable<IItem> FilterSuitableItems(IEnumerable<IItem> items)
			{
				return items.OfType<WearT>();
			}
		}

		private class JewelryEquipmentSlot : EquipmentSlot
		{
			public JewelryEquipmentSlot(char letter, IJewelry jewelry, Language language)
				: base(letter, jewelry, language)
			{ }

			public override void Display(IAlive forWhom)
			{
				System.Console.Write(" * ");

				System.Console.ForegroundColor = ConsoleColor.Yellow;
				System.Console.Write($"[{Letter}]");

				System.Console.ForegroundColor = ConsoleColor.Cyan;
				System.Console.WriteLine($" {Wear.GetDescription(LanguageItems, forWhom)}");
				System.Console.ForegroundColor = ConsoleColor.White;
			}

			public override IEnumerable<IItem> FilterSuitableItems(IEnumerable<IItem> items)
			{
				throw new NotSupportedException();
			}
		}

		private class AddJewelryEquipmentSlot : EquipmentSlot
		{
			public AddJewelryEquipmentSlot(char letter, Language language)
				: base(letter, null, language)
			{ }

			public override void Display(IAlive forWhom)
			{
				System.Console.Write(" + ");

				System.Console.ForegroundColor = ConsoleColor.Yellow;
				System.Console.Write($"[{Letter}]");
			}

			public override IEnumerable<IItem> FilterSuitableItems(IEnumerable<IItem> items)
			{
				return items.OfType<IJewelry>();
			}
		}
	}
}