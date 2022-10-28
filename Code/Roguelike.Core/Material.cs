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

		public static readonly ICollection<Material> All = new List<Material>
		{
			Wood,
			Metal,
			Stone,
		};

		#endregion
	}
}
