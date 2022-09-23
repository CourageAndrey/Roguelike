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

		public int ChangeAgressive
		{ get; set; }

		public int Wait
		{ get; set; }

		public int Attack
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
				ChangeAgressive = 1000,
				Wait = 500,
				Attack = 1000,
			};
		}
	}
}
