using System;

using Roguelike.Core.Localization;

namespace Roguelike.Core
{
	public class ItemType
	{
		#region Properties

		private readonly Func<Language, string> getName;

		#endregion

		private ItemType(Func<Language, string> getName)
		{
			this.getName = getName;
		}

		public string GetName(Language language)
		{
			return getName(language);
		}

		#region List

		public static readonly ItemType Weapon = new ItemType(language => language.ItemTypeWeapon);

		public static readonly ItemType Wear = new ItemType(language => language.ItemTypeWear);

		public static readonly ItemType Food = new ItemType(language => language.ItemTypeFood);

		public static readonly ItemType Tool = new ItemType(language => language.ItemTypeTool);

		#endregion
	}
}
