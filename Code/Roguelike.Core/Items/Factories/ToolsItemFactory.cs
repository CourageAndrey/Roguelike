using System.Drawing;

using Roguelike.Core.Interfaces;
using Roguelike.Core.Mechanics;

namespace Roguelike.Core.Items.Factories
{
	internal class ToolsItemFactory
	{
		public IItem CreateLog()
		{
			return new Item(
				(language, alive) => language.Items.Tools.Log,
				() => 0.5m,
				ItemType.Tool,
				Color.Brown,
				Material.Wood
			);
		}
	}
}
