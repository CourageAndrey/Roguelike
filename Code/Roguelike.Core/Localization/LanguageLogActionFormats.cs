﻿using System.Xml.Serialization;

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
				ChangeFightModeDisabled = "{0} didn't manage to become more or less agressive",
				ChangeWeapon = "{0} changed weapon from {1} to {2}",
				ChangeWeaponDisabled = "{0} didn't manage to change weapon",
				Attack = "{0} attacks {1} with {2}",
				Death = "{0} die: {1}",
				DropItem = "{0} picks {1}",
				PickItem = "{0} drops {1}",
				PickItemDisabled = "{0} didn't manage to pick item",
			};
		}
	}
}