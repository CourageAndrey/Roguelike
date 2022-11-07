using System;
using System.Collections.Generic;
using System.Drawing;

using Roguelike.Core.Localization;

namespace Roguelike.Core
{
	public class Material
	{
		#region Properties

		private readonly Func<LanguageMaterials, string> _getName;

		public Color Color
		{ get; }

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

		#region List

		public static readonly Material Wood = new Material(language => language.Wood, Color.Brown);
		public static readonly Material Metal = new Material(language => language.Metal, Color.Blue);
		public static readonly Material Stone = new Material(language => language.Stone, Color.Gray);
		public static readonly Material Skin = new Material(language => language.Skin, Color.Transparent);
		public static readonly Material Fabric = new Material(language => language.Fabric, Color.Transparent);
		public static readonly Material Paper = new Material(language => language.Paper, Color.White);
		public static readonly Material Bone = new Material(language => language.Bone, Color.LightGray);
		public static readonly Material Food = new Material(language => language.Food, Color.LimeGreen);
		public static readonly Material Liquid = new Material(language => language.Liquid, Color.Blue);

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
