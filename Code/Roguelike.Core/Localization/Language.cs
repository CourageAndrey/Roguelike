using System.Xml.Serialization;

namespace Roguelike.Core.Localization
{
	[XmlType]
	public class Language
	{
		#region Properties

		[XmlIgnore]
		public string Name
		{ get; set; }

		[XmlElement]
		public string SelectInteractionPromt
		{ get; set; }
		[XmlElement]
		public string SelectWeaponPromt
		{ get; set; }
		[XmlElement]
		public string SelectItemToDropPromt
		{ get; set; }
		[XmlElement]
		public string SelectItemToPickPromt
		{ get; set; }

		[XmlElement]
		public string InteractionFormat
		{ get; set; }

		[XmlElement]
		public string InteractionOpenDoor
		{ get; set; }
		[XmlElement]
		public string InteractionCloseDoor
		{ get; set; }
		[XmlElement]
		public string InteractionChopTree
		{ get; set; }
		[XmlElement]
		public string InteractionBackstab
		{ get; set; }
		[XmlElement]
		public string InteractionChat
		{ get; set; }
		[XmlElement]
		public string InteractionTrade
		{ get; set; }
		[XmlElement]
		public string InteractionPickpocket
		{ get; set; }
		[XmlElement]
		public string InteractionPickItem
		{ get; set; }

		[XmlElement]
		public string LogActionFormatOpenDoor
		{ get; set; }
		[XmlElement]
		public string LogActionFormatCloseDoor
		{ get; set; }
		[XmlElement]
		public string LogActionFormatMove
		{ get; set; }
		[XmlElement]
		public string LogActionFormatMoveDisabled
		{ get; set; }
		[XmlElement]
		public string LogActionFormatChopTree
		{ get; set; }
		[XmlElement]
		public string LogActionFormatStartFight
		{ get; set; }
		[XmlElement]
		public string LogActionFormatStopFight
		{ get; set; }
		[XmlElement]
		public string LogActionFormatChangeFightModeDisabled
		{ get; set; }
		[XmlElement]
		public string LogActionFormatChangeWeapon
		{ get; set; }
		[XmlElement]
		public string LogActionFormatChangeWeaponDisabled
		{ get; set; }
		[XmlElement]
		public string LogActionFormatWait
		{ get; set; }
		[XmlElement]
		public string LogActionFormatAttack
		{ get; set; }
		[XmlElement]
		public string LogActionFormatDeath
		{ get; set; }
		[XmlElement]
		public string LogActionFormatDropItem
		{ get; set; }
		[XmlElement]
		public string LogActionFormatPickItem
		{ get; set; }
		[XmlElement]
		public string LogActionFormatPickItemDisabled
		{ get; set; }

		[XmlElement]
		public string ReathReasonKilled
		{ get; set; }

		[XmlElement]
		public string UiCommonOk
		{ get; set; }
		[XmlElement]
		public string UiCommonCancel
		{ get; set; }

		[XmlElement]
		public string UiMainNewGame
		{ get; set; }
		[XmlElement]
		public string UiMainLoadGame
		{ get; set; }
		[XmlElement]
		public string UiMainHelp
		{ get; set; }
		[XmlElement]
		public string UiMainExit
		{ get; set; }
		[XmlElement]
		public string UiMainSelectSave
		{ get; set; }
		[XmlElement]
		public string UiMainSavesFilter
		{ get; set; }

		[XmlElement]
		public string UiCharacterGeneral
		{ get; set; }
		[XmlElement]
		public string UiCharacterBody
		{ get; set; }
		[XmlElement]
		public string UiCharacterEffects
		{ get; set; }
		[XmlElement]
		public string UiCharacterStats
		{ get; set; }
		[XmlElement]
		public string UiCharacterSkills
		{ get; set; }
		[XmlElement]
		public string UiCharacterWearedItems
		{ get; set; }
		[XmlElement]
		public string UiCharacterInventory
		{ get; set; }

		[XmlElement]
		public string HelpTitle
		{ get; set; }
		[XmlElement]
		public string HelpText
		{ get; set; }

		[XmlElement]
		public string DirectionNone
		{ get; set; }
		[XmlElement]
		public string DirectionLeft
		{ get; set; }
		[XmlElement]
		public string DirectionRight
		{ get; set; }
		[XmlElement]
		public string DirectionUp
		{ get; set; }
		[XmlElement]
		public string DirectionDown
		{ get; set; }
		[XmlElement]
		public string DirectionUpLeft
		{ get; set; }
		[XmlElement]
		public string DirectionUpRight
		{ get; set; }
		[XmlElement]
		public string DirectionDownLeft
		{ get; set; }
		[XmlElement]
		public string DirectionDownRight
		{ get; set; }

