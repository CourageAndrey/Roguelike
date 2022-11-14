using System.Xml.Serialization;

namespace Roguelike.Core.Localization
{
	[XmlType]
	public class LanguageObjects
	{
		#region Properties

		[XmlElement]
		public string Pool
		{ get; set; }
		[XmlElement]
		public string Bed
		{ get; set; }
		[XmlElement]
		public string CorpseFormat
		{ get; set; }
		[XmlElement]
		public string Door
		{ get; set; }
		[XmlElement]
		public string Fire
		{ get; set; }
		[XmlElement]
		public string ItemsPile
		{ get; set; }
		[XmlElement]
		public string Stump
		{ get; set; }
		[XmlElement]
		public string Tree
		{ get; set; }
		[XmlElement]
		public string Wall
		{ get; set; }
		[XmlElement]
		public string Dog
		{ get; set; }
		[XmlElement]
		public string Horse
		{ get; set; }
		[XmlElement]
		public string HumanMale
		{ get; set; }
		[XmlElement]
		public string HumanFemale
		{ get; set; }

		#endregion

		public static LanguageObjects CreateDefault()
		{
			return new LanguageObjects
			{
				Pool = "Pool",
				Bed = "Bed",
				CorpseFormat = "Corpse of {0}",
				Door = "Door",
				Fire = "Fire",
				ItemsPile = "ItemsPile",
				Stump = "Stump",
				Tree = "Tree",
				Wall = "Wall",
				Dog = "Dog",
				Horse = "Horse",
				HumanMale = "Human male",
				HumanFemale = "Human female",
			};
		}
	}
}
