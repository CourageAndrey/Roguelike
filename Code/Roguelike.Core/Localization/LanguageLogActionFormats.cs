using System.Xml.Serialization;

namespace Roguelike.Core.Localization
{
	[XmlType]
	public class LanguageLogActionFormats
	{
		#region Properties

		[XmlElement]
		public string OpenDoor
		{ get; set; }
		[XmlElement]
		public string CloseDoor
		{ get; set; }
		[XmlElement]
		public string Move
		{ get; set; }
		[XmlElement]
		public string MoveDisabled
		{ get; set; }
		[XmlElement]
		public string ChopTree
		{ get; set; }
		[XmlElement]
		public string StartFight
		{ get; set; }
		[XmlElement]
		public string StopFight
		{ get; set; }
		[XmlElement]
		public string ChangeFightModeDisabled
		{ get; set; }
		[XmlElement]
		public string ChangeWeapon
		{ get; set; }
		[XmlElement]
		public string ChangeWeaponDisabled
		{ get; set; }
		[XmlElement]
		public string Wait
		{ get; set; }
		[XmlElement]
		public string Attack
		{ get; set; }
		[XmlElement]
		public string Shoot
		{ get; set; }
		[XmlElement]
		public string Death
		{ get; set; }
		[XmlElement]
		public string DropItem
		{ get; set; }
		[XmlElement]
		public string PickItem
		{ get; set; }
		[XmlElement] 
		public string PickItemDisabled
		{ get; set; }
		[XmlElement]
		public string ReadBook
		{ get; set; }
		[XmlElement]
		public string RideHorse
		{ get; set; }
		[XmlElement]
		public string Eat
		{ get; set; }
		[XmlElement]
		public string Drink
		{ get; set; }
		[XmlElement]
		public string Vomit
		{ get; set; }
		[XmlElement]
		public string Dress
		{ get; set; }
		[XmlElement]
		public string Undress
		{ get; set; }
		[XmlElement]
		public string Backstab
		{ get; set; }

		#endregion

		public static LanguageLogActionFormats CreateDefault()
		{
			return new LanguageLogActionFormats
			{
				OpenDoor = "{0} opens the door at {1}.",
				CloseDoor = "{0} closes the door at {1}.",
				Move = "{0} moves from {1} to {2}.",
				MoveDisabled = "{0} attempts to move, but does not succeed.",
				ChopTree = "{0} chops the tree at {1}",
				StartFight = "{0} prepared to fight using {1}",
				StopFight = "{0} stopped to fight using {1}",
				Wait = "{0} waits",
				ChangeFightModeDisabled = "{0} didn't manage to become more or less aggressive",
				ChangeWeapon = "{0} changed weapon from {1} to {2}",
				ChangeWeaponDisabled = "{0} didn't manage to change weapon",
				Attack = "{0} attacks {1} with {2}",
				Shoot = "{0} shoots {1} with {2}",
				Death = "{0} die: {1}",
				DropItem = "{0} picks {1}",
				PickItem = "{0} drops {1}",
				PickItemDisabled = "{0} didn't manage to pick item",
				ReadBook = "{0} reads book",
				RideHorse = "{0} (un)rides horse",
				Eat = "{0} eats {1}",
				Drink = "{0} drinks {1}",
				Vomit = "{0} vomits",
				Dress = "{0} dresses {1}",
				Undress = "{0} undresses {1}",
				Backstab = "{0} backstabs {1} with {2}",
			};
		}
	}
}
