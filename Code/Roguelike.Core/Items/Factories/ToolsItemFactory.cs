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

		public IItem CreatePickaxe()
		{
			return new Item(
				(language, alive) => language.Items.Tools.Pickaxe,
				() => 2.5m,
				ItemType.Tool,
				Material.Metal.Color,
				Material.Metal
			);
		}

		public IItem CreateHammer()
		{
			return new Item(
				(language, alive) => language.Items.Tools.Hammer,
				() => 1.5m,
				ItemType.Tool,
				Material.Metal.Color,
				Material.Metal
			);
		}

		public IItem CreateShovel()
		{
			return new Item(
				(language, alive) => language.Items.Tools.Shovel,
				() => 2m,
				ItemType.Tool,
				Material.Metal.Color,
				Material.Metal
			);
		}

		public IItem CreateTorch()
		{
			return new Item(
				(language, alive) => language.Items.Tools.Torch,
				() => 0.3m,
				ItemType.Tool,
				Color.Orange,
				Material.Wood
			);
		}

		public IItem CreateRope()
		{
			return new Item(
				(language, alive) => language.Items.Tools.Rope,
				() => 0.5m,
				ItemType.Tool,
				Color.Brown,
				Material.Fabric
			);
		}
	}
}
