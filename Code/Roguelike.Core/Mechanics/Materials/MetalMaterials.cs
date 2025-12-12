using System.Drawing;

namespace Roguelike.Core.Mechanics.Materials
{
	public class MetalMaterials
	{
		public readonly Material Bronze = new Material(language => language.Metals.Bronze, Color.DarkOrange, Material.Metal);
		public readonly Material Iron = new Material(language => language.Metals.Iron, Color.Blue, Material.Metal);
		public readonly Material Steel = new Material(language => language.Metals.Steel, Color.DeepSkyBlue, Material.Metal);
		public readonly Material Mithril = new Material(language => language.Metals.Mithril, Color.LightGray, Material.Metal).MakeRustproof();
		public readonly Material Adamantine = new Material(language => language.Metals.Adamantine, Color.DarkGray, Material.Metal).MakeRustproof();
		public readonly Material Ethereum = new Material(language => language.Metals.Ethereum, Color.Cyan, Material.Metal).MakeRustproof();
		public readonly Material Copper = new Material(language => language.Metals.Copper, Color.Orange, Material.Metal);
		public readonly Material Silver = new Material(language => language.Metals.Silver, Color.Silver, Material.Metal).MakeRustproof();
		public readonly Material Gold = new Material(language => language.Metals.Gold, Color.Gold, Material.Metal).MakeRustproof();
		public readonly Material Platinum = new Material(language => language.Metals.Platinum, Color.WhiteSmoke, Material.Metal).MakeRustproof();

		public readonly ICollection<Material> All;

		internal MetalMaterials()
		{
			All = new List<Material>
			{
				Copper,
				Bronze,
				Iron,
				Steel,
				Mithril,
				Adamantine,
				Ethereum,
				Silver,
				Gold,
				Platinum,
			};
		}
	}
}
