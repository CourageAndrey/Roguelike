using System.Drawing;

namespace Roguelike.Core.Mechanics.Materials
{
	public class FabricMaterials
	{
		public readonly Material Cotton = new Material(language => language.Fabrics.Cotton, Color.White, Material.Fabric);
		public readonly Material Wool = new Material(language => language.Fabrics.Wool, Color.Beige, Material.Fabric);
		public readonly Material Silk = new Material(language => language.Fabrics.Silk, Color.Ivory, Material.Fabric).MakeWaterProof();
		public readonly Material Linen = new Material(language => language.Fabrics.Linen, Color.FloralWhite, Material.Fabric);
		public readonly Material Canvas = new Material(language => language.Fabrics.Canvas, Color.Bisque, Material.Fabric);
		public readonly Material Velvet = new Material(language => language.Fabrics.Velvet, Color.DarkMagenta, Material.Fabric);
		public readonly Material Satin = new Material(language => language.Fabrics.Satin, Color.LavenderBlush, Material.Fabric);
		public readonly Material Burlap = new Material(language => language.Fabrics.Burlap, Color.BurlyWood, Material.Fabric);
		public readonly Material Denim = new Material(language => language.Fabrics.Denim, Color.RoyalBlue, Material.Fabric);

		public readonly ICollection<Material> All;

		internal FabricMaterials()
		{
			All = new List<Material>
			{
				Cotton,
				Wool,
				Silk,
				Linen,
				Canvas,
				Velvet,
				Satin,
				Burlap,
				Denim,
			};
		}
	}
}
