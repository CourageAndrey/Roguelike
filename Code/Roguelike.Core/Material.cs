using System;
using System.Collections.Generic;
using System.Drawing;
using Roguelike.Core;
using Roguelike.Core.Interfaces;
using Roguelike.Core.Localization;

namespace Roguelike.Core
{
	public class Material : IColorful
	{
		#region Properties

		private readonly Func<LanguageMaterials, string> _getName;

		public Color Color
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

		private Material(Func<LanguageMaterials, string> getName, Color color)
		{
			_getName = getName;
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
		public static readonly Material Metal = new Material(language => language.Metal, Color.Blue).MakeConductingElectricity().MakeDrawnable().MakeRustable();
		public static readonly Material Stone = new Material(language => language.Stone, Color.Gray).MakeDrawnable();
		public static readonly Material Skin = new Material(language => language.Skin, Color.Transparent).MakeBurnable();
		public static readonly Material Fabric = new Material(language => language.Fabric, Color.Transparent).MakeBurnable().MakeDrainable();
		public static readonly Material Paper = new Material(language => language.Paper, Color.White).MakeBurnable().MakeDrainable();
		public static readonly Material Bone = new Material(language => language.Bone, Color.LightGray).MakeBurnable();
		public static readonly Material Food = new Material(language => language.Food, Color.LimeGreen).MakeBurnable();
		public static readonly Material Liquid = new Material(language => language.Liquid, Color.Blue).MakeConductingElectricity();

		public static readonly ICollection<Material> All = new List<Material>
		{
			Wood,
			Metal,
			Stone,
			Skin,
			Fabric,
			Paper,
			Bone,
			Food,
			Liquid,
		};

		#endregion
	}
}
