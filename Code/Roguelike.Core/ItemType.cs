using System;

using Roguelike.Core.Localization;

namespace Roguelike.Core
{
	public class ItemType
	{
		#region Properties

		private readonly Func<LanguageItemTypes, string> getName;

		#endregion

		private ItemType(Func<LanguageItemTypes, string> getName)
		{
			this.getName = getName;
		}

		public string GetName(LanguageItemTypes language)
		{
			return getName(language);
		}

		#region List

		public static readonly ItemType Weapon = new ItemType(language => language.Weapon);

		public static readonly ItemType Wear = new ItemType(language => language.Wear);

		public static readonly ItemType Food = new ItemType(language => language.Food);

		public static readonly ItemType Potion = new ItemType(language => language.Potion);

		public static readonly ItemType Tool = new ItemType(language => language.Tool);

		public static readonly ItemType Paper = new ItemType(language => language.Paper);

		#endregion
	}
}
