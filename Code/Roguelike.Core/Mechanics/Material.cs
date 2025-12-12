using System.Drawing;

using Roguelike.Core.Interfaces;
using Roguelike.Core.Localization;

namespace Roguelike.Core.Mechanics
{
	public class Material : IColorful
	{
		#region Properties

		private readonly Func<LanguageMaterials, string> _getName;

		public Color Color
		{ get; }

		public Material? Parent
		{ get; }

		public bool CanBurn
		{ get; private set; }

		public bool CanDrain
		{ get; private set; }

		public bool CanRust
		{ get; private set; }

		public bool CanConductElectricity
		{ get; private set; }

		public bool CanDrawn
		{ get; private set; }

		#endregion

		private Material(Func<LanguageMaterials, string> getName, Color color, Material? parent = null)
		{
			_getName = getName;

			Parent = parent;
			if (parent != null)
			{
				Color = parent.Color;

				CanBurn = parent.CanBurn;
				CanDrain = parent.CanDrain;
				CanRust = parent.CanRust;
				CanConductElectricity = parent.CanConductElectricity;
				CanDrawn = parent.CanDrawn;
			}

			Color = color;
		}

		public string GetName(LanguageMaterials language)
		{
			return _getName(language);
		}

		private Material MakeBurnable()
		{
			CanBurn = true;
			return this;
		}

		private Material MakeDrainable()
		{
			CanDrain = true;
			return this;
		}

		private Material MakeRustable()
		{
			CanRust = true;
			return this;
		}

		private Material MakeRustproof()
		{
			CanRust = false;
			return this;
		}

		private Material MakeConductingElectricity()
		{
			CanConductElectricity = true;
			return this;
		}

		private Material MakeDrawnable()
		{
			CanDrawn = true;
			return this;
		}

		private Material MakeWaterProof()
		{
			CanDrain = true;
			return this;
		}

		public Material Modify(Func<LanguageMaterials, string> getName, Color color)
		{
			var material = new Material(getName, color);
			if (CanBurn)
			{
				material.MakeBurnable();
			}
			if (CanDrain)
			{
				material.MakeDrainable();
			}
			if (CanRust)
			{
				material.MakeRustable();
			}
			if (CanConductElectricity)
			{
				material.MakeConductingElectricity();
			}
			if (CanDrawn)
			{
				material.MakeDrawnable();
			}
			return material;
		}

		#region List

		public static readonly Material Wood = new Material(language => language.Wood, Color.Brown).MakeBurnable();
		public static readonly Material Oak = new Material(language => language.Woods.Oak, Color.SaddleBrown, Wood);
		public static readonly Material Pine = new Material(language => language.Woods.Pine, Color.DarkGoldenrod, Wood);
		public static readonly Material Birch = new Material(language => language.Woods.Birch, Color.Beige, Wood);
		public static readonly Material Maple = new Material(language => language.Woods.Maple, Color.Peru, Wood);
		public static readonly Material Ash = new Material(language => language.Woods.Ash, Color.DarkSlateGray, Wood);
		public static readonly Material Cedar = new Material(language => language.Woods.Cedar, Color.Chocolate, Wood);
		public static readonly Material Willow = new Material(language => language.Woods.Willow, Color.Olive, Wood);
		public static readonly Material Yew = new Material(language => language.Woods.Yew, Color.DarkOliveGreen, Wood);
		public static readonly Material Ebony = new Material(language => language.Woods.Ebony, Color.Black, Wood);
		public static readonly Material Mahogany = new Material(language => language.Woods.Mahogany, Color.Maroon, Wood);

		public static readonly Material Metal = new Material(language => language.Metal, Color.Blue).MakeConductingElectricity().MakeDrawnable().MakeRustable();
		public static readonly Material Bronze = new Material(language => language.Metals.Bronze, Color.DarkOrange, Metal);
		public static readonly Material Iron = new Material(language => language.Metals.Iron, Color.Blue, Metal);
		public static readonly Material Steel = new Material(language => language.Metals.Steel, Color.DeepSkyBlue, Metal);
		public static readonly Material Mithril = new Material(language => language.Metals.Mithril, Color.LightGray, Metal).MakeRustproof();
		public static readonly Material Adamantine = new Material(language => language.Metals.Adamantine, Color.DarkGray, Metal).MakeRustproof();
		public static readonly Material Ethereum = new Material(language => language.Metals.Ethereum, Color.Cyan, Metal).MakeRustproof();
		public static readonly Material Copper = new Material(language => language.Metals.Copper, Color.Orange, Metal);
		public static readonly Material Silver = new Material(language => language.Metals.Silver, Color.Silver, Metal).MakeRustproof();
		public static readonly Material Gold = new Material(language => language.Metals.Gold, Color.Gold, Metal).MakeRustproof();
		public static readonly Material Platinum = new Material(language => language.Metals.Platinum, Color.WhiteSmoke, Metal).MakeRustproof();

