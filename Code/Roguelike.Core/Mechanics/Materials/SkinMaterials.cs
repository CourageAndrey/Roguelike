using System.Drawing;

namespace Roguelike.Core.Mechanics.Materials
{
	public class SkinMaterials
	{
		public readonly Material Leather = new Material(language => language.Skins.Leather, Color.SaddleBrown, Material.Skin);
		public readonly Material Hide = new Material(language => language.Skins.Hide, Color.Tan, Material.Skin);
		public readonly Material Fur = new Material(language => language.Skins.Fur, Color.Brown, Material.Skin);
		public readonly Material Scale = new Material(language => language.Skins.Scale, Color.DarkSlateGray, Material.Skin);
		public readonly Material Dragonhide = new Material(language => language.Skins.Dragonhide, Color.DarkRed, Material.Skin).MakeFireProof();

		public readonly ICollection<Material> All;

		internal SkinMaterials()
		{
			All = new List<Material>
			{
				Leather,
				Hide,
				Fur,
				Scale,
				Dragonhide,
			};
		}
	}
}
