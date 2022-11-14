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

		public int BloatedWaterLevel
		{ get; set; }

		public int TicksToChangeLevel
		{ get; set; }

		public int HungerDeathLevel
		{ get; set; }

		public int OvereatingDeathLevel
		{ get; set; }

		public int OverwaterDeathLevel
		{ get; set; }

		public int ThirstDeathLevel
		{ get; set; }

		public int OverEatingDeathChancePercent
		{ get; set; }

		public int OverWaterDeathChancePercent
		{ get; set; }

	#endregion

	public static FoodBalance CreateDefault()
		{
			return new FoodBalance
			{
				HungerLevel = 0,
				ThirstLevel = 0,
				BloatedLevel = 2000,
				BloatedWaterLevel = 3000,
				TicksToChangeLevel = 1000,
				OvereatingDeathLevel = 15000,
				OverwaterDeathLevel = 8000,
				HungerDeathLevel = -50000,
				ThirstDeathLevel = -10000,
				OverEatingDeathChancePercent = 10,
				OverWaterDeathChancePercent = 99,
			};
		}
	}
}
