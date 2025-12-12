using System.Drawing;

namespace Roguelike.Core.Mechanics.Materials
{
	public class StoneMaterials
	{
		public readonly Material Granite = new Material(language => language.Stones.Granite, Color.DimGray, Material.Stone);
		public readonly Material Marble = new Material(language => language.Stones.Marble, Color.White, Material.Stone);
		public readonly Material Limestone = new Material(language => language.Stones.Limestone, Color.LightGray, Material.Stone);
		public readonly Material Sandstone = new Material(language => language.Stones.Sandstone, Color.SandyBrown, Material.Stone);
		public readonly Material Obsidian = new Material(language => language.Stones.Obsidian, Color.DarkSlateBlue, Material.Stone);
		public readonly Material Quartz = new Material(language => language.Stones.Quartz, Color.White, Material.Stone);
		public readonly Material Basalt = new Material(language => language.Stones.Basalt, Color.DarkSlateGray, Material.Stone);
		public readonly Material Slate = new Material(language => language.Stones.Slate, Color.DarkGray, Material.Stone);
		public readonly Material Flint = new Material(language => language.Stones.Flint, Color.Gray, Material.Stone);
		public readonly Material Jade = new Material(language => language.Stones.Jade, Color.MediumSeaGreen, Material.Stone);

		public readonly ICollection<Material> All;

		internal StoneMaterials()
		{
			All = new List<Material>
			{
				Granite,
				Marble,
				Limestone,
				Sandstone,
				Obsidian,
				Quartz,
				Basalt,
				Slate,
				Flint,
				Jade,
			};
		}
	}
}
