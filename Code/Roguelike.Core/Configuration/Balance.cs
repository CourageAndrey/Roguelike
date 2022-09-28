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

		public int MaxLogSize
		{ get; set; }

		public int BaseHitPossibility
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

				MaxLogSize = 512,

				BaseHitPossibility = 50,
			};
		}
	}
}