		[XmlElement]
		public string BodyPartArm
		{ get; set; }
		[XmlElement]
		public string BodyPartLeg
		{ get; set; }
		[XmlElement]
		public string BodyPartTail
		{ get; set; }
		[XmlElement]
		public string BodyPartWing
		{ get; set; }
		[XmlElement]
		public string BodyPartBody
		{ get; set; }
		[XmlElement]
		public string BodyPartTentacle
		{ get; set; }
		[XmlElement]
		public string BodyPartHead
		{ get; set; }
		[XmlElement]
		public string BodyPartFace
		{ get; set; }
		[XmlElement]
		public string BodyPartEye
		{ get; set; }
		[XmlElement]
		public string BodyPartEar
		{ get; set; }
		[XmlElement]
		public string BodyPartNose
		{ get; set; }
		[XmlElement]
		public string BodyPartHoove
		{ get; set; }
		[XmlElement]
		public string BodyPartHorn
		{ get; set; }
		[XmlElement]
		public string BodyPartFinger
		{ get; set; }
		[XmlElement]
		public string BodyPartWrist
		{ get; set; }
		[XmlElement]
		public string BodyPartFoot
		{ get; set; }
		[XmlElement]
		public string BodyPartShoulder
		{ get; set; }
		[XmlElement]
		public string BodyPartElbow
		{ get; set; }
		[XmlElement]
		public string BodyPartForearm
		{ get; set; }
		[XmlElement]
		public string BodyPartHaunch
		{ get; set; }
		[XmlElement]
		public string BodyPartKnee
		{ get; set; }
		[XmlElement]
		public string BodyPartShin
		{ get; set; }
		[XmlElement]
		public string BodyPartRibs
		{ get; set; }
		[XmlElement]
		public string BodyPartHeart
		{ get; set; }
		[XmlElement]
		public string BodyPartLung
		{ get; set; }
		[XmlElement]
		public string BodyPartLiver
		{ get; set; }
		[XmlElement]
		public string BodyPartStomach
		{ get; set; }
		[XmlElement]
		public string BodyPartKidney
		{ get; set; }
		[XmlElement]
		public string BodyPartSkull
		{ get; set; }
		[XmlElement]
		public string BodyPartBrain
		{ get; set; }
		[XmlElement]
		public string BodyPartHairs
		{ get; set; }
		[XmlElement]
		public string BodyPartMouth
		{ get; set; }
		[XmlElement]
		public string BodyPartTongue
		{ get; set; }
		[XmlElement]
		public string BodyPartThroat
		{ get; set; }
		[XmlElement]
		public string BodyPartTooth
		{ get; set; }
		[XmlElement]
		public string BodyPartSkin
		{ get; set; }

		[XmlElement]
		public string GameWin
		{ get; set; }
		[XmlElement]
		public string GameDefeat
		{ get; set; }

		[XmlElement]
		public string ItemTypeWeapon
		{ get; set; }
		[XmlElement]
		public string ItemTypeWear
		{ get; set; }
		[XmlElement]
		public string ItemTypeTool
		{ get; set; }
		[XmlElement]
		public string ItemTypeFood
		{ get; set; }

		[XmlElement]
		public string QuestionWhatIsYourName
		{ get; set; }
		[XmlElement]
		public string QuestionHowOldAreYou
		{ get; set; }
		[XmlElement]
		public string QuestionWhatDoYouDo
		{ get; set; }
		[XmlElement]
		public string QuestionWhereAreWeNow
		{ get; set; }
		[XmlElement]
		public string QuestionWhereAreYouFrom
		{ get; set; }
		[XmlElement]
		public string AnswerFormatNameAgain
		{ get; set; }
		[XmlElement]
		public string AnswerFormatNameFirst
		{ get; set; }
		[XmlElement]
		public string AnswerFormatAge
		{ get; set; }

		#endregion

