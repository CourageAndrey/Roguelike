namespace Roguelike.Core.Configuration
{
	public class Balance
	{
		#region Properties

		public ActionLongevityBalance ActionLongevity
		{ get; set; }

		public TimeBalance Time
		{ get; set; }

		public WorldSizeBalance WorldSize
		{ get; set; }

		public DistanceBalance Distance
		{ get; set; }

		public PlayerBalance Player
		{ get; set; }

		public FoodBalance Food
		{ get; set; }

		public int MaxLogSize
		{ get; set; }

		#endregion

		public static Balance CreateDefault()
		{
			return new Balance
			{
				ActionLongevity = ActionLongevityBalance.CreateDefault(),
				Time = TimeBalance.CreateDefault(),
				WorldSize = WorldSizeBalance.CreateDefault(),
				Distance = DistanceBalance.CreateDefault(),
				Player = PlayerBalance.CreateDefault(),
				Food = FoodBalance.CreateDefault(),

				MaxLogSize = 512,
			};
		}
	}
}
