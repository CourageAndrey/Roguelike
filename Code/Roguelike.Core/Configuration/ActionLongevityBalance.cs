namespace Roguelike.Core.Configuration
{
	public class ActionLongevityBalance
	{
		#region Properties

		public int Null
		{ get; set; }

		public int Step
		{ get; set; }

		public int Disabled
		{ get; set; }

		public int OpenCloseDoor
		{ get; set; }

		public int ChopTree
		{ get; set; }

		public int EquipWeapon
		{ get; set; }

		public int ChangeAggressive
		{ get; set; }

		public int ChangeWeapon
		{ get; set; }

		public int Wait
		{ get; set; }

		public int Attack
		{ get; set; }

		public int Shoot
		{ get; set; }

		public int DropItem
		{ get; set; }

		public int PickItem
		{ get; set; }

		public int ReadBook
		{ get; set; }

		public int RideHorse
		{ get; set; }

		public int Eat
		{ get; set; }

		public int Drink
		{ get; set; }

		public int Backstab
		{ get; set; }

		public DressTimeBalance Dress
		{ get; set; }

		#endregion

		public static ActionLongevityBalance CreateDefault()
		{
			return new ActionLongevityBalance
			{
				Null = 0,
				Step = 1000,
				Disabled = 1000,
				OpenCloseDoor = 2000,
				ChopTree = 24 * 60 * 60 * 1000,
				EquipWeapon = 2000,
				ChangeAggressive = 1000,
				ChangeWeapon = 2500,
				Wait = 500,
				Attack = 1000,
				Shoot = 3000,
				DropItem = 1500,
				PickItem = 1500,
				ReadBook = 60 * 60 * 1000,
				RideHorse = 5 * 60 * 1000,
				Eat = 10 * 60 * 1000,
				Drink = 30 * 1000,
				Backstab = 500,
				Dress = DressTimeBalance.CreateDefault(),
			};
		}
	}
}
