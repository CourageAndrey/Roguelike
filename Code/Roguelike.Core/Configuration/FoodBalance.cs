namespace Roguelike.Core.Configuration
{
	public class FoodBalance
	{
		#region Properties

		public int HungerLevel
		{ get; set; }

		public int ThirstLevel
		{ get; set; }

		public int BloatedLevel
		{ get; set; }

		public int TicksToChangeLevel
		{ get; set; }

		public int HungerDeathLevel
		{ get; set; }

		public int ThirstDeathLevel
		{ get; set; }

		#endregion

		public static FoodBalance CreateDefault()
		{
			return new FoodBalance
			{
				HungerLevel = 0,
				ThirstLevel = 0,
				BloatedLevel = 2000,
				TicksToChangeLevel = 1000,
				HungerDeathLevel = -50000,
				ThirstDeathLevel = -10000,
			};
		}
	}
}