		public static Language CreateDefault()
		{
			return new Language
			{
				Name = "English",

				SelectInteractionPromt = "Please, select what to do.",
				SelectWeaponPromt = "Please, select weapon to equip.",
				SelectItemToDropPromt = "Please, select item to drop.",
				SelectItemToPickPromt = "Please, select item to pick.",

				InteractionFormat = "{0} ({1})",

				InteractionOpenDoor = "Open the door.",
				InteractionCloseDoor = "Close the door",
				InteractionChopTree = "Chop the tree",
				InteractionBackstab = "Backstab",
				InteractionChat = "Chat",
				InteractionTrade = "Trade",
				InteractionPickpocket = "Pickpocket",
				InteractionPickItem = "Pick item",

				LogActionFormatOpenDoor = "{0} opens the door at {1}.",
				LogActionFormatCloseDoor = "{0} closes the door at {1}.",
				LogActionFormatMove = "{0} moves from {1} to {2}.",
				LogActionFormatMoveDisabled = "{0} attempts to move, but does not succeed.",
				LogActionFormatChopTree = "{0} chops the tree at {1}",
				LogActionFormatStartFight = "{0} prepared to fight using {1}",
				LogActionFormatStopFight = "{0} stopped to fight using {1}",
				LogActionFormatWait = "{0} waits",
				LogActionFormatChangeFightModeDisabled = "{0} didn't manage to become more or less agressive",
				LogActionFormatChangeWeapon = "{0} changed weapon from {1} to {2}",
				LogActionFormatChangeWeaponDisabled = "{0} didn't manage to change weapon",
				LogActionFormatAttack = "{0} attacks {1} with {2}",
				LogActionFormatDeath = "{0} die: {1}",
				LogActionFormatDropItem = "{0} picks {1}",
				LogActionFormatPickItem = "{0} drops {1}",
				LogActionFormatPickItemDisabled = "{0} didn't manage to pick item",

				ReathReasonKilled = "killed by {0}",

				UiCommonOk = "OK",
				UiCommonCancel = "Cancel",

				UiMainNewGame = "Start new game",
				UiMainLoadGame = "Load saved game",
				UiMainHelp = "Show help",
				UiMainExit = "Exit",
				UiMainSelectSave = "Please, choose save file name",
				UiMainSavesFilter = "SaveFiles|*.xml",

				UiCharacterGeneral = "Character",
				UiCharacterBody = "Body",
				UiCharacterEffects = "Effects",
				UiCharacterStats = "Stats",
				UiCharacterSkills = "Skills",
				UiCharacterWearedItems = "Weared Items",
				UiCharacterInventory = "Inventory",

				HelpTitle = "Help",
				HelpText = "Under construction...",

				DirectionNone = "here",
				DirectionLeft = "west",
				DirectionRight = "east",
				DirectionUp = "north",
				DirectionDown = "south",
				DirectionUpLeft = "north-west",
				DirectionUpRight = "north-east",
				DirectionDownLeft = "south-west",
				DirectionDownRight = "south-east",

				BodyPartArm = "Arm",
				BodyPartLeg = "Leg",
				BodyPartTail = "Tail",
				BodyPartWing = "Wing",
				BodyPartBody = "Body",
				BodyPartTentacle = "Tentacle",
				BodyPartHead = "Head",
				BodyPartFace = "Face",
				BodyPartEye = "Eye",
				BodyPartEar = "Ear",
				BodyPartNose = "Nose",
				BodyPartHoove = "Hoove",
				BodyPartHorn = "Horn",
				BodyPartFinger = "Finger",
				BodyPartWrist = "Wrist",
				BodyPartFoot = "Foot",
				BodyPartShoulder = "Shoulder",
				BodyPartElbow = "Elbow",
				BodyPartForearm = "Forearm",
				BodyPartHaunch = "Haunch",
				BodyPartKnee = "Knee",
				BodyPartShin = "Shin",
				BodyPartRibs = "Ribs",
				BodyPartHeart = "Heart",
				BodyPartLung = "Lung",
				BodyPartLiver = "Liver",
				BodyPartStomach = "Stomach",
				BodyPartKidney = "Kidney",
				BodyPartSkull = "Skull",
				BodyPartBrain = "Brain",
				BodyPartHairs = "Hairs",
				BodyPartMouth = "Mouth",
				BodyPartTongue = "Tongue",
				BodyPartThroat = "Throat",
				BodyPartTooth = "Tooth",
				BodyPartSkin = "Skin",

				GameWin = "Congratulations! You win the game.",
				GameDefeat = "Sorry, but game is over.",

				ItemTypeWeapon = "Weapon",
				ItemTypeWear = "Wear",
				ItemTypeFood = "Food",
				ItemTypeTool = "Tool",

				QuestionWhatIsYourName = "What is your name?",
				QuestionHowOldAreYou = "How old are you?",
				QuestionWhatDoYouDo = "What do you do?",
				QuestionWhereAreWeNow = "Where are we now?",
				QuestionWhereAreYouFrom = "Where are you from?",

				AnswerFormatNameAgain = "As I've said before, my name is {0}.",
				AnswerFormatNameFirst = "My name is {0}. - Nice to get aquainted, my name is {1}.",
				AnswerFormatAge = "I'm {0} years old.",
			};
		}

		public override string ToString()
		{
			return Name;
		}
	}
}
