using System.Xml.Serialization;

using Roguelike.Core.Localization.Items;

namespace Roguelike.Core.Localization
{
	[XmlType]
	public class LanguageItems
	{
		#region Properties

		[XmlElement]
		public LanguageItemTypes ItemTypes
		{ get; set; }

		[XmlElement]
		public LanguageClothes Clothes
		{ get; set; }

		[XmlElement]
		public LanguageTools Tools
		{ get; set; }

		[XmlElement]
		public LanguageFood Food
		{ get; set; }

		[XmlElement]
		public LanguagePotions Potions
		{ get; set; }

		[XmlElement]
		public LanguageMeleeWeapons MeleeWeapons
		{ get; set; }

		[XmlElement]
		public LanguageRangeWeapons RangeWeapons
		{ get; set; }

		[XmlElement]
		public LanguagePapers Papers
		{ get; set; }

		[XmlElement]
		public LanguageMissiles Missiles
		{ get; set; }

		[XmlElement]
		public LanguageJewelry Jewelry
		{ get; set; }

		[XmlElement]
		public LanguageArmor Armor
		{ get; set; }

		[XmlElement]
		public LanguageSpecial Special
		{ get; set; }

		#endregion

		public static LanguageItems CreateDefault()
		{
			return new LanguageItems
			{
				ItemTypes = LanguageItemTypes.CreateDefault(),
				Clothes = LanguageClothes.CreateDefault(),
				Tools = LanguageTools.CreateDefault(),
				Food = LanguageFood.CreateDefault(),
				Potions = LanguagePotions.CreateDefault(),
				MeleeWeapons = LanguageMeleeWeapons.CreateDefault(),
				RangeWeapons = LanguageRangeWeapons.CreateDefault(),
				Papers = LanguagePapers.CreateDefault(),
				Missiles = LanguageMissiles.CreateDefault(),
				Jewelry = LanguageJewelry.CreateDefault(),
				Armor = LanguageArmor.CreateDefault(),
				Special = LanguageSpecial.CreateDefault(),
			};
		}
	}
}
