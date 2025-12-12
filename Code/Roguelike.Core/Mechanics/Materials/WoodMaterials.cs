using System.Drawing;

namespace Roguelike.Core.Mechanics.Materials
{
	public class WoodMaterials
	{
		public readonly Material Oak = new Material(language => language.Woods.Oak, Color.SaddleBrown, Material.Wood);
		public readonly Material Pine = new Material(language => language.Woods.Pine, Color.DarkGoldenrod, Material.Wood);
		public readonly Material Birch = new Material(language => language.Woods.Birch, Color.Beige, Material.Wood);
		public readonly Material Maple = new Material(language => language.Woods.Maple, Color.Peru, Material.Wood);
		public readonly Material Ash = new Material(language => language.Woods.Ash, Color.DarkSlateGray, Material.Wood);
		public readonly Material Cedar = new Material(language => language.Woods.Cedar, Color.Chocolate, Material.Wood);
		public readonly Material Willow = new Material(language => language.Woods.Willow, Color.Olive, Material.Wood);
		public readonly Material Yew = new Material(language => language.Woods.Yew, Color.DarkOliveGreen, Material.Wood);
		public readonly Material Ebony = new Material(language => language.Woods.Ebony, Color.Black, Material.Wood);
		public readonly Material Mahogany = new Material(language => language.Woods.Mahogany, Color.Maroon, Material.Wood);

		public readonly ICollection<Material> All;

		internal WoodMaterials()
		{
			All = new List<Material>
			{
				Oak,
				Pine,
				Birch,
				Maple,
				Ash,
				Cedar,
				Willow,
				Yew,
				Ebony,
				Mahogany,
			};
		}
	}
}
