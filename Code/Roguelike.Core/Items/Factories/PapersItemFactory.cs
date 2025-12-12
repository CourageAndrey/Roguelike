using System.Drawing;

using Roguelike.Core.Aspects;
using Roguelike.Core.Interfaces;
using Roguelike.Core.Localization;
using Roguelike.Core.Mechanics;

namespace Roguelike.Core.Items.Factories
{
	internal class PapersItemFactory
	{
		public IItem CreateBook(Color coverColor, Func<LanguageBooks, string> getTitle, Func<LanguageBooks, string> getText)
		{
			return new Item(
				(language, alive) => language.Items.Papers.Book,
				() => 1,
				ItemType.Paper,
				coverColor,
				Material.Paper,
				new Paper(getTitle, getText)
			);
		}
	}
}
