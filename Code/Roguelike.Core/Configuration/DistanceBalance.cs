namespace Roguelike.Core.Configuration
{
	public class DistanceBalance
	{
		#region Properties

		public int AiRange
		{ get; set; }

		public int HeroInitialView
		{ get; set; }

		public int NightVisibilityPercent
		{ get; set; }

		public int TwilightVisibilityPercent
		{ get; set; }

		public int DungeonVisibilityPercent
		{ get; set; }

		#endregion

		public static DistanceBalance CreateDefault()
		{
			return new DistanceBalance
			{
				AiRange = 30,
				HeroInitialView = 15,
				NightVisibilityPercent=20,
				TwilightVisibilityPercent=45,
				DungeonVisibilityPercent=5,
			};
		}
	}
}