		public static readonly Material Stone = new Material(language => language.Stone, Color.Gray).MakeDrawnable();
		public static readonly Material Granite = new Material(language => language.Stones.Granite, Color.DimGray, Stone);
		public static readonly Material Marble = new Material(language => language.Stones.Marble, Color.White, Stone);
		public static readonly Material Limestone = new Material(language => language.Stones.Limestone, Color.LightGray, Stone);
		public static readonly Material Sandstone = new Material(language => language.Stones.Sandstone, Color.SandyBrown, Stone);
		public static readonly Material Obsidian = new Material(language => language.Stones.Obsidian, Color.DarkSlateBlue, Stone);
		public static readonly Material Quartz = new Material(language => language.Stones.Quartz, Color.White, Stone);
		public static readonly Material Basalt = new Material(language => language.Stones.Basalt, Color.DarkSlateGray, Stone);
		public static readonly Material Slate = new Material(language => language.Stones.Slate, Color.DarkGray, Stone);
		public static readonly Material Flint = new Material(language => language.Stones.Flint, Color.Gray, Stone);
		public static readonly Material Jade = new Material(language => language.Stones.Jade, Color.MediumSeaGreen, Stone);

		public static readonly Material Skin = new Material(language => language.Skin, Color.Transparent).MakeBurnable();
		public static readonly Material Leather = new Material(language => language.Skins.Leather, Color.SaddleBrown, Skin);
		public static readonly Material Hide = new Material(language => language.Skins.Hide, Color.Tan, Skin);
		public static readonly Material Fur = new Material(language => language.Skins.Fur, Color.Brown, Skin);
		public static readonly Material Scale = new Material(language => language.Skins.Scale, Color.DarkSlateGray, Skin);
		public static readonly Material Dragonhide = new Material(language => language.Skins.Dragonhide, Color.DarkRed, Skin);

		public static readonly Material Fabric = new Material(language => language.Fabric, Color.Transparent).MakeBurnable().MakeDrainable();
		public static readonly Material Cotton = new Material(language => language.Fabrics.Cotton, Color.White, Fabric);
		public static readonly Material Wool = new Material(language => language.Fabrics.Wool, Color.Beige, Fabric);
		public static readonly Material Silk = new Material(language => language.Fabrics.Silk, Color.Ivory, Fabric).MakeWaterProof();
		public static readonly Material Linen = new Material(language => language.Fabrics.Linen, Color.FloralWhite, Fabric);
		public static readonly Material Canvas = new Material(language => language.Fabrics.Canvas, Color.Bisque, Fabric);
		public static readonly Material Velvet = new Material(language => language.Fabrics.Velvet, Color.DarkMagenta, Fabric);
		public static readonly Material Satin = new Material(language => language.Fabrics.Satin, Color.LavenderBlush, Fabric);
		public static readonly Material Burlap = new Material(language => language.Fabrics.Burlap, Color.BurlyWood, Fabric);
		public static readonly Material Denim = new Material(language => language.Fabrics.Denim, Color.RoyalBlue, Fabric);

		public static readonly Material Paper = new Material(language => language.Paper, Color.White).MakeBurnable().MakeDrainable();

		public static readonly Material Bone = new Material(language => language.Bone, Color.LightGray).MakeBurnable();

		public static readonly Material Food = new Material(language => language.Food, Color.LimeGreen).MakeBurnable();

		public static readonly Material Liquid = new Material(language => language.Liquid, Color.Blue).MakeConductingElectricity();

		public static readonly ICollection<Material> All = new List<Material>
		{
			Wood,
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

			Metal,
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

			Stone,
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

			Skin,
			Leather,
			Hide,
			Fur,
			Scale,
			Dragonhide,

			Fabric,
			Cotton,
			Wool,
			Silk,
			Linen,
			Canvas,
			Velvet,
			Satin,
			Burlap,
			Denim,

			Paper,

			Bone,

			Food,
			
			Liquid,
		};

		#endregion
	}
}
