using Roguelike.Core.Localization;

namespace Roguelike.Core.Mechanics
{
	public class Element
	{
		#region Properties

		private readonly Func<LanguageElements, string> _getName;

		#endregion

		private Element(Func<LanguageElements, string> nameGetter)
		{
			_getName = nameGetter;
		}

		public string GetName(LanguageElements language)
		{
			return _getName(language);
		}

		#region List

		public static Element Air { get; } = new Element(l => l.Air);
		public static Element Water { get; } = new Element(l => l.Water);
		public static Element Ground { get; } = new Element(l => l.Ground);
		public static Element Fire { get; } = new Element(l => l.Fire);
		public static Element Frost { get; } = new Element(l => l.Frost);
		public static Element Metal { get; } = new Element(l => l.Metal);
		public static Element Poison { get; } = new Element(l => l.Poison);
		public static Element Electricity { get; } = new Element(l => l.Electricity);
		public static Element Acid { get; } = new Element(l => l.Acid);
		public static Element Soul { get; } = new Element(l => l.Soul);

		#endregion
	}
}
